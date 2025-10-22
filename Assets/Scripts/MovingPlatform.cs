using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private WaypointPath _waypointPath;

    // platform movement speed
    [SerializeField]
    private float _speed;

    // platform movement target index
    private int _targetWaypointIndex;

    //smooths out the platform movement
    private Transform _previousWaypoint;
    private Transform _targetWaypoint;

    // time it takes to get to the target waypoint
    private float _timeToWaypoint;

    // the time elapsed
    private float _elapsedTime;

    void Start()
    {
        // sets the previous waypooint to the first waypoint in the path and sets the target as the second waypoint in the path
        TargetNextWaypoint();
    }

    void FixedUpdate()
    {
        // timekeeping
        _elapsedTime += Time.deltaTime;

        // working out the percentage of the journey completed by dividing the elapsed time by the time it takes to get to the waypoint
        float elapsedPercentage = _elapsedTime / _timeToWaypoint;

        // adds an ease-in and ease-out curve to the patform's path
        elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage);

        // updating the position using lerp. It changes the position of the platform based on the percentage of the journey that has elapsed
        transform.position = Vector3.Lerp(_previousWaypoint.position, _targetWaypoint.position, elapsedPercentage);

        // adds the rotation to the platform
        transform.rotation = Quaternion.Lerp(_previousWaypoint.rotation, _targetWaypoint.rotation, elapsedPercentage);

        // moves onto the next waypoint once the journey is complete
        if (elapsedPercentage >= 1)
        {
            TargetNextWaypoint();
        }
    }

    // updates the next waypoint in the path
    private void TargetNextWaypoint()
    {
        // sets previous waypoint to the current target index
        _previousWaypoint = _waypointPath.GetWaypoint(_targetWaypointIndex);

        // sets the current index to the next index in the path
        _targetWaypointIndex = _waypointPath.GetNextWaypointIndex(_targetWaypointIndex);

        // sets the target waypoint at the new target index
        _targetWaypoint = _waypointPath.GetWaypoint(_targetWaypointIndex);

        // set the timing
        _elapsedTime = 0;

        // distance between the previous and target waypoints
        float distanceToWaypoint = Vector3.Distance(_previousWaypoint.position, _targetWaypoint.position);

        // calculates the time to get there by dividing the distance by the speed
        _timeToWaypoint = distanceToWaypoint / _speed;
    }
    // this is called when an object enters a trigger
    private void OnTriggerEnter(Collider other)
    {
        // parent of the object that has entered the trigger is set to the transform of the platform
        other.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.SetParent(null);
    }
}