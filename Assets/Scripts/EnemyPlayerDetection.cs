using System.Collections;
using UnityEngine;

public class EnemyPlayerDetection : MonoBehaviour
{
    public bool playerDetected {get; private set;}
    public Vector2 directionToTarget;

    [Header("Box Parameters")]
    [SerializeField] private Transform detectorOrigin;
    public Vector2 detectorSize = Vector2.one, detectorOriginOffset = Vector2.one;

    [SerializeField] private float detectionDelay = 0.3f;
    [SerializeField] private LayerMask detectorLayerMask;

    [Header("Gizmo Parameters")]
    [SerializeField] private Color idleGizmoColour = Color.green, activeGizmoColour = Color.red;
    [SerializeField] private bool showGizmos = true;

    private GameObject target;

    public GameObject Target
    {
        get => target;
        private set
        {
            target = value;
            playerDetected = target != null;
        }
    }

    private void Start()
    {
        StartCoroutine(DetectionCoroutine());
    }

    private void Update()
    {
        if (playerDetected)
            directionToTarget = target.transform.position - detectorOrigin.position;

        if (directionToTarget.x > 0 && playerDetected)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (playerDetected)
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(detectionDelay);
        PerformDetection();
        StartCoroutine(DetectionCoroutine());
    }

    private void PerformDetection()
    {
        Collider2D collider = Physics2D.OverlapBox((Vector2)detectorOrigin.position + detectorOriginOffset, detectorSize, 0, detectorLayerMask);

        if (collider != null)
            Target = collider.gameObject;
        else
            Target = null;
    }

    private void OnDrawGizmos()
    {
        if (showGizmos && detectorOrigin != null)
        {
            Gizmos.color = idleGizmoColour;

            if (playerDetected)
                Gizmos.color = activeGizmoColour;

            Gizmos.DrawCube((Vector2)detectorOrigin.position + detectorOriginOffset, detectorSize);
        }
    }
}