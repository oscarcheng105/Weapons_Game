using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyPathFind : MonoBehaviour
{
    public Transform target;
    public float speed=5f;
    public float nextWayPointDistance=3f;
    public Animator animator;
    
    Path path;
    int currentWaypoint=0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;
    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath",0f,0.5f);
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(rb.position,target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void Update()
    {
        Vector2 direction = (target.transform.position-transform.position).normalized;
        Vector2 force = direction * 400 * Time.deltaTime;
        movement.x = force.x;
        movement.y = force.y;

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(path==null)
            return;
        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true; 
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        transform.position = Vector2.MoveTowards(transform.position, (Vector2)path.vectorPath[currentWaypoint], speed*Time.deltaTime);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if(distance < nextWayPointDistance)
        {
            currentWaypoint++; 
        }

    }
}
