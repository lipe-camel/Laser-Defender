using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;    
    List<Transform> waypoints;
    int waypointIndex = 0;

    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig waveConfigToFollow) //to push the WaveConfig from a SerializeField from another script
    {
        this.waveConfig = waveConfigToFollow;
    }

    private void Move()
    {
        if (waypointIndex <= waypoints.Count - 1) //if hasn't finished the path
        {
            var targetPosition = waypoints[waypointIndex];
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime; //speed of movement
            transform.position = Vector2.MoveTowards(transform.position, targetPosition.position, movementThisFrame); //make it move
            if (transform.position == targetPosition.position) //move to next waypoint
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
