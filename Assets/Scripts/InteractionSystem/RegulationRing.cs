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

    public Door door;
    public MeasureDevice measureDevice;

    public bool Interact(Interactor interactor)
    {
        if (door.doorValue > 0)
        {
            numberOfRegulationKeyPressed = keysToCheck.Count(key => Input.GetKey(regulateMinusKey));
            for (int i = door.doorValue; i <= numberOfRegulationKeyPressed; i++)
            {
                door.doorValue--;
                measureDevice.Update();
            }
        }
        else if (door.doorValue == 0)
        {
            measureDevice.doorValueText.text = "Door value: 0/nDoor neutralized.";
        }
        return true;
    }
}