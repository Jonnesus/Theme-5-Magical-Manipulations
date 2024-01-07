using UnityEngine;

public class EnemyBigBullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rotateSpeed = 200f;

    private Rigidbody2D rb;
    private Transform target;

    System.Random random = new System.Random();

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        transform.rotation = Quaternion.Euler(0, 0, 180);

    }

    private void FixedUpdate()
    {
        if (target == null)
            Destroy(gameObject);
        else
        {
            Vector2 direction = (Vector2)target.position - rb.position;
            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, transform.up).z;

            rb.angularVelocity = -rotateAmount * rotateSpeed;
            rb.velocity = transform.up * speed;
        }
    }

    void OnCollisionEnter2D(Collision2D hitInfo)
    {
        PlayerHealth player = hitInfo.collider.GetComponent<PlayerHealth>();

        if (player != null)
            player.TakeDamage(random.Next(12, 25));

        Destroy(gameObject);
    }
}