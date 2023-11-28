using UnityEngine;

public class PlayerSwinging : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform gunTip;
    [SerializeField] private Transform cameraObject;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask swingSurface;
    [SerializeField] private PlayerMovement playerMovement;

    [Header("Swinging")]
    [SerializeField] private float maxSwingDistance;
    private Vector3 swingPoint;
    private SpringJoint joint;

    [Header("OdmGear")]
    [SerializeField] private Transform orientation;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float horizontalThrustForce;
    [SerializeField] private float forwardThrustForce;
    [SerializeField] private float extendCableSpeed;

    [Header("Prediction")]
    [SerializeField] private RaycastHit predictionHit;
    [SerializeField] private float predictionSphereCastRadius;
    [SerializeField] private Transform predictionPoint;

    [Header("Input")]
    [SerializeField] private KeyCode swingKey = KeyCode.Mouse0;

    private void Start()
    {
        StopSwing();
    }

    private void Update()
    {
        if (Input.GetKeyDown(swingKey) && !playerMovement.grounded)
            StartSwing();

        if (Input.GetKeyUp(swingKey))
            StopSwing();
        
        CheckForSwingPoints();

        if (playerMovement.grounded)
            predictionPoint.gameObject.SetActive(false);

        if (joint != null)
            OdmGearMovement();
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    private void CheckForSwingPoints()
    {
        if (joint != null)
            return;

        RaycastHit sphereCastHit;
        Physics.SphereCast(cameraObject.position, predictionSphereCastRadius, cameraObject.forward,
                            out sphereCastHit, maxSwingDistance, swingSurface);

        RaycastHit raycastHit;
        Physics.Raycast(cameraObject.position, cameraObject.forward,
                            out raycastHit, maxSwingDistance, swingSurface);

        Vector3 realHitPoint;

        //Option 1 - Direct Hit
        if (raycastHit.point != Vector3.zero)
        {
            predictionPoint.gameObject.SetActive(true);
            realHitPoint = raycastHit.point;
        }
        //Option 2 - Indirect (predicted) Hit
        else if (sphereCastHit.point != Vector3.zero)
        {
            predictionPoint.gameObject.SetActive(true);
            realHitPoint = sphereCastHit.point;
        }
        //Option 3 - Miss
        else
        {
            predictionPoint.gameObject.SetActive(false);
            realHitPoint = Vector3.zero;
        }

        //realHitPoint found
        if (realHitPoint != Vector3.zero)
        {
            predictionPoint.gameObject.SetActive(true);
            predictionPoint.position = realHitPoint;
        }
        //realHitPoint not found
        else
        {
            predictionPoint.gameObject.SetActive(false);
        }

        predictionHit = raycastHit.point == Vector3.zero ? sphereCastHit : raycastHit;
    }

    private void StartSwing()
    {
        lineRenderer.enabled = true;
        //Return if predictionHit not found
        if (predictionHit.point == Vector3.zero)
            return;

        //Deactivate active grapple
        if (GetComponent<PlayerGrappling>() != null)
            GetComponent<PlayerGrappling>().StopGrapple();

        playerMovement.ResetRestrictions();

        playerMovement.swinging = true;
        predictionPoint.gameObject.SetActive(false);

        swingPoint = predictionHit.point;
        joint = player.gameObject.AddComponent<SpringJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = swingPoint;

        float distanceFromPoint = Vector3.Distance(player.position, swingPoint);

        //Distance grapple will try to keep from grapple point
        joint.maxDistance = distanceFromPoint * 0.8f;
        joint.minDistance = distanceFromPoint * 0.25f;

        joint.spring = 4.5f;
        joint.damper = 7f;
        joint.massScale = 4.5f;

        lineRenderer.positionCount = 2;
        currentGrapplePosition = gunTip.position;
    }

    public void StopSwing()
    {
        playerMovement.swinging = false;

        lineRenderer.positionCount = 0;
        lineRenderer.enabled = false;

        Destroy(joint);
    }

    private void OdmGearMovement()
    {
        //Right swing movement
        if (Input.GetKey(KeyCode.D))
            rb.AddForce(orientation.right * horizontalThrustForce * Time.deltaTime);

        //Left swing movement
        if (Input.GetKey(KeyCode.A))
            rb.AddForce(-orientation.right * horizontalThrustForce * Time.deltaTime);

        //Forward swing movement
        if (Input.GetKey(KeyCode.W)) rb.AddForce(orientation.forward * horizontalThrustForce * Time.deltaTime);

        //Shortens cable (move up on swing)
        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 directionToPoint = swingPoint - transform.position;
            rb.AddForce(directionToPoint.normalized * forwardThrustForce * Time.deltaTime);

            float distanceFromPoint = Vector3.Distance(transform.position, swingPoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;
        }
        //Extends cable (move down on swing)
        if (Input.GetKey(KeyCode.S))
        {
            float extendedDistanceFromPoint = Vector3.Distance(transform.position, swingPoint) + extendCableSpeed;

            joint.maxDistance = extendedDistanceFromPoint * 0.8f;
            joint.minDistance = extendedDistanceFromPoint * 0.25f;
        }
    }

    private Vector3 currentGrapplePosition;

    private void DrawRope()
    {
        //If not grappling, don't draw rope
        if (!joint)
            return;

        currentGrapplePosition =
            Vector3.Lerp(currentGrapplePosition, swingPoint, Time.deltaTime * 8f);

        lineRenderer.SetPosition(0, gunTip.position);
        lineRenderer.SetPosition(1, currentGrapplePosition);
    }
}