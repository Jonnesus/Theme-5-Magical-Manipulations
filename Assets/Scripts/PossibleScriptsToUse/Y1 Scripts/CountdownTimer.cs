using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private double startingTime;
    public double currentTime;

    [SerializeField] private TextMeshProUGUI countdownText;

    [SerializeField] private GameObject noTimePanel;

    [SerializeField] string playScene = "Overworld";
    [SerializeField] string mainMenuScene = "StartScene";

    void Start()
    {
        currentTime = startingTime;
    }

    void Update()
    {
        currentTime -= Time.deltaTime;
        countdownText.text = currentTime.ToString("00.00");

        if (currentTime <= 0)
        {
            Time.timeScale = 0f;
            noTimePanel.SetActive(true);

            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(playScene);
                Time.timeScale = 1f;
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(mainMenuScene);
                noTimePanel.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 1f;
            }
        }
    }
}