using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerOutcomesEDIT : MonoBehaviour
{
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private GameObject portalPanel;
    [SerializeField] private LayerMask deathLayer;

    private bool gameFreeze = false;

    [SerializeField] string playScene = "Overworld";
    [SerializeField] string mainMenuScene = "StartScene";

    private void Update()
    {
        if (gameFreeze == true && Input.GetButtonDown("Submit"))
        {
            SceneManager.LoadScene(playScene);
            deathPanel.SetActive(false);
            gameFreeze= false;
            Time.timeScale = 1f;
        }
        else if (gameFreeze == true && Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene(mainMenuScene);
            deathPanel.SetActive(false);
            gameFreeze= false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 1f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DeathObject")
        {
            Debug.Log("Death");
            Time.timeScale = 0f;
            deathPanel.SetActive(true);
            gameFreeze= true;
        }

        if (collision.gameObject.CompareTag("LevelPortal"))
        {
            Time.timeScale = 0f;
            portalPanel.SetActive(true);
            gameFreeze = true;
        }
    }
}