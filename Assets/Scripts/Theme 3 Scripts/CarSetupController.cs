using UnityEngine;

public class CarSetupontroller : MonoBehaviour
{
    [SerializeField] GameObject checkFWD;
    [SerializeField] GameObject checkRWD;
    [SerializeField] GameObject checkAWD;
    [SerializeField] GameObject checkManual;
    [SerializeField] GameObject checkAutomatic;
    [SerializeField] GameObject setupPanel;
    [SerializeField] GameObject timePanel;

    [SerializeField] controller controller;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void FWDSelect()
    {
        controller.drive = controller.driveType.frontWheelDrive;
        checkFWD.SetActive(true);
        checkRWD.SetActive(false);
        checkAWD.SetActive(false);
    }

    public void RWDSelect()
    {
        controller.drive = controller.driveType.rearWheelDrive;
        checkFWD.SetActive(false);
        checkRWD.SetActive(true);
        checkAWD.SetActive(false);
    }

    public void AWDSelect()
    {
        controller.drive= controller.driveType.allWheelDrive;
        checkFWD.SetActive(false);
        checkRWD.SetActive(false);
        checkAWD.SetActive(true);
    }

    public void ManualSelect()
    {
        controller.isAutomatic = false;
        checkManual.SetActive(true);
        checkAutomatic.SetActive(false);
    }

    public void AutomaticSelect()
    {
        controller.isAutomatic = true;
        checkManual.SetActive(false);
        checkAutomatic.SetActive(true);
    }

    public void StartTimeTrial()
    {
        Time.timeScale = 1;
        setupPanel.SetActive(false);
        timePanel.SetActive(true);
    }
}