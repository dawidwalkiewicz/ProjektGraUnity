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
        for (int i = 0; i <= gdManager.doors.Count - 1; i++)
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
        for (int i = 0; i < doors.Count - 1; i++)
        {
            if (Input.GetKeyDown(measureKey))
            {
                doorValueText.enabled = true;
                transform.position = new Vector3(0.7f, 0.1f, 0.1f);
                doorValueText.text = "Door value: " + doors[i].doorValue;
            }
            else if (Input.GetKeyUp(measureKey))
            {
                transform.position = measureDevicePosition.position;
                doorValueText.enabled = false;
            }
        }
    }
}