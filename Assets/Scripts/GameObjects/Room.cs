using UnityEngine;

public class Room : MonoBehaviour
{
    public Wall wall1, wall2;
    public Ceiling ceiling;
    public Floor floor;
    public Stats statistics;
    public RegulationRing regulationRing;
    MeasureDevice measureDevice;

    void Awake()
    {
        if (statistics == null)
        {
            statistics = GameObject.Find("Stats").GetComponent<Stats>();
        }
        if (regulationRing == null)
        {
            regulationRing = GameObject.Find("RegulationRing").GetComponent<RegulationRing>();
        }
        if (measureDevice == null)
        {
            measureDevice = GameObject.Find("MeasureDevice").GetComponent<MeasureDevice>();
        }
        statistics.NeutralizedRoomsCounter = 0;
    }

    void Update()
    {
        Door door = measureDevice.GetClosestDoor();
        if (door.IsNeutralized)
        {
            NeutralizeRoom();
        }
    }

    public void NeutralizeRoom()
    {
        if (regulationRing.numberOfRegulationKeyPressed == 0 & wall1.isWallHit & wall2.isWallHit & ceiling.isCeilingHit && floor.isFloorHit)
        {
            statistics.NeutralizedRoomsCounter++;
        }
    }
}