using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float force = 5f;
    [SerializeField] private GameObject player;

    private Rigidbody2D rb;

    System.Random random = new System.Random();

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("PlayerFirePoint");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation + 90);
    }

    private void OnCollisionEnter2D(Collision2D hitInfo)
    { 
        PlayerHealth player = hitInfo.collider.GetComponent<PlayerHealth>();

        if (player != null)
            player.TakeDamage(random.Next(5,15));

        Destroy(gameObject);
    }
}