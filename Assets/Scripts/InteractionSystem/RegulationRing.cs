using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RegulationRing : MonoBehaviour
{
    public float regulationMovement;
    [Header("Keybinds")]
    public KeyCode regulateMinusKey = KeyCode.KeypadMinus;
    public KeyCode regulatePlusKey = KeyCode.KeypadPlus;

    public HashSet<KeyCode> keysToCheck;
    public int numberOfRegulationKeyPressed = 0;
    private int defualtNumberOfRegulationKeyPressed = 0;
    public float speed = 30;

    public GameDataManager gdManager;
    public Stats statistics;
    public MeasureDevice measureDevice;
    public List<Door> doors;

    void Awake()
    {
        if (statistics == null)
        {
            statistics = GameObject.Find("Stats").GetComponent<Stats>();
        }
        statistics.TooHighValue = false;
        statistics.TooLowValue = false;
        statistics.WrongRingSettings = false;
    }

    void Start()
    {
        doors = new List<Door>();
        if (gdManager != null && gdManager.doors != null)
        {
            for (int i = 0; i < gdManager.doors.Count; i++)
            {
                doors.Add(gdManager.doors[i]);
            }
        }
        if (measureDevice != null)
        {
            measureDevice.gdManager = gdManager;
        }
    }

    void Update()
    {
        Door door = measureDevice.GetClosestDoor();
        RegulateDoor(door);
    }

    public void RegulateDoor(Door door)
    {
        if (Input.GetKeyDown(regulateMinusKey))
        {
            RegulateRingMinus(door);
            numberOfRegulationKeyPressed++;
        }
        else if (Input.GetKeyDown(regulatePlusKey))
        {
            RegulateRingPlus(door);
            numberOfRegulationKeyPressed--;
        }
    }

    public void IsRingReseted()
    {
        if (numberOfRegulationKeyPressed == defualtNumberOfRegulationKeyPressed)
        {
            statistics.WrongRingSettings = false;
            statistics.TooLowValue = false;
            statistics.TooHighValue = false;
        }
        else
        {
            statistics.WrongRingSettings = true;
            if (numberOfRegulationKeyPressed > defualtNumberOfRegulationKeyPressed)
            {
                statistics.TooLowValue = true;
                statistics.TooHighValue = false;
            }
            else if (numberOfRegulationKeyPressed < defualtNumberOfRegulationKeyPressed)
            {
                statistics.TooHighValue = true;
                statistics.TooLowValue = false;
            }
        }
    }

    public void RegulateRingPlus(Door door)
    {
        if (door != null)
        {
            transform.Rotate(speed * Time.deltaTime * Vector3.up);
        }
    }

    public void RegulateRingMinus(Door door)
    {
        if (door != null)
        {
            if (door.doorValue > door.minValue)
            {
                transform.Rotate(speed * Time.deltaTime * -Vector3.up);
                door.doorValue--;
                measureDevice.UpdateTextAndValue(doors.IndexOf(door), door.doorValue);
            }
            else if (door.doorValue == door.minValue)
            {
                measureDevice.MeasureDoorValue();
                measureDevice.UpdateTextAndValue(doors.IndexOf(door), door.doorValue);
            }
        }
    }
}