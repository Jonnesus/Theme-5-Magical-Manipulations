using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField] private GameObject portal;

    private Transform target;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        target = GameObject.FindGameObjectWithTag("Enemy").transform;
    }

    private void Update()
    {
        TargetCheck();
    }

    private void TargetCheck()
    {
        if (target == null)
        {
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
                portal.SetActive(true);
            else
                target = GameObject.FindGameObjectWithTag("Enemy").transform;
        }
    }
}