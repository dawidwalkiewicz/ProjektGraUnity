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
    public Stats statistics;
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
                RegulateRingMinus(doors[i]);
            }
        }
    }

    public void ResetRingSetting()
    {
        foreach (var door in doors)
        {
            while (numberOfRegulationKeyPressed > 0)
            {
                RegulateRingPlus(door);
                numberOfRegulationKeyPressed--;
            }
        }
    }

    public void RegulateRingPlus(Door door)
    {
        if (door != null && Input.GetKey(regulatePlusKey))
        {
            transform.Rotate(speed * Time.deltaTime * Vector3.up);
            /*if (door.doorValue < door.maxValue)
            {
                measureDevice.UpdateTextAndValue(doors.IndexOf(door), door.doorValue++);
            }*/
        }
    }

    public void RegulateRingMinus(Door door)
    {
        if (door != null)
        {
            if (door.doorValue > door.minValue && Input.GetKey(regulateMinusKey))
            {
                transform.Rotate(speed * Time.deltaTime * -Vector3.up);
                measureDevice.UpdateTextAndValue(doors.IndexOf(door), door.doorValue--);
            }
            else if (door.doorValue == door.minValue)
            {
                measureDevice.MeasureDoorValue();
                measureDevice.doorValueText.text = "Door value: 0" + System.Environment.NewLine + "Door neutralized.";
                statistics.NeutralizedDoorsCounter++;
                statistics.UnneutralizedDoorsCounter--;
            }
        }
    }
}