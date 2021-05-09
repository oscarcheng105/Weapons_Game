using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
	public int maxHealth = 50;
	int currentHealth;
    public Slider healthBar;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
       currentHealth = maxHealth;
    }

    void Update()
    {
        healthBar.value = currentHealth;
    }

    public void takeDamage(int damage)
    {
    	currentHealth -= damage;
    	if(currentHealth <=0)
        {
            Update();
    		Die();
        }
    }

    void Die()
    {
    	Debug.Log("Player Dead");
        animator.SetBool("Dead", true);
        animator.SetTrigger("Die");
        GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        GetComponent<Collider2D>().enabled = false;
    	GetComponent<PlayerAttack>().enabled = false;
    	GetComponent<Movement>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        this.enabled = false;
    }
}
