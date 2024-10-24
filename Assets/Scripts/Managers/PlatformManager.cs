using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Unity Custom Gizmo Lesson Script
// Joseph, Charlie

public class PlatformManager : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints = new Transform[3];
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private bool showGizmo = true;

    private int currentWayPoint = 0;
    private float distance = 0.1f;

    // Platform
    private void Update()
    {
        // Get current waypoint
        Vector3 targetPosition = waypoints[currentWayPoint].position;

        // Move towards waypoint
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Move towards the next waypoint
        if (Vector3.Distance(transform.position, targetPosition) < distance)
        {
            currentWayPoint = (currentWayPoint + 1) % waypoints.Length;
        }
    }

    // Gizmo
    private void OnDrawGizmos()
    {
        if (!showGizmo || waypoints == null || waypoints.Length != 3)
            return;

        // Draw path
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (waypoints[i] != null)
            {
                Gizmos.color = (i == currentWayPoint) ? Color.green : Color.red;

                // Draw spheres for waypoints
                Gizmos.DrawWireSphere(waypoints[i].position, 0.25f);

                // Waypoint number stuff
                #if UNITY_EDITOR
                Vector3 textPosition = waypoints[i].position + Vector3.up * 0.5f;
                Handles.Label(textPosition, (i + 1).ToString(), new GUIStyle()
                {
                    normal = new GUIStyleState() {textColor = (i == currentWayPoint) ? Color.green : Color.white},
                    fontSize = 20,
                    fontStyle = FontStyle.Bold,
                    alignment = TextAnchor.MiddleCenter
                });
                #endif

                // Draw lines between waypoints
                if (i < waypoints.Length - 1 && waypoints[i + 1] != null)
                {
                    Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
                }
            }
        }

        // Connect last point to first point
        if (waypoints[0] != null && waypoints[waypoints.Length - 1] != null)
        {
            Gizmos.DrawLine(waypoints[waypoints.Length - 1].position, waypoints[0].position);
        }

        // Draw platform pos
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.25f);
    }
}
