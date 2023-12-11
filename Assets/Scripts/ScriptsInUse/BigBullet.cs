using UnityEngine;

public class BigBullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rotateSpeed = 200f;

    private Rigidbody2D rb;
    private Transform target;

    System.Random random = new System.Random();

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Enemy").transform;
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
        EnemyTakeDamage enemy = hitInfo.collider.GetComponent<EnemyTakeDamage>();

        if (enemy != null)
            enemy.TakeDamage(random.Next(12, 25));

        if (hitInfo.gameObject.tag == "Button")
            Debug.Log("Button Hit");

        Destroy(gameObject);
    }
}