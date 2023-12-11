using UnityEngine;

[RequireComponent(typeof(InputManager))]

public class PlayerPause : MonoBehaviour
{
    [SerializeField] private MenuPauseController pauseController;
    [SerializeField] private PlayerHealth playerHealth;

    private InputManager IM;

    private void Start()
    {
        IM = GetComponent<InputManager>();
    }

    private void Update()
    {
        PauseActivate();
    }

    private void PauseActivate()
    {
        if (IM.cancel && playerHealth.playerAlive)
        {
            pauseController.pauseMenuPanel.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
    }
}