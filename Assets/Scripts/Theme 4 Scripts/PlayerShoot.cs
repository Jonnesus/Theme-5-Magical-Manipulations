using UnityEngine;

[RequireComponent(typeof(InputManager))]

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float fireRate = 15f;
    [SerializeField] private float range = 100f;

    private float nextFire = 0f;

    [SerializeField] private Camera fpsCam;
    [SerializeField] private ParticleSystem muzzleFlash;

    private InputManager IM;

    private void Start()
    {
        IM = GetComponent<InputManager>();
    }

    private void Update()
    {
        if (IM.fire == true && Time.time >= nextFire)
        {
            nextFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            EnemyTakeDamage target = hit.transform.GetComponent<EnemyTakeDamage>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
}