using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ceiling : MonoBehaviour
{
    public WeaponSystem weapon;
    public Stats statistics;
    public bool isCeilingHit = false;

    void Start()
    {
        weapon = gameObject.AddComponent<WeaponSystem>();
        statistics = gameObject.AddComponent<Stats>();
    }

    void Update()
    {
        WasCeilingHit();
    }

    public void WasCeilingHit()
    {
        if (weapon.damage == 1)
        {
            isCeilingHit = true;
            statistics.WallsCounter++;
            statistics.MissedWalls--;
        }
        else if (weapon.damage > 1)
        {
            statistics.WallsHitMoreThanOnce++;
        }
    }
}