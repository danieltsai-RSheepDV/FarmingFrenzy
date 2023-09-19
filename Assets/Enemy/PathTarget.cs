using System;
using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(Seeker))]
public class PathTarget : MonoBehaviour
{
    //Constants
    private const float NextWaypointRadius = 0.2f;
    
    // Fields
    [NonSerialized] public GameObject mTarget;
    
    // Astar Variables
    private Path path;
    private int currentWaypoint = 0;
    private Seeker seeker;
    
    void Start()
    {
        seeker = GetComponent<Seeker>();
    }

    void Update()
    {
        if (mTarget == null) return;
        
        if (path == null) return;

        if (currentWaypoint >= path.vectorPath.Count) return;
            
        float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);

        if (distance < NextWaypointRadius)
        {
            currentWaypoint++;
        }
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(UpdatePath), 0f, 0.5f);
    }
    
    private void UpdatePath() 
    {
        if (mTarget == null) return;
        
        if (!seeker.IsDone()) return;
            
        seeker.StartPath(transform.position, mTarget.transform.position, p =>
        {
            path = p;
            currentWaypoint = 0;
        });
    }
}