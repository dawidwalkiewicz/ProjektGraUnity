using UnityEngine;

public class Room : MonoBehaviour
{
    public Wall wall1, wall2;
    public Ceiling ceiling;
    public Floor floor;
    public GameDataManager gdManager;
    public RegulationRing regulationRing;
    MeasureDevice measureDevice;

    void Awake()
    {
        if (gdManager == null)
        {
            gdManager = GameObject.Find("GameDataManager").GetComponent<GameDataManager>();
        }
        if (measureDevice == null)
        {
            measureDevice = GameObject.Find("MeasureDevice").GetComponent<MeasureDevice>();
        }
    }

    void Start()
    {
        if (regulationRing == null)
        {
            regulationRing = gdManager.regulationRing;
        }
        gdManager.statistics.NeutralizedRoomsCounter = 0;
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
        for (int i = 0; i < gdManager.rooms.Count; i++)
        {
            if (gdManager.rooms[i].regulationRing.numberOfRegulationKeyPressed == 0 && gdManager.rooms[i].wall1.isWallHit
                && gdManager.rooms[i].wall2.isWallHit && gdManager.rooms[i].ceiling.isCeilingHit &&
                gdManager.rooms[i].floor.isFloorHit)
            {
                gdManager.statistics.NeutralizedRoomsCounter++;
            }
        }
    }
}