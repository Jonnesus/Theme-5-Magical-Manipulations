using UnityEngine;

public class TimeIncrease : MonoBehaviour
{
    public CountdownTimer countdownTimer;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            countdownTimer.currentTime += 5f;
            GameObject.Destroy(gameObject);
        }
    }
}