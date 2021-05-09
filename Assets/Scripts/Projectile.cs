using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//
/// <summary>
/// Projectile Characteristics:
/// hitbox
/// duration
/// speed
/// direction
/// ways to add more than linear movement to projectiles
/// </summary>
//

///  Projectile class for projectiles (arrows/other ranged boss attacks)

public enum Projectile_Type { arrow, bomb, circle, boomerang }; 
// arrow - straight in a line
// bomb - moves some distance, bounces, and continues moving at a lower speed
// circle - moves in a circle
// boomerang - goes somewhere, moves back in the direction it came

public class Projectile : MonoBehaviour
{
    private Projectile_Type projectile;
    [SerializeField] Rigidbody2D rb;
    private int timeAlive;
    private int timeToLive;
    
    [SerializeField] int direction;
    public  float velX = 5f;
    public  float velY = 0f;

    private SpriteRenderer renderer;
    
 
    private int damage;
    // circular movement
    [SerializeField] float radius;
    [SerializeField] float angle;
    // Start is called before the first frame update


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // called on every tick of physics engine
    void FixedUpdate()
    {

        rb.velocity = new Vector2(velX, velY);
        Destroy(gameObject, 2f);
    }

    private void setProjectileType(Projectile_Type p)
    {
        projectile = p;
    }
    /*
    private void moveInCircle()
    {
        float x = radius * Mathf.Cos(angle);
        float y = radius * Mathf.Sin(angle);

        rb.MovePosition(new Vector2(x, y));
        angle += 2*Time.deltaTime;
        
    }
    
    private void moveInLine()
    {
        rb.MovePosition(rb.position + Time.deltaTime * velocity);
    }
    */
    
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        Boss enemy = collision.gameObject.GetComponent<Boss>();
        if (enemy)
        {

            // deal damage, play animation, remove projectile
            enemy.takeDamage(damage);
            Destroy(gameObject);
        }
    }
    



}
