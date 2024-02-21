using UnityEngine;

public class Ceiling : MonoBehaviour
{
    public WeaponSystem weapon;
    public Stats statistics;
    public bool isCeilingHit = false;
    private int bulletHitCount = 0;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            bulletHitCount++;
            WasCeilingHit(collision);
        }
    }

    public void WasCeilingHit(Collision collision)
    {
        if (bulletHitCount == 1)
        {
            isCeilingHit = true;
            statistics.WallsCounter++;
            statistics.MissedWalls--;
        }
        else if (bulletHitCount > 1)
        {
            isCeilingHit = true;
            statistics.WallsCounter++;
            statistics.MissedWalls--;
            statistics.WallsHitMoreThanOnce++;
        }
    }
}