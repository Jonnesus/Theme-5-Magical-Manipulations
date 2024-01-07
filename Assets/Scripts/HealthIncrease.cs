using UnityEngine;

public class HealthIncrease : MonoBehaviour
{
    public PlayerHealth playerhealth;

    [SerializeField] private float healthIncrease;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerhealth.health += healthIncrease;
            GameObject.Destroy(gameObject);
        }
    }
}