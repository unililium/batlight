using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour {

    private Transform targetPlayer;
	private Seeker seeker;
    [Header("Enemy speed"), Range(0f, 50f)]
	public float speed = 5f;
	// The calculated path
    public Path path;
    // The max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistance = 3;
    // The waypoint we are currently moving towards
    private int currentWaypoint = 0;
    // How often to recalculate the path (in seconds)
    public float repathRate = 0.1f;
    private float lastRepath = -9999;

    private Vector3 target;
    public Transform[] dummyTr;
    private int indexDtr = 0;
    private bool canChase = true;
    [Header("Chase cooldown after sonar use is:"), Range(0f, 20f)]
    public float chaseCooldownAfterSonar = 5f;
    [Header("Enemy chases the player for x seconds:"), Range(0f, 20f)]
    public float chaseForSeconds = 5f;
    private float nextChase;
    private bool chasing = false;
    private float distanceToPlayer;
    [Header("Enemy chases only if distance from player is less than x when she uses sonar:"), Range(0f, 200f)]
    public float triggerDistance;
    private int rotCounter = 4;

    public void Start()
	{
		targetPlayer = GameObject.FindWithTag("Player").transform;
        //Get a reference to the Seeker component we added earlier
        seeker = GetComponent<Seeker>();
        EventManager.StartListening ("Sonar", ChasePlayer);
    }

    public void OnPathComplete(Path p)
	{
		if (!p.error) {
            path = p;
            // Reset the waypoint counter so that we start to move towards the first point in the path
            currentWaypoint = 0;
        }
    }

	void FixedUpdate()
	{
        if(chasing)
        {
            targetPlayer = GameObject.FindWithTag("Player").transform;
            target = targetPlayer.position;
            StartCoroutine(ChaseForSeconds());
        }
        else
        {
            target = dummyTr[indexDtr].position;
        }

		if (Time.time - lastRepath > repathRate && seeker.IsDone())
		{
            lastRepath = Time.time+ Random.value*repathRate*0.5f;
            // Start a new path to the targetPosition, call the the OnPathComplete function
            // when the path has been calculated (which may take a few frames depending on the complexity)
            seeker.StartPath(transform.position, target, OnPathComplete);
        }

        if (path == null)
		{
            // We have no path to follow yet, so don't do anything
            return;
        }

        if (currentWaypoint > path.vectorPath.Count) return;

        if (currentWaypoint == path.vectorPath.Count)
		{
            //Debug.Log("End Of Path Reached");
            if(!chasing)
            {
                indexDtr = indexDtr != dummyTr.Length - 1 ? indexDtr + 1 : 0;
                target = dummyTr[indexDtr].position;
                seeker.StartPath(transform.position, target, OnPathComplete);
            }
            currentWaypoint++;
            return;
        }
        // Direction to the next waypoint
        distanceToPlayer = (transform.position - targetPlayer.position).magnitude;
        Vector3 dir = (path.vectorPath[currentWaypoint] - target).normalized;

		if(rotCounter == 4)
        {
            Vector3 targetToRotateTo = currentWaypoint < path.vectorPath.Count - 3 ? path.vectorPath[currentWaypoint + 2] : target;
            transform.LookAt(targetToRotateTo);
            transform.Rotate(0f, 90f, 90f);
            rotCounter--;
        }
        else if(rotCounter == 0)
        {
            rotCounter = 4;
        }
        else
        {
            rotCounter--;
        }


        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, path.vectorPath[currentWaypoint], step);

        // The commented line is equivalent to the one below, but the one that is used
        // is slightly faster since it does not have to calculate a square root
        //if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
        if ((transform.position-path.vectorPath[currentWaypoint]).sqrMagnitude < nextWaypointDistance*nextWaypointDistance)
		{
            currentWaypoint++;
            return;
        }
	}

    void ChasePlayer()
    {
        if(canChase && distanceToPlayer < triggerDistance)
        {
            chasing = true;
            canChase = false;
            StartCoroutine(ResetCanChase());
        }
    }

    IEnumerator ResetCanChase()
    {
        yield return new WaitForSeconds(chaseCooldownAfterSonar);
        canChase = true;
    }

    IEnumerator ChaseForSeconds()
    {
        yield return new WaitForSeconds(chaseForSeconds);
        chasing = false;
    }

    /// <summary>
    /// Sent when another object enters a collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnColliderEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            // End game
            
            other.gameObject.SetActive(false);
        }
    }
}
