using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateAttacks : MonoBehaviour
{   
    public Transform target;
    public Rigidbody2D rb;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float dashSpeed = 1000;

    public float normalAttackRange = 1f;
    public int normalAttackDamage = 10;
    public float lungeAttackRange = 6f;
    public int lungeAttackDamage = 15;
    public float spinAttackRange =  3f;
    public int spinAttackDamage = 12;    

    private Vector2 direction;
    
    void Update() {
        direction = (target.transform.position-transform.position).normalized;
    }

    void NormalAttack()
    {
        Hit(normalAttackDamage, normalAttackRange);
    }
    void LungeAttack()
    {   
        //rb.bodyType = RigidbodyType2D.Kinematic;
        Hit((lungeAttackDamage), lungeAttackRange);
    }

    void SpinAttack()
    {   
        //rb.bodyType = RigidbodyType2D.Kinematic;
        Hit((spinAttackDamage), spinAttackRange);
    }

    void Thrust()
    {
        rb.velocity =  direction * dashSpeed * Time.deltaTime * 1.5f;
    } 
    void Lunge()
    {
        rb.velocity = direction * dashSpeed * Time.deltaTime * 3f;
    }
    void JumpBack()
    {
        rb.velocity = -direction * dashSpeed * Time.deltaTime;
    }

    private void Hit(int damage, float range)
    {
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, range, enemyLayers);
        foreach(Collider2D p in hitPlayers)
        {
            p.GetComponent<PlayerHealth>().takeDamage(damage);  
        }
    }
    
    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position,normalAttackRange);
        Gizmos.DrawWireSphere(attackPoint.position,lungeAttackRange);
        Gizmos.DrawWireSphere(attackPoint.position,spinAttackRange);
    }
}
