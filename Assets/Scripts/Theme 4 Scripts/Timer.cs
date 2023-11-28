using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private TextMeshProUGUI currentTimeText;
    [SerializeField] private TextMeshProUGUI bestTimeText;
    [SerializeField] private TextMeshProUGUI currentHealth;

    private float currentTime = 0;
    private float bestTime = 0;
    private bool startTimer = false;

    private void Start()
    {
        startTimer = true;
    }

    private void Update()
    {
        currentTimeText.text = "Time Survived: " + currentTime.ToString("F2");

        if (startTimer == true)
        {
            currentTime = currentTime + Time.deltaTime;
        }

        PlayerHealthCheck();
    }

    private void PlayerHealthCheck()
    {
        if (playerHealth.playerAlive == false)
        {
            startTimer = false;
            bestTime = currentTime;
            currentHealth.text = "0";
            bestTimeText.text = "You Survived For " + bestTime.ToString("F2") + " Seconds!";
        }
        else
        {
            currentHealth.text = "Health: " + Convert.ToString(playerHealth.health);
        }
    }
}