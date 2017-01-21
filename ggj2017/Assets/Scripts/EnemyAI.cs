using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour {

	private Transform tr;
    public Transform targetPlayer;
	private Seeker seeker;
	private CharacterController controller;
	public float speed = 5;
	// The calculated path
    public Path path;
    // The max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistance = 0;
    // The waypoint we are currently moving towards
    private int currentWaypoint = 0;
    // How often to recalculate the path (in seconds)
    public float repathRate = 0.1f;
    private float lastRepath = -9999;

    public void Start ()
	{
		tr = GetComponent<Transform>();
		targetPlayer = GameObject.FindWithTag("Player").transform;
        //Get a reference to the Seeker component we added earlier
        seeker = GetComponent<Seeker>();
    }

    public void OnPathComplete (Path p)
	{
		if (!p.error) {
            path = p;
            // Reset the waypoint counter so that we start to move towards the first point in the path
            currentWaypoint = 0;
        }
    }

	void FixedUpdate()
	{
		targetPlayer = GameObject.FindWithTag("Player").transform;
		//Debug.Log ("player pos "+targetPlayer.position);
		
		if (Time.time - lastRepath > repathRate && seeker.IsDone())
		{
            lastRepath = Time.time+ Random.value*repathRate*0.5f;
            // Start a new path to the targetPosition, call the the OnPathComplete function
            // when the path has been calculated (which may take a few frames depending on the complexity)
            seeker.StartPath(tr.position, targetPlayer.position, OnPathComplete);
        }

        if (path == null)
		{
            // We have no path to follow yet, so don't do anything
            return;
        }

        if (currentWaypoint > path.vectorPath.Count) return;

        if (currentWaypoint == path.vectorPath.Count)
		{
            Debug.Log("End Of Path Reached");
            currentWaypoint++;
            return;
        }
        // Direction to the next waypoint
        Vector3 dir = (path.vectorPath[currentWaypoint]-transform.position).normalized;
        //dir *= speed;
		float step = speed * Time.deltaTime;
        tr.position = Vector3.MoveTowards(tr.position, dir, step);
        // The commented line is equivalent to the one below, but the one that is used
        // is slightly faster since it does not have to calculate a square root
        //if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
        if ((transform.position-path.vectorPath[currentWaypoint]).sqrMagnitude < nextWaypointDistance*nextWaypointDistance)
		{
            currentWaypoint++;
            return;
        }
	}
}
