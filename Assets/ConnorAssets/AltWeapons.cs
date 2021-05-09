using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltWeapons : MonoBehaviour
{
    private float bowPressTime;
    private float swordPressTime;
    private float spearPressTime;
    private float bowPower;
    public int state; //0 = not attacking, 1 = attack start >= 2 means attack in progress
    private bool chargeBool;

    public Animator animator;
    public bool inAnim;
    public float swordAnimTime;
    public bool attacking;
    public Cons.Weapons weapon;
    public int swordHits;
    public float swordComboTimer;
    //public float spearChargeTime;
    public GameObject player;
    public Cons.Direction direction;

    public int damage;

    private float bowSpeed;

    public GameObject arrow;
    void Start()
    {
        bowPressTime = 0;
        swordPressTime = 0;
        spearPressTime = 0;
        bowPower = 1;
        state = 0;


        inAnim = false;
        swordAnimTime = .25f;
        attacking = false;
        swordHits = 0;
        swordComboTimer = 1.8f;
        //spearChargeTime = 1.5f;
        direction = player.GetComponent<PlayerMovement>().direction;
        damage = 0;
    }

    void Update()
    {

        Debug.Log("KFHBSDH");
        inAnim = inAnimation();
        if (!attacking)
        {
            Debug.Log("ASDFGH");
            direction = player.GetComponent<PlayerMovement>().direction;
            inUse();
        }
        else
        {
            switch (weapon)
            {
                case Cons.Weapons.Sword:
                    if (state == 1)
                    {
                        state = 2;
                        swordSlash(); //activates sword animation depending on the combo
                    }
                    else if (!inAnim)
                    {
                        attacking = false;
                        state = 0;
                    }
                    break;

                case Cons.Weapons.Spear:
                    if (state == 1)
                    {
                        if (Time.time - spearPressTime < .1f)
                        { }
                        else if (!Input.GetButton("Spear"))
                        {
                            state = 2;
                            spearStab(); //activate spear stab animation
                        }
                        /*else
                        {
                            state = 3;
                            chargeBool = false;
                            player.GetComponent<PlayerMovement>().canMove = false;
                            spearCharge1(); //start animation for charging the attack
                        }
                    }
                    else if(state == 2 && !inAnim)
                    {
                        attacking = false;
                        state = 0;
                    }
                    /*else if(state == 3)
                    {
                        if (!chargeBool)
                        {
                            direction = player.GetComponent<PlayerMovement>().direction;
                            spearCharge1();
                        }
                        if (Input.GetKeyDown("space") || (Time.time - spearPressTime < spearChargeTime && !Input.GetButton("Spear")))
                        {
                            switch (direction)
                            {
                                case Cons.Direction.West:
                                    animator.SetTrigger("CancelW");
                                    break;
                                case Cons.Direction.North:
                                    animator.SetTrigger("CancelN");
                                    break;
                                case Cons.Direction.East:
                                    animator.SetTrigger("CancelE");
                                    break;
                                case Cons.Direction.South:
                                    animator.SetTrigger("CancelS");
                                    break;
                            }
                            state = 0;
                            attacking = false;
                        }
                        if (Time.time - spearPressTime > spearChargeTime && !Input.GetButton("Spear") && !inAnim)
                        {
                            state = 0;
                            attacking = false;
                            player.GetComponent<PlayerMovement>().canMove = true;
                            player.GetComponent<PlayerMovement>().lockDir = false;
                        }
                        else if (Time.time - spearPressTime >= spearChargeTime && chargeBool == false)
                        {
                            player.GetComponent<PlayerMovement>().lockDir = true;
                            spearCharge2(); //start charge animation (not charging the attack)
                            chargeBool = true;
                        }*/
                    }
                    break;

                case Cons.Weapons.Bow:
                    if (state == 1)
                    {
                        state = 2;
                        damage = 2;
                        player.GetComponent<PlayerMovement>().canDash = false;
                        bowCharge(); //start bow animation
                    }
                    else
                    {
                        if (Input.GetButton("Bow"))
                        {
                            direction = player.GetComponent<PlayerMovement>().direction;
                            bowCharge();
                            if (bowPower < 3)
                            {
                                bowPower = 1 * (1 + (Time.time - bowPressTime));

                            }
                            if (bowSpeed < 50)
                                bowSpeed = 3 * (1 + (Time.time - bowPressTime));
                        }
                        if (!Input.GetButton("Bow"))
                        {
                            Debug.Log("Hello");
                            Arrow(); //fire arrow
                            if (!inAnim)
                            {
                                attacking = false;
                                state = 0;
                                player.GetComponent<PlayerMovement>().canDash = true;
                                animator.SetBool("FullCharge", false);
                                animator.ResetTrigger("ArrowW");
                                animator.ResetTrigger("ArrowN");
                                animator.ResetTrigger("ArrowE");
                                animator.ResetTrigger("ArrowS");
                                animator.ResetTrigger("BowChargeW");
                                animator.ResetTrigger("BowChargeN");
                                animator.ResetTrigger("BowChargeE");
                                animator.ResetTrigger("BowChargeS");

                                animator.SetInteger("Direction", 0);
                                animator.SetInteger("MDirection", 0);
                            }
                        }
                    }
                    break;
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
            spearPressTime = Time.time;
            attacking = true;
            state = 1;
        }
        else if (Input.GetButtonDown("Bow"))
        {
            //GetComponent<ManageScenes>().WeaponStatus(Cons.Weapons.Bow);
            animator.SetTrigger("BowCharge");
            weapon = Cons.Weapons.Bow;
            bowPressTime = Time.time;
            attacking = true;
            state = 1;
        }
    }

    public void swordSlash()
    {
        animator.SetTrigger("Sword");
        switch (direction)
        {
            case Cons.Direction.West:
                animator.SetInteger("Direction", 4);
                switch (swordHits)
                {
                    case 0:
                        swordAnimTime = 1.25f;
                        damage = 1;
                        //animator.SetTrigger("Sword1W);
                        animator.SetTrigger("Sword1");
                        swordHits++;
                        break;
                    case 1:
                        damage = 1;
                        //animator.SetTrigger("Sword2W");
                        animator.SetTrigger("Sword2");
                        swordHits++;
                        break;
                    case 2:
                        damage = 2;
                        //animator.SetTrigger("Sword3W");
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
        switch (direction)
        {
            case Cons.Direction.West:
                damage = 2;
                animator.SetTrigger("SpearStabW");
                break;
            case Cons.Direction.North:
                damage = 2;
                animator.SetTrigger("SpearStabN");
                break;
            case Cons.Direction.East:
                damage = 2;
                animator.SetTrigger("SpearStabE");
                break;
            case Cons.Direction.South:
                damage = 2;
                animator.SetTrigger("SpearStabS");
                break;
        }
    }

    public void spearCharge1()
    {
        switch (direction)
        {
            case Cons.Direction.West:
                animator.SetTrigger("SpearCharge1W");
                break;
            case Cons.Direction.North:
                animator.SetTrigger("SpearCharge1N");
                break;
            case Cons.Direction.East:
                animator.SetTrigger("SpearCharge1E");
                break;
            case Cons.Direction.South:
                animator.SetTrigger("SpearCharge1S");
                break;
        }
    }

    public void spearCharge2()
    {
        switch (direction)
        {
            case Cons.Direction.West:
                animator.SetTrigger("SpearCharge2W");
                break;
            case Cons.Direction.North:
                animator.SetTrigger("SpearCharge2N");
                break;
            case Cons.Direction.East:
                animator.SetTrigger("SpearCharge2E");
                break;
            case Cons.Direction.South:
                animator.SetTrigger("SpearCharge2S");
                break;
        }
    }

    public void bowCharge()
    {
        bool moving = player.GetComponent<PlayerMovement>().isWalking;
        animator.SetTrigger("Bow");
        if (!moving)
        {
            switch (direction)
            {
                case Cons.Direction.West:
                    animator.SetTrigger("BowChargeW");
                    animator.SetInteger("Direction", 4);
                    break;
                case Cons.Direction.North:
                    animator.SetTrigger("BowChargeN");
                    animator.SetInteger("Direction", 1);
                    break;
                case Cons.Direction.East:
                    animator.SetTrigger("BowChargeE");
                    animator.SetInteger("Direction", 2);
                    break;
                case Cons.Direction.South:
                    animator.SetTrigger("BowChargeS");
                    animator.SetInteger("Direction", 3);
                    break;
            }
        }
        else
        {
            switch (direction)
            {
                case Cons.Direction.West:
                    animator.SetTrigger("BowChargeWM");
                    animator.SetInteger("MDirection", 4);
                    break;
                case Cons.Direction.North:
                    animator.SetTrigger("BowChargeNM");
                    animator.SetInteger("MDirection", 1);
                    break;
                case Cons.Direction.East:
                    animator.SetTrigger("BowChargeEM");
                    animator.SetInteger("MDirection", 2);
                    break;
                case Cons.Direction.South:
                    animator.SetTrigger("BowChargeSM");
                    animator.SetInteger("MDirection", 4);
                    break;
            }
        }
    }

    public void Arrow()
    {
        Vector2 arrowPos = transform.position;
        Quaternion rot = Quaternion.identity;
        float arrowVelX = 0, arrowVelY = 0;
        switch (direction)
        {
            case Cons.Direction.West:
                animator.SetTrigger("ArrowW");
                arrowPos += new Vector2(-1.5f, 0);
                rot = Quaternion.Euler(0, 0, 180);
                arrowVelX = -bowSpeed;
                break;
            case Cons.Direction.North:
                animator.SetTrigger("ArrowN");
                arrowPos += new Vector2(0, +1.5f);
                rot = Quaternion.Euler(0, 0, 90);
                arrowVelY = bowSpeed;
                break;
            case Cons.Direction.East:
                animator.SetTrigger("ArrowE");
                arrowPos += new Vector2(+1.5f, 0);
                rot = Quaternion.identity;
                arrowVelX = bowSpeed;
                break;
            case Cons.Direction.South:
                animator.SetTrigger("ArrowS");
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
        return !animator.GetCurrentAnimatorStateInfo(0).IsName("Inactive");
    }
}
