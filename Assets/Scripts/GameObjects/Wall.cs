using UnityEngine;

public class Wall : MonoBehaviour
{
    public WeaponSystem weapon;
    public Stats statistics;
    public RegulationRing regulationRing;
    public bool isWallHit = false;
    private int bulletHitCount = 0;

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
        statistics.WallsCounter = 0;
        statistics.MissedWalls = 3;
        statistics.WallsHitMoreThanOnce = 0;
    }

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