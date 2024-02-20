using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameDataManager : MonoBehaviour
{
    public Stats statistics;
    public RegulationRing regulationRing;

    public static GameDataManager instance;
    public bool dead = false;
    public bool neutralizedDoors = false;
    public bool tooCloseToTheDoor = false;
    public Vector3 characterPosition;
    public RaycastHit rcHit;
    float distance;

    public CharacterHealth charHealth;
    public List<Door> doors;
    public List<Wall> walls;
    public Ceiling ceiling;

    void Awake()
    {
        instance = this;
        doors = new List<Door>();
        walls = new List<Wall>();
        statistics = gameObject.AddComponent<Stats>();
        characterPosition = new Vector3(-3f, 0f, 10f);
    }

    void Start()
    {
        Door door1 = gameObject.AddComponent<Door>();
        door1.doorValue = Random.Range(0, 9);
        Door door2 = gameObject.AddComponent<Door>();
        door2.doorValue = Random.Range(0, 9);
        Door door3 = gameObject.AddComponent<Door>();
        door3.doorValue = Random.Range(0, 9);
        Door door4 = gameObject.AddComponent<Door>();
        door4.doorValue = Random.Range(0, 9);
        Wall wall1 = gameObject.AddComponent<Wall>();
        Wall wall2 = gameObject.AddComponent<Wall>();
        ceiling = gameObject.AddComponent<Ceiling>();
        doors.Add(door1);
        doors.Add(door2);
        doors.Add(door3);
        doors.Add(door4);
        walls.Add(wall1);
        walls.Add(wall2);
        statistics.NeutralizedDoorsCounter = 0;
        statistics.UnneutralizedDoorsCounter = 4;
        statistics.NeutralizedRoomsCounter = 0;
        statistics.MissedWalls = 3;
    }

    void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.forward, out rcHit))
        {
            distance = rcHit.distance;
        }
        NeutralizeDoor();
    }

    void NeutralizeDoor()
    {
        for (int i = 0; i <= doors.Count - 1; i++)
        {
            if (doors[i].doorValue > 0)
            {
                tooCloseToTheDoor = true;
                doors[i].GetComponent<MeshRenderer>().material.color = Color.red;
            }
            else if (doors[i].doorValue > 0 && distance <= 3.5 && distance > 1)
            {
                regulationRing.RegulateDoor(doors);
            }
            else
            {
                neutralizedDoors = true;
                statistics.WasMeasurementSet = true;
                statistics.NeutralizedDoorsCounter++;
                statistics.UnneutralizedDoorsCounter--;
                doors[i].transform.position += new Vector3(0f, 10f, 0f);
                ShootWalls();
            }
        }
    }

    void ShootWalls()
    {
        for (int i = 0; i <= walls.Count - 1; i++)
        {
            walls[i].WasWallHit();
        }
        ceiling.WasCeilingHit();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Door") && collision.gameObject.layer.Equals("whatIsDoor"))
        {
            charHealth.TakeDamage(1);
        }
    }
}