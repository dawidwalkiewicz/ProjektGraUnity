using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public WeaponSystem weapon;
    public GameDataManager gdManager;
    public RegulationRing regulationRing;
    public GameObject prefab;
    public bool isFloorHit = false;
    private int bulletHitCount = 0;
    private float health = 100f;
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
            gdManager.statistics.MissedWalls += 1;
            gdManager.statistics.WallsCounter = 0;
            gdManager.statistics.WallsHitMoreThanOnce = 0;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == prefab.name)
        {
            bulletHitCount++;
            WasFloorHit(collision);
        }
    }

    public void WasFloorHit(Collision collision)
    {
        if (bulletHitCount == 1)
        {
            isFloorHit = true;
            gdManager.statistics.WallsCounter++;
            gdManager.statistics.MissedWalls--;
        }
        else if (bulletHitCount > 1)
        {
            isFloorHit = true;
            gdManager.statistics.WallsCounter++;
            gdManager.statistics.MissedWalls--;
            gdManager.statistics.WallsHitMoreThanOnce++;
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}