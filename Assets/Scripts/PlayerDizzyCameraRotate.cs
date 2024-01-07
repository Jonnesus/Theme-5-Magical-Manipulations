using System.Collections;
using UnityEngine;

public class PlayerDizzyCameraRotate : MonoBehaviour
{
    [SerializeField] private GameObject vCam;

    [Header("Small Hit")]
    [SerializeField] private float turnSpeed = 20f;
    [SerializeField] private float rotateDirection = -1f;
    [SerializeField] private float rotationTime = 0.25f;
    [Header("Big Hit")]
    [SerializeField] private float turnSpeedBig = 40f;
    [SerializeField] private float rotateDirectionBig = 1f;
    [SerializeField] private float rotationTimeBig = 0.5f;

    private bool turnCamera = false;
    private bool turnCameraBig = false;

    private void Update()
    {
        if (turnCamera)
        {
            vCam.transform.eulerAngles += new Vector3(0, 0, rotateDirection * turnSpeed * Time.deltaTime);
        }
        else if (turnCameraBig)
        {
            vCam.transform.eulerAngles += new Vector3(0, 0, rotateDirectionBig * turnSpeedBig * Time.deltaTime);
        }
    }

    IEnumerator rotationTimer()
    {
        yield return new WaitForSeconds(rotationTime);
        turnCamera = false;
        yield return false;
    }

    IEnumerator rotationTimerBig()
    {
        yield return new WaitForSeconds(rotationTimeBig);
        turnCameraBig = false;
        yield return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet" && !turnCamera)
        {
            turnCamera = true;
            StartCoroutine(rotationTimer());
        }
        else if (collision.gameObject.tag == "EnemyBigBullet" && !turnCameraBig)
        {
            turnCameraBig = true;
            StartCoroutine(rotationTimerBig());
        }
    }
}