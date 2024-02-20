using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public WeaponSystem weapon;
    public Stats statistics;
    public bool isWallHit = false;

    void Start()
    {
        weapon = gameObject.AddComponent<WeaponSystem>();
        statistics = gameObject.AddComponent<Stats>();
    }

    void Update()
    {
        WasWallHit();
    }

    public void WasWallHit()
    {
        if (weapon.damage == 1)
        {
            isWallHit = true;
            statistics.WallsCounter++;
            statistics.MissedWalls--;
        }
        else if (weapon.damage > 1)
        {
            statistics.WallsHitMoreThanOnce++;
        }
    }
}