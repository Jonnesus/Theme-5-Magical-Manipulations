using UnityEngine;

public class DeathWallMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject path;
    [SerializeField] private Transform[] wayPoints;
    
    private Vector3 targetPosition;
    private int pointIndex, pointCount, direction = 1;

    private void Awake()
    {
        wayPoints = new Transform[path.transform.childCount];
        for (int i = 0; i < path.gameObject.transform.childCount; i++)
        {
            wayPoints[i] = path.transform.GetChild(i).gameObject.transform;
        }
    }

    private void Start()
    {
        pointCount = wayPoints.Length;
        pointIndex = 1;
        targetPosition = wayPoints[pointIndex].transform.position;
    }

    private void Update()
    {
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (transform.position == targetPosition )
        {
            NextPoint();
        }
    }
    void NextPoint()
    {
        if (pointIndex == pointCount - 1) // last point, set to -1 for repeating movement, 0 for one path
        {
            direction = 0;
        }

        if (pointIndex == 0) // first point, repeat loop
        {
            direction = 1;
        }

        pointIndex += direction;
        targetPosition = wayPoints[pointIndex].transform.position;
    }
}