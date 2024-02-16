using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RegulationRing : MonoBehaviour
{
    public float regulationMovement;
    [Header("Keybinds")]
    public KeyCode regulateMinusKey = KeyCode.Minus;
    public KeyCode regulatePlusKey = KeyCode.Plus;

    private readonly HashSet<KeyCode> keysToCheck;
    int numberOfRegulationKeyPressed;

    public MeasureDevice measureDevice;
    public List<Door> doors;
    Animator _ringAnim;

    void Start()
    {
        _ringAnim = this.transform.parent.GetComponent<Animator>();
    }

    void Update()
    {
        RegulateDoor(doors);
        ResetRingSetting();
    }

    public void RegulateDoor(List<Door> doors)
    {
        for (int i = 0; i <= doors.Count; i++)
        {
            numberOfRegulationKeyPressed = 0;
            if (doors[i].doorValue > 0)
            {
                numberOfRegulationKeyPressed = keysToCheck.Count(key => Input.GetKey(regulateMinusKey));
                for (int j = 0; j <= numberOfRegulationKeyPressed; j++)
                {
                    doors[i].doorValue--;
                    measureDevice.MeasureDoorValue();
                }
            }
            else if (doors[i].doorValue == 0)
            {
                measureDevice.doorValueText.text = "Door value: 0" + System.Environment.NewLine + "Door neutralized.";
            }
        }
    }

    public void ResetRingSetting()
    {
        if (numberOfRegulationKeyPressed != 0)
        {
            for (int i = 0; i <= numberOfRegulationKeyPressed; i++)
            {
                if (Input.GetKey(regulatePlusKey))
                {
                    numberOfRegulationKeyPressed--;
                }
            }
        }
    }
}