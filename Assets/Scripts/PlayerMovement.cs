using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float maxSpeed;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDuration;

    private Vector3 velocity;
    private Vector3 lastDirection;
    private Vector3 dashVelocity;
    private float dashTimer;

    // other components use this if they need to override the controls to move
    public Vector3 forcedMovementVelocity;
    public float forcedMovementTimer;

    public bool canMove = true;
    public bool canDash = true;
    public bool lockDir = false;

    // properties for direction and walking/dashing status
    public Cons.Direction direction
    {
        get
        {
            if (Mathf.Abs(lastDirection.x) > Mathf.Abs(lastDirection.y))
            {
                return lastDirection.x > 0 ? Cons.Direction.East : Cons.Direction.West;
            }
            else
            {
                return lastDirection.y > 0 ? Cons.Direction.North : Cons.Direction.South;
            }

        }
    }
    public bool isWalking
    {
        get { return canMove && velocity.sqrMagnitude != 0; }
    }
    public bool isDashing
    {
        get { return dashTimer > 0; }
    }

    // during spear charge up, locked in place until dodge or spear charge go
    // during bow charge, can move but pressing dodge will instead aim in that direction
    private void Update()
    {
        velocity.x = Input.GetAxis("Horizontal") * maxSpeed;
        velocity.y = Input.GetAxis("Vertical") * maxSpeed;

        if (!lockDir)
        {
            if (velocity.sqrMagnitude != 0)
                lastDirection = velocity;
        }
        if (canDash)
        {
            if (Input.GetKeyDown("space") && dashTimer <= 0)
            {
                canMove = false;
                dashTimer = dashDuration;
                dashVelocity = velocity.normalized * dashSpeed;
                if (dashVelocity.sqrMagnitude == 0)
                    dashVelocity = lastDirection.normalized * dashSpeed;
            }
        }
        /*
        canMove = !Input.GetKey("e");
        canDash = !Input.GetKey("q");
        lockDir = Input.GetKey("x");
        switch (direction)
        {
            case Cons.Direction.North:
                GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case Cons.Direction.East:
                GetComponent<SpriteRenderer>().color = Color.yellow;
                break;
            case Cons.Direction.South:
                GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case Cons.Direction.West:
                GetComponent<SpriteRenderer>().color = Color.blue;
                break;
        }
        */
    }

    private void FixedUpdate()
    {
        if(forcedMovementTimer > 0)
        {
            GetComponent<Rigidbody2D>().MovePosition(transform.position + forcedMovementVelocity);
            forcedMovementTimer -= Time.deltaTime;
        }
        else if(dashTimer > 0)
        {
            GetComponent<Rigidbody2D>().MovePosition(transform.position + dashVelocity);
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0) canMove = true;
        }
        else if (canMove) {
            GetComponent<Rigidbody2D>().MovePosition(transform.position + velocity);
        }
    }
}
