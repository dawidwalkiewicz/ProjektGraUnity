using UnityEngine;
using UnityEngine.UI;

public class WeaponSystem : MonoBehaviour
{
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    bool shooting, readyToShoot, reloading;

    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    public GameObject muzzleFlash, bulletHoleGraphic;
    public CamShake camShake;
    public float camShakeMagnitude, camShakeDuration;
    public Text text;
    public Wall wall;
    public Ceiling ceiling;
    private int wallHitCount;
    private int ceilingHitCount;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    void Start()
    {
        wallHitCount = 0;
        ceilingHitCount = 0;
    }

    private void Update()
    {
        MyInput();
        text.text = bulletsLeft + " / " + magazineSize;
    }

    private void MyInput()
    {
        if (allowButtonHold)
            shooting = Input.GetKey(KeyCode.Mouse0);
        else
            shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
            Reload();

        if (readyToShoot && shooting && !reloading && magazineSize > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        _ = fpsCam.transform.forward + new Vector3(x, y, 0);

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);
            Target target = rayHit.transform.GetComponent<Target>();

            if (target != null)
            {
                target.TakeDamage(damage);

                if (rayHit.collider.CompareTag("Wall"))
                {
                    wallHitCount++;
                    if (wallHitCount == 1)
                    {
                        Debug.Log("Wall was hit once.");
                    }
                    else if (wallHitCount > 1)
                    {
                        Debug.Log("Wall was hit more than once.");
                    }
                }
                else if (rayHit.collider.CompareTag("Ceiling"))
                {
                    ceilingHitCount++;
                    if (ceilingHitCount == 1)
                    {
                        Debug.Log("Ceiling was hit once.");
                    }
                    else if (ceilingHitCount > 1)
                    {
                        Debug.Log("Ceiling was hit more than once.");
                    }
                }
            }
        }
        camShake.DoShake();

        Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
        Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        bulletsLeft--;
        bulletsShot--;

        Invoke(nameof(ResetShot), timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
        {
            Invoke(nameof(Shoot), timeBetweenShots);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke(nameof(ReloadFinished), reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}