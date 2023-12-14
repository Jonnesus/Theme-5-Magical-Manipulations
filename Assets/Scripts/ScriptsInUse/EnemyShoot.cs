using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private float fireRateSmall = 15f;
    [SerializeField] private float maxRange = 10f;

    [SerializeField] private GameObject firePoint;
    [SerializeField] private GameObject bulletPrefab;

    private float nextFireSmall = 0f;
    private float distance;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < maxRange && Time.time >= nextFireSmall)
        {
            nextFireSmall = Time.time + 1f / fireRateSmall;
            Shoot();
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
    }
}