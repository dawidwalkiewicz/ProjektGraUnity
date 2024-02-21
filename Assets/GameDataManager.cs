using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager instance;

    public CharacterHealth charHealth;
    public List<Door> doors;
    public List<Wall> walls;
    public Ceiling ceiling;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        //doors.Clear();
        //walls.Clear();
        for (int i = 0; i < 4; i++)
        {
            Door door = gameObject.AddComponent<Door>();
            doors.Add(door);
            if (doors.Count > 0)
            {
                doors[i].doorValue = Random.Range(0, 9);
            }
        }
        for (int j = 0; j < 2; j++)
        {
            Wall wall = gameObject.AddComponent<Wall>();
            walls.Add(wall);
        }
        ceiling = gameObject.AddComponent<Ceiling>();
    }

    void Update()
    {
        for (int i = 0; i < doors.Count; i++)
        {
            doors[i].NeutralizeDoor();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Door") && collision.gameObject.layer.Equals("whatIsDoor"))
        {
            charHealth.TakeDamage(1);
        }
    }
}