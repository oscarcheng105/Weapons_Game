using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateAI : StateMachineBehaviour
{   
    public float normalAttackRange = 1.5f;
    public float normalAttackRate = 1f;
    private float nextNormalAttack = 0f;

    public float lungeAttackRange = 4f;
    public float lungeAttackRate = 6f;
    private float nextLungeAttack = 0f;

    public float spinAttackRange = 2.5f;
    public float spinAttackRate = 6f;
    private float nextSpinAttack = 0f;

    public float jumpBackRange = 1f;
    public float jumpBackRate = 6f;
    private float nextJumpBack = 0f;

    private float dist;

    private Transform player;
    private Rigidbody2D rb;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dist = Vector2.Distance(player.position,rb.position);

        //Patrol State
        if(player.position.y >= 14 && animator.GetBool("Engaged") == false)
        {
            animator.SetTrigger("Engaged");
            animator.GetComponent<Patrol>().enabled = false;
            animator.GetComponent<EnemyPathFind>().enabled = true;
        }

        if(animator.GetBool("Engaged") == true)
        {
            //Engaged State
            if(dist<=lungeAttackRange && dist>=spinAttackRange && Time.time >= nextLungeAttack)
            {
                rb.velocity = new Vector2(0,0);
                animator.SetTrigger("LungeAttack");
                nextLungeAttack = Time.time + lungeAttackRate;
            }
            else if(dist<=spinAttackRange && dist>=normalAttackRange && Time.time>= nextSpinAttack)
            {
                rb.velocity = new Vector2(0,0);
                animator.SetTrigger("SpinAttack");
                nextSpinAttack = Time.time + nextSpinAttack;
            }
            else if(dist<=normalAttackRange && Time.time>= nextNormalAttack)
            {
                rb.velocity = new Vector2(0,0);
                animator.SetTrigger("NormalAttack");
                nextNormalAttack = Time.time + normalAttackRate;
            }
            else if(dist<=jumpBackRange && Time.time >= nextJumpBack)
            {
                rb.velocity = new Vector2(0,0);
                animator.SetTrigger("JumpBack");
                nextJumpBack = Time.time + jumpBackRate;
            }
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!animator.GetBool("Dead"))
        {
        animator.ResetTrigger("LungeAttack");
        animator.ResetTrigger("SpinAttack");
        animator.ResetTrigger("NormalAttack");
        animator.ResetTrigger("JumpBack");
        }
    }
}
