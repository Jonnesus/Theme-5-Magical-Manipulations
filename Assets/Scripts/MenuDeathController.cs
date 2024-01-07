using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuDeathController : MonoBehaviour
{
    public GameObject deathMenuPanel;

    [SerializeField] private string menuScene = "StartScene";
    [SerializeField] private string playScene = "Overworld";

    public void Restart()
    {
        deathMenuPanel.SetActive(false);
        SceneManager.LoadScene(playScene);
    }

    public void QuitToMainMenu()
    {
        deathMenuPanel.SetActive(false);
        SceneManager.LoadScene(menuScene);
    }
}