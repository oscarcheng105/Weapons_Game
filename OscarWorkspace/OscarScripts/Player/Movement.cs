﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	public float moveSpeed = 4f;

	public Rigidbody2D rb;
    public Animator animator;

	Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Speed",movement.sqrMagnitude);
        animator.SetFloat("Horizontal",movement.x);
        animator.SetFloat("Vertical",movement.y);

    }

    void FixedUpdate()
    {
    	rb.MovePosition(rb.position+movement*moveSpeed*Time.fixedDeltaTime);
    }
}
