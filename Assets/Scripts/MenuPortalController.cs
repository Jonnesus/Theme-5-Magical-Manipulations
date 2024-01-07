using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPortalController : MonoBehaviour
{
    [SerializeField] private string menuScene = "StartScene";
    [SerializeField] private string playScene = "Overworld";

    public GameObject portalMenuPanel;

    public void Resume()
    {
        portalMenuPanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }

    public void Restart()
    {
        portalMenuPanel.SetActive(false);
        SceneManager.LoadScene(playScene);
    }

    public void QuitToMainMenu()
    {
        portalMenuPanel.SetActive(false);
        SceneManager.LoadScene(menuScene);
    }
}