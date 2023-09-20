using System;
using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(Seeker))]
public class PathTarget : MonoBehaviour
{
    //Constants
    private const float NextWaypointRadius = 1f;
    
    // Fields
    public GameObject mTarget
    {
        get
        {
            return target;
        }
        set
        {
            target = value;
            UpdatePath();
        }
    }
    private GameObject target;
    
    // Astar Variables
    private Path path;
    private int currentWaypoint = 0;
    private Seeker seeker;
    
    void Awake()
    {
        seeker = GetComponent<Seeker>();
    }

    void Update()
    {
        if (mTarget == null) return;

        if (path == null) return;

        if (currentWaypoint >= path.vectorPath.Count) return;
            
        float distance = Vector2.Distance(transform.position, GetCurrentWayPoint());

        if (distance < NextWaypointRadius)
        {
            UpdatePath();
        }
    }

    private void UpdatePath() 
    {
        if (mTarget == null) return;
        
        if (!seeker.IsDone()) return;
            
        seeker.StartPath(transform.position, mTarget.transform.position, p =>
        {
            path = p;
            currentWaypoint = 1;
        });
    }
    
    public Vector3 GetCurrentWayPoint()
    {
        if (path == null)
        {
            return transform.position;
        }
        return path.vectorPath[currentWaypoint];
    }
}