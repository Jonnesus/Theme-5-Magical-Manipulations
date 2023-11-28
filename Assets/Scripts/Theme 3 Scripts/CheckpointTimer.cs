using TMPro;
using UnityEngine;

public class CheckpointTimer : MonoBehaviour
{
    public TextMeshProUGUI currentTimeText;
    public TextMeshProUGUI bestTimeText;

    private float currentTime = 0;
    private float bestTime = 0;

    private bool startTimer = false;
    private bool checkpoint1 = false;
    private bool checkpoint2 = false;

    private void Update()
    {
        currentTimeText.text = "Time: " + currentTime.ToString("F2");

        if (startTimer == true)
        {
            currentTime = currentTime + Time.deltaTime;          
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "StartFinish")
        {
            if (startTimer == false)
            {
                startTimer = true;
                currentTime = 0;
                checkpoint1 = false;
                checkpoint2 = false;
            }

            if (checkpoint1 == true && checkpoint2 == true)
            {
                startTimer = false;

                if (bestTime == 0)
                {
                    bestTime = currentTime;
                }
                if (currentTime < bestTime)
                {
                    bestTime = currentTime;
                }

                bestTimeText.text = "Best Time: " + bestTime.ToString("F2");
            }
        }

        if (other.gameObject.name == "Checkpoint1")
        {
            checkpoint1 = true;
        }

        if (other.gameObject.name == "Checkpoint2")
        {
            checkpoint2 = true;
        }
    }
}