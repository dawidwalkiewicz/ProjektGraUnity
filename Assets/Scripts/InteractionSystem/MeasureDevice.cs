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
    //public Transform newMeasureDevicePosition;
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
            doors.Add(gdManager.doors[i]);
        }
    }

    void Update()
    {
        MeasureDoorValue();
    }

    public void MeasureDoorValue()
    {
        for (int i = 0; i < doors.Count; i++)
        {
            if (doors[i] != null)
            {
                if (Input.GetKeyDown(measureKey))
                {
                    doorValueText.enabled = true;
                    doorValueText.text = "Door value: " + doors[i].doorValue;
                    //transform.position = newMeasureDevicePosition.position;
                }
                else if (Input.GetKeyUp(measureKey))
                {
                    doorValueText.enabled = false;
                    //transform.position = measureDevicePosition.position;
                }
            }
        }
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