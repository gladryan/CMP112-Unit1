using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    // calls a waypoint with a specific index
    public Transform GetWaypoint(int waypointIndex)
    {
        return transform.GetChild(waypointIndex);
    }

    // calls the next waypoint index in the path
    public int GetNextWaypointIndex(int currentWaypointIndex)
    {
        // incrementing the waypoints
        int nextWaypointIndex = currentWaypointIndex + 1;

        // loops the waypoint back to the beginning
        if (nextWaypointIndex == transform.childCount)
        {
            nextWaypointIndex = 0;
        }

        return nextWaypointIndex;
    }
}