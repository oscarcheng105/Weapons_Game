using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    private float currentSpot;
    private float startPosition;
    public float speed = 3f;
    public Transform[] moveSpots;
    private int randomSpots;
    private float waitTime;
    public float startWaitTime;
    public Animator animator;
    public Vector2 movement;
    private Vector2 direction;
    private Vector2 force;
    private bool idle = true;

    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        randomSpots = Random.Range(0,moveSpots.Length);
        currentSpot = randomSpots;
        startPosition = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //Move boss to position
        transform.position = Vector2.MoveTowards(transform.position,moveSpots[randomSpots].position, speed*Time.deltaTime);
        //Animation indication
        force = (moveSpots[randomSpots].position-transform.position) * speed * Time.deltaTime;
        movement.x = force.x*100;
        movement.y = force.y*100;
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        if(idle)
        {
            animator.SetFloat("idleHorizontal", movement.x);
            animator.SetFloat("idleVertical", movement.y);
            animator.SetFloat("idleSpeed", movement.sqrMagnitude);
            idle = false;
        }

        if(Vector2.Distance(transform.position, moveSpots[randomSpots].position) <0.1f)
        {
            if(waitTime<=0)
            {
                startPosition = transform.position.y;
                do{
                randomSpots = Random.Range(0,moveSpots.Length);
                }while(currentSpot == randomSpots);
                currentSpot = randomSpots;
                waitTime = startWaitTime;
                idle = true;
            }
            else
            {
                animator.SetFloat("Speed", 0);
                waitTime -= Time.deltaTime;
            }
        }
    }
}
