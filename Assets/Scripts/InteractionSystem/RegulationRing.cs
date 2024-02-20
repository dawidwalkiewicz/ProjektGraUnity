using System.Collections.Generic;
using System.Linq;
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
        for (int i = 0; i < gdManager.doors.Count; i++)
        {
            doors.Add(gdManager.doors[i]);
        }
        measureDevice.gdManager = gdManager;
    }

    void Update()
    {
        RegulateDoor(doors);
        ResetRingSetting();
    }

    public void RegulateDoor(List<Door> doors)
    {
        for (int i = 0; i < doors.Count; i++)
        {
            if (doors[i] != null)
            {
                RegulateRingMinus(doors[i].doorValue);
            }
        }
    }

    public void ResetRingSetting()
    {
        while (numberOfRegulationKeyPressed > 0)
        {
            RegulateRingPlus();
            numberOfRegulationKeyPressed--;
        }
    }

    public void RegulateRingPlus()
    {
        if (Input.GetKey(regulatePlusKey))
        {
            transform.Rotate(speed * Time.deltaTime * Vector3.up);
            if (numberOfRegulationKeyPressed > 0)
            {
                numberOfRegulationKeyPressed--;
            }
        }
    }

    public void RegulateRingMinus(int index)
    {
        for (int i = 0; i < doors.Count; i++)
        {
            if (doors[i] != null)
            {
                if (doors[i].doorValue > 0 && Input.GetKey(regulateMinusKey))
                {
                    transform.Rotate(speed * Time.deltaTime * -Vector3.up);
                    measureDevice.UpdateTextAndValue(i, doors[i].doorValue--);
                }
                else if (doors[i].doorValue == 0)
                {
                    measureDevice.MeasureDoorValue();
                    measureDevice.doorValueText.text = "Door value: 0" + System.Environment.NewLine + "Door neutralized.";
                    gdManager.statistics.NeutralizedDoorsCounter++;
                    gdManager.statistics.UnneutralizedDoorsCounter--;
                }
            }
        }
    }
}