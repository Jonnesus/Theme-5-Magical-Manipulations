using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    private Rigidbody2D rb;

    System.Random random = new System.Random();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        transform.rotation = Quaternion.Euler(0,0,90);
    }

    void OnCollisionEnter2D(Collision2D hitInfo)
    {
        Enemy enemy = hitInfo.collider.GetComponent<Enemy>();

        if (enemy != null)
            enemy.TakeDamage(random.Next(5,15));

        if (hitInfo.gameObject.tag == "Button")
            Debug.Log("Button Hit");

        Destroy(gameObject);
    }
}