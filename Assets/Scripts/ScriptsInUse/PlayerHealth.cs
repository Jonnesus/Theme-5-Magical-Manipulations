using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public bool playerAlive = true;

    [SerializeField] private float maxHealth = 100;
    [SerializeField] private GameObject inGameMenuPanel;
    [SerializeField] private GameObject deathMenuPanel;
    [SerializeField] private GameObject restartButton;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("Player Dead");
            playerAlive = false;
            inGameMenuPanel.SetActive(false);
            deathMenuPanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(restartButton);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
    }
}