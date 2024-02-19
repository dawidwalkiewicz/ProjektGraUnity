using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MeasureDevice : MonoBehaviour
{
    [Header("Keybinds")]
    public KeyCode measureKey = KeyCode.M;

    public List<Door> doors;
    public Text doorValueText;

    public Vector3 measureDevicePosition;

    void Awake()
    {
        measureDevicePosition = transform.position;
    }

    void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.forward, out RaycastHit rcHit))
        {
            float distance = rcHit.distance;

            if (distance <= 3.5 && distance > 0.3)
            {
                MeasureDoorValue();
            }
        }
    }

    public void MeasureDoorValue()
    {
        for (int i = 0; i < doors.Count - 1; i++)
        {
            if (Input.GetKeyDown(measureKey))
            {
                transform.position = new Vector3(0.7f, 0.1f, 0.1f);
                doorValueText.text = "Door value: " + doors[i].doorValue;
            }
            else if (Input.GetKeyUp(measureKey))
            {
                transform.position = measureDevicePosition;
            }
        }
    }
}