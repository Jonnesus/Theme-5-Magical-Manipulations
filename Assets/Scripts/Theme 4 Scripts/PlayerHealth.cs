using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public bool playerAlive = true;

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private GameObject inGameMenuPanel;
    [SerializeField] private GameObject deathMenuPanel;
    [SerializeField] private GameObject restartButton;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
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