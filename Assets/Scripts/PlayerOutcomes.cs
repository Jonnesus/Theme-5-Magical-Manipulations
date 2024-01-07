using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerOutcomes : MonoBehaviour
{
    public bool portalReached = false;

    [SerializeField] private MenuPortalController portalController;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private GameObject inGameMenuPanel;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject bestTimeText;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DeathObject")
        {
            PlayerHealth player = collision.collider.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(999);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("LevelPortal"))
        {
            portalReached = true;
            inGameMenuPanel.SetActive(false);
            portalController.portalMenuPanel.SetActive(true);
            bestTimeText.SetActive(true);
            EventSystem.current.SetSelectedGameObject(restartButton);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
    }
}