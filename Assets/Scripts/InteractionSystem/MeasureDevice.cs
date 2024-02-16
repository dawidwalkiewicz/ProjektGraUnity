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
        for (int i = 0; i < doors.Count; i++)
        {
            if (Input.GetKey(measureKey))
            {
                doorValueText.text = "Door value: " + doors[i].doorValue;
            }
        }
    }
}