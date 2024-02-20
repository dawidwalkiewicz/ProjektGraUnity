using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;

public class RegulationRing : MonoBehaviour
{
    public float regulationMovement;
    [Header("Keybinds")]
    public KeyCode regulateMinusKey = KeyCode.Minus;
    public KeyCode regulatePlusKey = KeyCode.Plus;

    public HashSet<KeyCode> keysToCheck;
    int numberOfRegulationKeyPressed;
    public float speed = 30;

    public GameDataManager gdManager;
    public MeasureDevice measureDevice;
    public List<Door> doors;

    void Start()
    {
        doors = new List<Door>();
        measureDevice = gameObject.AddComponent<MeasureDevice>();
        for (int i = 0; i <= gdManager.doors.Count - 1; i++)
        {
            doors.Add(gdManager.doors[i]);
        }
    }

    void Update()
    {
        RegulateDoor(doors);
        ResetRingSetting();
    }

    public void RegulateDoor(List<Door> doors)
    {
        for (int i = 0; i <= doors.Count - 1; i++)
        {
            numberOfRegulationKeyPressed = 0;
            if (doors[i].doorValue > 0)
            {
                numberOfRegulationKeyPressed = keysToCheck.Count(key => Input.GetKey(regulateMinusKey));
                for (int j = 0; j <= numberOfRegulationKeyPressed; j++)
                {
                    doors[i].doorValue--;
                    measureDevice.doorValueText.text = "Door value: " + doors[i].doorValue;
                }
            }
            else if (doors[i].doorValue == 0)
            {
                measureDevice.MeasureDoorValue();
                measureDevice.doorValueText.text = "Door value: 0" + System.Environment.NewLine + "Door neutralized.";
            }
        }
    }

    public void ResetRingSetting()
    {
        if (numberOfRegulationKeyPressed != 0)
        {
            for (int i = 1; i <= numberOfRegulationKeyPressed; i++)
            {
                RegulateRingPlus();
                numberOfRegulationKeyPressed--;
            }
        }
    }

    public void RegulateRingPlus()
    {
        if (Input.GetKey(regulatePlusKey))
        {
            transform.Rotate(speed * Time.deltaTime * Vector3.up);
        }
    }

    public void RegulateRingMinus()
    {
        if (Input.GetKey(regulatePlusKey))
        {
            transform.Rotate(speed * Time.deltaTime * -Vector3.up);
        }
    }
}