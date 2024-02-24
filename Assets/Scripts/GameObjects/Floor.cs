using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public WeaponSystem weapon;
    public Stats statistics;
    public RegulationRing regulationRing;
    public bool isFloorHit = false;
    private int bulletHitCount = 0;
    private float health = 100f;

    void Awake()
    {
        if (weapon == null)
        {
            weapon = GameObject.Find("Weapon").GetComponent<WeaponSystem>();
        }
        if (statistics == null)
        {
            statistics = GameObject.Find("Stats").GetComponent<Stats>();
        }
        if (regulationRing == null)
        {
            regulationRing = GameObject.Find("RegulationRing").GetComponent<RegulationRing>();
        }
        statistics.MissedWalls = 4;
        statistics.WallsCounter = 0;
        statistics.WallsHitMoreThanOnce = 0;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
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
            statistics.WallsCounter++;
            statistics.MissedWalls--;
        }
        else if (bulletHitCount > 1)
        {
            isFloorHit = true;
            statistics.WallsCounter++;
            statistics.MissedWalls--;
            statistics.WallsHitMoreThanOnce++;
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