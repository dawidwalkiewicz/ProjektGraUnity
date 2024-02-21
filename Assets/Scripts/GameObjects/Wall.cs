using UnityEngine;

public class Wall : MonoBehaviour
{
    public WeaponSystem weapon;
    public Stats statistics;
    public bool isWallHit = false;
    private int bulletHitCount = 0;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
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
            statistics.WallsCounter++;
            statistics.MissedWalls--;
        }
        else if (bulletHitCount > 1)
        {
            isWallHit = true;
            statistics.WallsCounter++;
            statistics.MissedWalls--;
            statistics.WallsHitMoreThanOnce++;
        }
    }
}