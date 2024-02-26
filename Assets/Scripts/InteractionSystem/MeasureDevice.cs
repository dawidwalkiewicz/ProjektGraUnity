using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MeasureDevice : MonoBehaviour
{
    [Header("Keybinds")]
    public KeyCode measureKey = KeyCode.M;

    public Text doorValueText;

    public Transform measureDevicePosition;
    public GameDataManager gdManager;

    void Awake()
    {
        transform.position = measureDevicePosition.position;
    }

    void Start()
    {
        doorValueText.enabled = false;
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
                UpdateTextAndValue(gdManager.doors.IndexOf(closestDoor), closestDoor.doorValue);
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
        foreach (Door door in gdManager.doors)
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

    public Room GetClosestRoom()
    {
        Room closestRoom = null;
        float closestDistance = 1.0f;
        foreach (Room room in gdManager.rooms)
        {
            float distance = Vector3.Distance(transform.position, room.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestRoom = room;
            }
        }
        return closestRoom;
    }

    public void UpdateTextAndValue(int index, int newValue)
    {
        if (index >= 0 && index < gdManager.doors.Count)
        {
            gdManager.doors[index].doorValue = newValue;
            if (newValue == gdManager.doors[index].minValue && !gdManager.doors[index].IsNeutralized)
            {
                doorValueText.text = "Door value: " + newValue.ToString() + System.Environment.NewLine + "Neutralize the door";
            }
            else if (newValue == gdManager.doors[index].minValue && gdManager.doors[index].IsNeutralized)
            {
                doorValueText.text = "Door value: " + newValue.ToString() + System.Environment.NewLine + "Door neutralized";
            }
            else if (newValue > gdManager.doors[index].minValue)
            {
                gdManager.statistics.TooLowValue = true;
                gdManager.statistics.TooHighValue = false;
                doorValueText.text = "Door value: " + newValue.ToString() + System.Environment.NewLine + "Too low value set";
            }
            else if (newValue < gdManager.doors[index].minValue)
            {
                gdManager.statistics.TooLowValue = false;
                gdManager.statistics.TooHighValue = true;
                doorValueText.text = "Door value: " + newValue.ToString() + System.Environment.NewLine + "Too high value set";
            }
        }
    }
}