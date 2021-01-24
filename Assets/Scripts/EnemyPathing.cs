using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class EnemyPathing : MonoBehaviour
{
    [SerializeField][Tooltip("Choose the Scriptable Object to get the waypoints.")] WaveConfig waveConfig;
    
    List<Transform> waypoints;
    int waypointIndex = 0;
    float moveSpeed = 2f;

    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        moveSpeed = waveConfig.GetMoveSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex];
            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition.position, movementThisFrame);
            if (transform.position == targetPosition.position)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
