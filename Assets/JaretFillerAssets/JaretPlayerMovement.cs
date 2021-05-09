using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaretPlayerMovement : MonoBehaviour
{
    
    public float movementSpeed;
    private Rigidbody2D rigidbody;
    private Vector2 movement;
    public GameObject attack;


    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();
        Physics.IgnoreLayerCollision(10, 12);
    }

    
    void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        rigidbody.MovePosition(rigidbody.position + movement * movementSpeed * Time.fixedDeltaTime);
    }

    void Update()
    {
        /*if (Input.GetKeyDown("space"))
        {
            Transform playerT = this.GetComponent<Transform>();
            Instantiate(attack, 
                new Vector3(playerT.position.x, playerT.position.y, playerT.position.z),
                Quaternion.identity);
        }*/
    }

}
