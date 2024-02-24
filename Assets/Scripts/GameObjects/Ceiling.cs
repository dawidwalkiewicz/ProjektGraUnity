using UnityEngine;

public class Ceiling : MonoBehaviour
{
    public WeaponSystem weapon;
    public Stats statistics;
    public RegulationRing regulationRing;
    public bool isCeilingHit = false;
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
        statistics.MissedWalls = 4;
        statistics.WallsCounter = 0;
        statistics.WallsHitMoreThanOnce = 0;
    }

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