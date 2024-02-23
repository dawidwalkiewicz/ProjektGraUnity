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
    public List<Wall> walls;
    public Ceiling ceiling;
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
        walls = new List<Wall>();
        Wall wall1 = GameObject.Find("Wall1").GetComponent<Wall>();
        walls.Add(wall1);
        Wall wall2 = GameObject.Find("Wall2").GetComponent<Wall>();
        walls.Add(wall2);
        ceiling = GameObject.Find("Ceiling").GetComponent<Ceiling>();
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