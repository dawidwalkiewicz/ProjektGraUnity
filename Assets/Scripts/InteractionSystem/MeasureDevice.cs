using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeasureDevice : MonoBehaviour
{
    [Header("Keybinds")]
    public KeyCode measureKey = KeyCode.M;

    public Text doorValueText;
    public List<Door> doors;

    public Transform measureDevicePosition;
    public GameDataManager gdManager;

    void Awake()
    {
        transform.position = measureDevicePosition.position;
    }

    void Start()
    {
        doorValueText.enabled = false;
        doors = gdManager.doors;
    }

    void Update()
    {
        MeasureDoorValue();
    }

    public void MeasureDoorValue()
    {
        if (Input.GetKeyDown(measureKey))
        {
            Door closestDoor = GetClosestDoor();
            if (closestDoor != null)
            {
                doorValueText.enabled = true;
                UpdateTextAndValue(doors.IndexOf(closestDoor), closestDoor.doorValue);
            }
            else
            {
                doorValueText.enabled = false;
            }
        }
        else if (Input.GetKeyUp(measureKey))
        {
            doorValueText.enabled = false;
        }
    }

    public Door GetClosestDoor()
    {
        Door closestDoor = null;
        float closestDistance = 10.0f;
        foreach (Door door in doors)
        {
            float distance = Vector3.Distance(transform.position, door.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestDoor = door;
            }
        }
        return closestDoor;
    }

    public void UpdateTextAndValue(int index, int newValue)
    {
        if (index >= 0 && index < doors.Count)
        {
            doors[index].doorValue = newValue;
            if (newValue == doors[index].minValue && !doors[index].IsNeutralized)
            {
                doorValueText.text = "Door value: " + newValue.ToString() + System.Environment.NewLine + "Neutralize the door";
            }
            else if (newValue == doors[index].minValue && doors[index].IsNeutralized)
            {
                doorValueText.text = "Door value: " + newValue.ToString() + System.Environment.NewLine + "Door neutralized";
            }
            else if (newValue > doors[index].minValue)
            {
                gdManager.statistics.TooLowValue = true;
                gdManager.statistics.TooHighValue = false;
                doorValueText.text = "Door value: " + newValue.ToString() + System.Environment.NewLine + "Too low value set";
            }
            else if (newValue < doors[index].minValue)
            {
                gdManager.statistics.TooLowValue = false;
                gdManager.statistics.TooHighValue = true;
                doorValueText.text = "Door value: " + newValue.ToString() + System.Environment.NewLine + "Too high value set";
            }
        }
    }
}