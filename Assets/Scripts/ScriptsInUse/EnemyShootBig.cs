using UnityEngine;

public class EnemyShootBig : MonoBehaviour
{
    [SerializeField] private float fireRateBig = 5f;
    [SerializeField] private float maxRange = 10;

    [SerializeField] private GameObject firePoint;
    [SerializeField] private GameObject bigBulletPrefab;

    private float nextFireBig = 0f;
    private float distance;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < maxRange && Time.time >= nextFireBig)
        {
            nextFireBig = Time.time + 1f / fireRateBig;
            ShootBig();
        }
    }

    private void ShootBig()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null)
            return;
        else
            Instantiate(bigBulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
    }
}