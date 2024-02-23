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
                if (closestDoor.doorValue == closestDoor.minValue)
                {
                    doorValueText.text = "Door value: " + closestDoor.doorValue + System.Environment.NewLine + "Door neutralized.";
                }
                else
                {
                    doorValueText.text = "Door value: " + closestDoor.doorValue;
                }
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
            if (newValue == doors[index].minValue)
            {
                doorValueText.text = "Door value: " + newValue.ToString() + System.Environment.NewLine + "Door neutralized.";
            }
            else
            {
                doorValueText.text = "Door value: " + newValue.ToString();
            }
        }
    }
}