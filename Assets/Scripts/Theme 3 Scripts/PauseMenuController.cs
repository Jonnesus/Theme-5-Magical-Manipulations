using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private string menuScene = "StartScene";
    [SerializeField] private string playScene = "Overworld";

    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject resumeButton;

    private inputManager IM;

    private void Start()
    {
        IM = GetComponent<inputManager>();
    }

    private void Update()
    {
        PauseActivate();
    }

    private void PauseActivate()
    {
        if (Input.GetButton("Cancel"))
        {
            pauseMenuPanel.SetActive(true);
            Time.timeScale = 0;
            EventSystem.current.SetSelectedGameObject(resumeButton);
        }
    }
    public void Resume()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        pauseMenuPanel.SetActive(false);
        SceneManager.LoadScene(playScene);
    }

    public void QuitToMainMenu()
    {
        pauseMenuPanel.SetActive(false);
        SceneManager.LoadScene(menuScene);
        SceneManager.UnloadSceneAsync(playScene);
    }
}