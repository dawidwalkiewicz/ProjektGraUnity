using UnityEngine;

public class Stats : MonoBehaviour
{
    public string GameCompletionTime { get; set; }
    public int NeutralizedDoorsCounter { get; set; }
    public int UnneutralizedDoorsCounter { get; set; }
    public int NeutralizedRoomsCounter { get; set; }
    public int WallsCounter { get; set; }
    public bool WasMeasurementSet { get; set; }
    public bool TooHighValue { get; set; }
    public bool TooLowValue { get; set; }
    public bool WrongRingSettings { get; set; }
    public int MissedWalls { get; set; }
    public int WallsHitMoreThanOnce { get; set; }

    public void ResetStats()
    {
        GameCompletionTime = "0:00,000";
        NeutralizedDoorsCounter = 0;
        UnneutralizedDoorsCounter = 4;
        NeutralizedRoomsCounter = 0;
        WallsCounter = 0;
        WasMeasurementSet = false;
        TooHighValue = false;
        TooLowValue = false;
        WrongRingSettings = false;
        MissedWalls = 3;
        WallsHitMoreThanOnce = 0;
    }
}