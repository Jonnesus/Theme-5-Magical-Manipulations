using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;

    private Rigidbody2D rb;

    System.Random random = new System.Random();

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.rotation = Quaternion.Euler(0,0,90);

        rb.velocity = Vector3.left * speed;
    }

    private void OnCollisionEnter2D(Collision2D hitInfo)
    { 
        PlayerHealth player = hitInfo.collider.GetComponent<PlayerHealth>();

        if (player != null)
            player.TakeDamage(random.Next(5,15));

        Destroy(gameObject);
    }
}