using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    public bool isDead = false;

    [SerializeField] private float health = 50f;

    [Header("Damage SFX")]
    [SerializeField] private AudioClip[] damageSFX;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(float amount)
    {
        audioSource.PlayOneShot(damageSFX[Random.Range(0, damageSFX.Length)]);
        health -= amount;

        if (health <= 0)
        {
            isDead = true;
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy Killed");
        Destroy(gameObject);
    }
}