using UnityEngine;

public class WeaponK : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    float fireCooldown;
    public float fireRate;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (Time.time > fireCooldown)
        {
            fireCooldown = Time.time + fireRate;
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }
}