using UnityEngine;

public class BigBullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rotateSpeed = 200f;

    private Rigidbody2D rb;
    private EnemyPlayerDetection target;

    System.Random random = new System.Random();

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        DetectTarget();
        MoveBullet();
    }

    private void DetectTarget()
    {
        float distancetoTarget = Mathf.Infinity;
        EnemyPlayerDetection[] allEnemies = GameObject.FindObjectsByType<EnemyPlayerDetection>(FindObjectsSortMode.None);

        foreach (EnemyPlayerDetection currentEnemy in allEnemies)
        {
            float distancetoEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;

            if (distancetoEnemy < distancetoTarget)
            {
                distancetoTarget = distancetoEnemy;
                target = currentEnemy;
            }
        }
    }

    private void MoveBullet()
    {
        Vector2 direction = (Vector2)target.transform.position - rb.position;
        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotateAmount * rotateSpeed;
        rb.velocity = transform.up * speed;
    }

    void OnCollisionEnter2D(Collision2D hitInfo)
    {
        EnemyTakeDamage enemy = hitInfo.collider.GetComponent<EnemyTakeDamage>();

        if (enemy != null)
            enemy.TakeDamage(random.Next(12, 25));

        Destroy(gameObject);
    }
}