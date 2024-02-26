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
    private readonly int defaultNumberOfRegulationKeyPressed = 0;
    public float speed = 30;

    public GameDataManager gdManager;
    public MeasureDevice measureDevice;
    public List<Door> doors;
    public List<Room> rooms;

    void Awake()
    {
        if (gdManager == null)
        {
            gdManager = GameObject.Find("GameDataManager").GetComponent<GameDataManager>();
        }
        if (measureDevice == null)
        {
            measureDevice = GameObject.Find("MeasureDevice").GetComponent<MeasureDevice>();
        }
    }

    void Start()
    {
        doors = gdManager.doors;
        rooms = gdManager.rooms;
        gdManager.statistics.TooHighValue = false;
        gdManager.statistics.TooLowValue = false;
        gdManager.statistics.WrongRingSettings = false;
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
        if (numberOfRegulationKeyPressed == defaultNumberOfRegulationKeyPressed)
        {
            gdManager.statistics.WrongRingSettings = false;
            measureDevice.doorValueText.text = "Ring reseted";
        }
        else
        {
            gdManager.statistics.WrongRingSettings = true;
            measureDevice.doorValueText.text = "Wrong ring settings";
        }
    }

    public void RegulateRingPlus(Door door)
    {
        if (door != null && door.doorValue < door.minValue && !door.IsNeutralized)
        {
            transform.Rotate(speed * Time.deltaTime * Vector3.up);
            gdManager.statistics.TooHighValue = true;
            gdManager.statistics.TooLowValue = false;
            measureDevice.doorValueText.text += System.Environment.NewLine + "Too high value set";
        }
        else if (door.IsNeutralized)
        {
            transform.Rotate(speed * Time.deltaTime * Vector3.up);
            IsRingReseted();
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
                gdManager.statistics.TooLowValue = false;
                gdManager.statistics.TooHighValue = false;
                measureDevice.MeasureDoorValue();
                measureDevice.UpdateTextAndValue(doors.IndexOf(door), door.doorValue);
                gdManager.statistics.WasMeasurementSet = true;
            }
        }
    }
}