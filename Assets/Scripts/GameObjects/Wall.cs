using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public WeaponSystem weapon;
    public GameDataManager gdManager;
    public RegulationRing regulationRing;
    public GameObject prefab;
    public bool isWallHit = false;
    private int bulletHitCount = 0;
    List<Room> rooms;

    void Awake()
    {
        if (weapon == null)
        {
            weapon = GameObject.Find("Weapon").GetComponent<WeaponSystem>();
        }
        if (gdManager == null)
        {
            gdManager = GameObject.Find("GameDataManager").GetComponent<GameDataManager>();
        }
    }

    void Start()
    {
        if (regulationRing == null)
        {
            regulationRing = gdManager.regulationRing;
        }
        if (rooms == null)
        {
            rooms = gdManager.rooms;
        }
        for (int i = 0; i < rooms.Count; i++)
        {
            gdManager.statistics.MissedWalls += 2;
            gdManager.statistics.WallsCounter = 0;
            gdManager.statistics.WallsHitMoreThanOnce = 0;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == prefab.name)
        {
            bulletHitCount++;
            WasWallHit(collision);
        }
    }

    public void WasWallHit(Collision collision)
    {
        if (bulletHitCount == 1)
        {
            isWallHit = true;
            gdManager.statistics.WallsCounter++;
            gdManager.statistics.MissedWalls--;
        }
        else if (bulletHitCount > 1)
        {
            isWallHit = true;
            gdManager.statistics.WallsCounter++;
            gdManager.statistics.MissedWalls--;
            gdManager.statistics.WallsHitMoreThanOnce++;
        }
    }
}