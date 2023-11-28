using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] string menuScene = "StartScene";
    [SerializeField] string playScene = "Overworld";

    [SerializeField] GameObject optionsMenuPanel;
    [SerializeField] GameObject optionsCloseButton;
    [SerializeField] GameObject optionsOpenButton;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OptionsMenuClose()
    {
        EventSystem.current.SetSelectedGameObject(optionsOpenButton);
        optionsMenuPanel.SetActive(false);
    }

    public void OptionsMenuOpen()
    {
        optionsMenuPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(optionsCloseButton);
    }

    public void StartGame()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene(playScene);
        SceneManager.UnloadSceneAsync(menuScene);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT GAME");
        Application.Quit();
    }
}