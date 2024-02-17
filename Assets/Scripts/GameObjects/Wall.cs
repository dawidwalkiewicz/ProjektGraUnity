using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    readonly WeaponSystem weapon;
    public bool isWallHit = false;

    void Update()
    {
        WasWallHit();
    }

    public void WasWallHit()
    {
        if (weapon.damage == 1)
        {
            isWallHit = true;
        }
    }
}