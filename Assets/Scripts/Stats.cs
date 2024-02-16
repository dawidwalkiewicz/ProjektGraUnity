using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public string GameCompletionTime { get; set; }
    public int NeutralizedDoorsCounter { get; set; }
    public int UnneutralizedDoorsCounter { get; set; }
    public int NeutralizedRoomsCounter { get; set; }
    public int WallsCounter { get; set; }
    public int WallsHitOncePlusCounter { get; set; }
    public bool WasMeasurementSet { get; set; }
    public bool TooHighValue { get; set; }
    public bool TooLowValue { get; set; }
    public bool WrongRingSettings { get; set; }
    public int MissedWalls { get; set; }
    public int WallsHitMoreThanOnce { get; set; }
}