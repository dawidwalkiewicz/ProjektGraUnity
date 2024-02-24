using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager instance;

    public CharacterHealth charHealth;
    public GameObject character;
    public Transform respawnPoint;
    public RegulationRing regulationRing;
    public Stats statistics;
    public List<Door> doors;
    public List<Room> rooms;
    MeasureDevice measureDevice;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        if (measureDevice == null)
        {
            measureDevice = GameObject.Find("MeasureDevice").GetComponent<MeasureDevice>();
        }
    }

    void Start()
    {
        doors = new List<Door>();
        Door door1 = GameObject.Find("Door1").GetComponent<Door>();
        door1.doorValue = Random.Range(1, 9);
        doors.Add(door1);
        Door door2 = GameObject.Find("Door2").GetComponent<Door>();
        door2.doorValue = Random.Range(1, 9);
        doors.Add(door2);
        Door door3 = GameObject.Find("Door3").GetComponent<Door>();
        door3.doorValue = Random.Range(1, 9);
        doors.Add(door3);
        Door door4 = GameObject.Find("Door4").GetComponent<Door>();
        door4.doorValue = Random.Range(1, 9);
        doors.Add(door4);
        Room room1 = GameObject.Find("Room1").GetComponent<Room>();
        rooms.Add(room1);
        Room room2 = GameObject.Find("Room2").GetComponent<Room>();
        rooms.Add(room2);
        Room room3 = GameObject.Find("Room3").GetComponent<Room>();
        rooms.Add(room3);
        Room room4 = GameObject.Find("Room4").GetComponent<Room>();
        rooms.Add(room4);
        for (int i = 0; i < doors.Count; i++)
        {
            if (character != null)
            {
                doors[i].character = character;
            }
            if (charHealth != null)
            {
                doors[i].characterHealth = charHealth;
            }
            if (respawnPoint != null)
            {
                doors[i].respawnPoint = respawnPoint;
            }
            if (regulationRing != null)
            {
                doors[i].regulationRing = regulationRing;
            }
            if (statistics != null)
            {
                doors[i].statistics = statistics;
            }
        }
    }

    void Update()
    {
        Door door = measureDevice.GetClosestDoor();
        door.NeutralizeDoor();
    }
}