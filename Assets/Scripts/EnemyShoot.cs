using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private float fireRateSmall = 15f, maxRange;

    [SerializeField] private GameObject firePoint, bulletPrefab;

    private float nextFireSmall = 0f, distance;

    private GameObject player;

    private EnemyPlayerDetection enemyPlayerDetection;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyPlayerDetection = GetComponent<EnemyPlayerDetection>();

        maxRange = enemyPlayerDetection.detectorSize.x / 2f + 0.55f;
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
        Instantiate(bulletPrefab, firePoint.transform.position, Quaternion.identity);
    }
}