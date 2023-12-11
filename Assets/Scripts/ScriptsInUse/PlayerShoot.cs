using UnityEngine;

[RequireComponent(typeof(InputManager))]

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private float fireRateSmall = 15f;
    [SerializeField] private float fireRateBig = 5f;

    [SerializeField] private GameObject firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bigBulletPrefab;

    private float nextFireSmall = 0f;
    private float nextFireBig = 0f;
    private InputManager IM;

    private void Start()
    {
        IM = GetComponent<InputManager>();
    }

    private void Update()
    {
        if (IM.fire && Time.time >= nextFireSmall)
        {
            nextFireSmall = Time.time + 1f / fireRateSmall;
            Shoot();
        }

        if (IM.fire2 && Time.time >= nextFireBig)
        {
            nextFireBig = Time.time + 1f / fireRateBig;
            ShootBig();
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
    }

    private void ShootBig()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") == null)
            return;
        else
            Instantiate(bigBulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
    }
}