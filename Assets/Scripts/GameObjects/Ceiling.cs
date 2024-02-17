using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ceiling : MonoBehaviour
{
    readonly WeaponSystem weapon;
    public bool isCeilingHit = false;

    void Update()
    {
        WasCeilingHit();
    }

    public void WasCeilingHit()
    {
        if (weapon.damage == 1)
        {
            isCeilingHit = true;
        }
    }
}