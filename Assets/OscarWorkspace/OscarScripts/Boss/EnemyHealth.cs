using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    SpriteRenderer sprite;
    public Slider healthBar;
    public Animator animator;

    void Start() {
        currentHealth = maxHealth;
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    void Update(){
    	healthBar.value = currentHealth;
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Update();
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy Dead");
        animator.SetTrigger("Dead");
        GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        GetComponent<Collider2D>().enabled = false;;
        GetComponent<PirateAttacks>().enabled = false;
        GetComponent<Patrol>().enabled = false;
        GetComponent<EnemyPathFind>().enabled = false;
    }

    public void Disappear()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        this.enabled = false;
    }

}
