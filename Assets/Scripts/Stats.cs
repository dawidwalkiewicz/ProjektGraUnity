using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public string gameCompletionTime = "0:00,000";
    public int neutralizedDoorsCounter = 0;
    public int unneutralizedDoorsCounter = 4;
    public int neutralizedRoomsCounter = 0;
    public int wallsCounter;
    public int wallsHitOncePlusCounter;
    public bool wasMeasurementSet;
    public bool tooHighValue;
    public bool tooLowValue;
    public bool wrongRingSettings;
    public int missedWalls;
    public int wallsHitMoreThanOnce = 0;
}