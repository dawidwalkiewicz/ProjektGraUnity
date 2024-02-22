using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        doors = new List<Door>();
        for (int i = 0; i < gdManager.doors.Count; i++)
        {
            Door door = gdManager.doors[i].GetComponent<Door>();
            if (door != null)
            {
                doors.Add(door);
            }
        }
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
                doorValueText.text = "Door value: " + closestDoor.doorValue;
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

    private Door GetClosestDoor()
    {
        Door closestDoor = null;
        float closestDistance = float.MaxValue;
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
            doorValueText.text = "Door value: " + doors[index].doorValue.ToString();
        }
    }
}