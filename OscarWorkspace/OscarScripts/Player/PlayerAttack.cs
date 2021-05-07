using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 10f;
    public int attackDamage = 20;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public Animator animator;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("j") || Input.GetKeyDown("k"))
            Attack();
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().takeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected() 
    {
        if(attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}