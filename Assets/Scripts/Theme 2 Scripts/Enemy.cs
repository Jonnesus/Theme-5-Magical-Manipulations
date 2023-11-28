using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;

    public void TakeDamage (int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die ()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D _colInfo)
    {
        PlayerController _player = _colInfo.collider.GetComponent<PlayerController>();
        PlayerController2 _player2 = _colInfo.collider.GetComponent<PlayerController2>();
        if (_player != null)
        {
            _player.DamagePlayer(40);
            TakeDamage(9999999);
        }
        if (_player2 != null)
        {
            _player2.DamagePlayer2(40);
            TakeDamage(9999999);
        }
    }
}