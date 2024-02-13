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

    public void Update()
    {
        RaycastHit rcHit;
        if (Physics.Raycast(transform.position, Vector3.forward, out rcHit))
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
            doorValueText.text = "Door value: " + doors[i].doorValue;
        }
    }
}