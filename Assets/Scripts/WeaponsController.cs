using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    private float bowPressTime;
    private float swordPressTime;

    public Cons.Direction facing;
    public Cons.Direction moveDir;
    public int state;  //0 = not attacking, 1 = attack start >= 2 means attack in progress
    public GameObject player;
    public Animator animator;
    public float bowSpeed;
    public GameObject arrow;
    public bool moving;
    public bool inAnim;
    public bool attacking;
    public Cons.Weapons weapon;
    public int swordHits;
    public float swordComboTimer;
    public int damage;
    public float swordAnimTime;
    public bool isDashing;
    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        bowPressTime = 0;
        swordPressTime = 0;

        state = 0;
        inAnim = false;
        swordAnimTime = .25f;
        attacking = false;
        swordHits = 0;
        swordComboTimer = 1.8f;
        facing = player.GetComponent<PlayerMovement>().direction;
        damage = 0;
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
            isDead = true;
        if (!isDead)
        {
            inAnim = inAnimation();
            isDashing = player.GetComponent<PlayerMovement>().isDashing;
            if (isDashing)
            {
                animator.SetBool("Dashing", true);
                animator.SetTrigger("change");
            }
            if (!attacking && !isDashing)
            {
                animator.SetBool("Attacking", false);
                animator.SetBool("Dashing", false);
                facing = player.GetComponent<PlayerMovement>().direction;
                switch (facing)
                {
                    case Cons.Direction.West:
                        damage = 2;
                        animator.SetInteger("Direction", 4);
                        break;
                    case Cons.Direction.North:
                        damage = 2;
                        animator.SetInteger("Direction", 1);
                        break;
                    case Cons.Direction.East:
                        damage = 2;
                        animator.SetInteger("Direction", 2);
                        break;
                    case Cons.Direction.South:
                        damage = 2;
                        animator.SetInteger("Direction", 3);
                        break;
                }
                moving = player.GetComponent<PlayerMovement>().isWalking;
                if (moving)
                {
                    animator.SetBool("Moving", true);
                }
                else
                {
                    animator.SetBool("Moving", false);
                }
                inUse();

            }
            else if (attacking)
            {
                animator.SetBool("Dashing", false);
                animator.SetBool("Attacking", true);
                switch (weapon)
                {
                    case Cons.Weapons.Sword:
                        if (state == 1)
                        {
                            state = 2;
                            player.GetComponent<PlayerMovement>().canMove = false;
                            player.GetComponent<PlayerMovement>().canDash = false;
                            swordSlash(); //activates sword animation depending on the combo
                        }
                        else if (!inAnim)
                        {
                            attacking = false;
                            state = 0;
                            player.GetComponent<PlayerMovement>().canMove = true;
                            player.GetComponent<PlayerMovement>().canDash = true;

                        }
                        break;

                    case Cons.Weapons.Spear:
                        if (state == 1)
                        {
                            state = 2;
                            player.GetComponent<PlayerMovement>().canMove = false;
                            player.GetComponent<PlayerMovement>().canDash = false;

                            spearStab(); //activate spear stab animation
                        }
                        else if (!inAnim)
                        {
                            state = 0;
                            attacking = false;
                            player.GetComponent<PlayerMovement>().canMove = true;
                            player.GetComponent<PlayerMovement>().canDash = true;
                        }
                        break;

                    case Cons.Weapons.Bow:
                        if (state == 1)
                        {
                            state = 2;
                            damage = 2;
                            player.GetComponent<PlayerMovement>().canDash = false;
                            facing = player.GetComponent<PlayerMovement>().direction;
                            moveDir = facing;
                            bowCharge(); //start bow animation
                        }
                        else if (state == 2)
                        {
                            if (Input.GetButton("Bow"))
                            {
                                if (player.GetComponent<PlayerMovement>().direction != moveDir)
                                {
                                    animator.SetTrigger("change");
                                    moveDir = player.GetComponent<PlayerMovement>().direction;
                                }
                                bowCharge();
                                if (bowSpeed < 75)
                                    bowSpeed = 20 * (1 + 3 * (Time.time - bowPressTime));
                            }
                            if (!Input.GetButton("Bow"))
                            {
                                Arrow(); //fire arrow
                                attacking = false;
                                state = 0;
                                player.GetComponent<PlayerMovement>().canDash = true;
                                animator.SetBool("Bow", false);
                                animator.SetInteger("Direction", 0);
                                animator.SetInteger("MoveDirection", 0);
                                bowSpeed = 20;
                            }
                        }
                        break;
                }
            }
        }
    }

    public void inUse() //tells if an attack is being used and what weapon is doing the attack
    {
        if (Input.GetButtonDown("Sword"))
        {
            //GetComponent<ManageScenes>().WeaponStatus(Cons.Weapons.Sword);  //will check if the sword is held
            weapon = Cons.Weapons.Sword;
            if (swordPressTime < Time.time - swordComboTimer)
            {
                swordHits = 0;
            }
            swordPressTime = Time.time;
            attacking = true;
            state = 1;
        }
        else if (Input.GetButtonDown("Spear"))
        {
            //GetComponent<ManageScenes>().WeaponStatus(Cons.Weapons.Spear);
            weapon = Cons.Weapons.Spear;
            attacking = true;
            state = 1;
        }
        else if (Input.GetButtonDown("Bow"))
        {
            //GetComponent<ManageScenes>().WeaponStatus(Cons.Weapons.Bow);
            weapon = Cons.Weapons.Bow;
            bowPressTime = Time.time;
            attacking = true;
            state = 1;
        }
    }

    public void swordSlash()
    {
        animator.SetTrigger("Sword");
        switch (facing)
        {
            case Cons.Direction.West:
                animator.SetInteger("Direction", 4);
                switch (swordHits)
                {
                    case 0:
                        swordAnimTime = 1.25f;
                        damage = 1;
                        animator.SetTrigger("Sword1");
                        swordHits++;
                        break;
                    case 1:
                        damage = 1;
                        animator.SetTrigger("Sword2");
                        swordHits++;
                        break;
                    case 2:
                        damage = 2;
                        animator.SetTrigger("Sword3");
                        swordAnimTime = 1.5f;
                        swordHits = 0;
                        break;
                }
                break;
            case Cons.Direction.North:
                animator.SetInteger("Direction", 1);
                switch (swordHits)
                {
                    case 0:
                        damage = 1;
                        swordAnimTime = 1.25f;
                        //animator.SetTrigger("Sword1N");
                        animator.SetTrigger("Sword1");
                        swordHits++;
                        break;
                    case 1:
                        damage = 1;
                        //animator.SetTrigger("Sword2N");
                        animator.SetTrigger("Sword2");
                        swordHits++;
                        break;
                    case 2:
                        damage = 2;
                        //animator.SetTrigger("Sword3N");
                        animator.SetTrigger("Sword3");
                        swordAnimTime = 1.5f;
                        swordHits = 0;
                        break;
                }
                break;
            case Cons.Direction.East:
                animator.SetInteger("Direction", 2);
                switch (swordHits)
                {
                    case 0:
                        damage = 1;
                        swordAnimTime = 1.25f;
                        //animator.SetTrigger("Sword1E");
                        animator.SetTrigger("Sword1");
                        swordHits++;
                        break;
                    case 1:
                        damage = 1;
                        //animator.SetTrigger("Sword2E");
                        animator.SetTrigger("Sword2");
                        swordHits++;
                        break;
                    case 2:
                        damage = 2;
                        //animator.SetTrigger("Sword3E");
                        animator.SetTrigger("Sword3");
                        swordAnimTime = 1.5f;
                        swordHits = 0;
                        break;
                }
                break;
            case Cons.Direction.South:
                animator.SetInteger("Direction", 3);
                switch (swordHits)
                {
                    case 0:
                        damage = 1;
                        swordAnimTime = 1.25f;
                        //animator.SetTrigger("Sword1S");
                        animator.SetTrigger("Sword1");
                        swordHits++;
                        break;
                    case 1:
                        damage = 1;
                        //animator.SetTrigger("Sword2S");
                        animator.SetTrigger("Sword2");
                        swordHits++;
                        break;
                    case 2:
                        damage = 2;
                        //animator.SetTrigger("Sword3S");
                        animator.SetTrigger("Sword3");
                        swordAnimTime = 1.5f;
                        swordHits = 0;
                        break;
                }
                break;
        }
    }

    public void spearStab()
    {
        animator.SetTrigger("Spear");
        switch (facing)
        {
            case Cons.Direction.West:
                damage = 2;
                animator.SetInteger("Direction", 4);
                break;
            case Cons.Direction.North:
                damage = 2;
                animator.SetInteger("Direction", 1);
                break;
            case Cons.Direction.East:
                damage = 2;
                animator.SetInteger("Direction", 2);
                break;
            case Cons.Direction.South:
                damage = 2;
                animator.SetInteger("Direction", 3);
                break;
        }
    }

    public void bowCharge()
    {
        if (!moving && player.GetComponent<PlayerMovement>().isWalking)
            animator.SetTrigger("change");
        moving = player.GetComponent<PlayerMovement>().isWalking;
        if (!moving)
        {
            animator.SetBool("Moving", false);
        }
        animator.SetBool("Bow", true);
        switch (facing)
        {
            case Cons.Direction.West:
                animator.SetInteger("Direction", 4);
                break;
            case Cons.Direction.North:
                animator.SetInteger("Direction", 1);
                break;
            case Cons.Direction.East:
                animator.SetInteger("Direction", 2);
                break;
            case Cons.Direction.South:
                animator.SetInteger("Direction", 3);
                break;
        }
        /*if (moving)
        {
            animator.SetBool("Moving", true);
            switch (moveDir)
            {
                case Cons.Direction.West:
                    animator.SetInteger("MoveDirection", 4);
                    break;
                case Cons.Direction.North:
                    animator.SetInteger("MoveDirection", 1);
                    break;
                case Cons.Direction.East:
                    animator.SetInteger("MoveDirection", 2);
                    break;
                case Cons.Direction.South:
                    animator.SetInteger("MoveDirection", 3);
                    break;
            }
        }*/
    }

    void Arrow()
    {
        animator.SetTrigger("Fire");
        Vector2 arrowPos = player.transform.position;
        Debug.Log(arrowPos);
        Quaternion rot = Quaternion.identity;
        float arrowVelX = 0, arrowVelY = 0;
        switch (facing)
        {
            case Cons.Direction.West:
                animator.SetInteger("Direction", 4);
                arrowPos += new Vector2(-1.5f, 0);
                rot = Quaternion.Euler(0, 0, 180);
                arrowVelX = -bowSpeed;
                break;
            case Cons.Direction.North:
                animator.SetInteger("Direction", 1);
                arrowPos += new Vector2(0, +1.5f);
                rot = Quaternion.Euler(0, 0, 90);
                arrowVelY = bowSpeed;
                break;
            case Cons.Direction.East:
                animator.SetInteger("Direction", 2);
                arrowPos += new Vector2(+1.5f, 0);
                rot = Quaternion.identity;
                arrowVelX = bowSpeed;
                break;
            case Cons.Direction.South:
                animator.SetInteger("Direction", 3);
                arrowPos += new Vector2(0, -1.5f);
                rot = Quaternion.Euler(0, 0, -90);
                arrowVelY = -bowSpeed;
                break;
        }
        GameObject instance = Instantiate(arrow, arrowPos, rot) as GameObject;
        instance.GetComponent<Projectile>().velX = arrowVelX;
        instance.GetComponent<Projectile>().velY = arrowVelY;
    }

    public bool inAnimation()
    {
        bool inanim = true;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
           inanim = false;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("IdleSouth"))
        {
           inanim = false;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("IdleNorth"))
        {
            inanim = false;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("IdleEast"))
        {
            inanim = false;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("IdleWest"))
        {
            inanim = false;
        }
        return inanim;
    }
}