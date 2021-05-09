using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 
 * deal damage 
 * take damage *
 * heal  *
 * current health *
 * call specific attacks
 * list of move set
 * current behaviour of boss and next move
 * movement *
 * death function/spawn weapon
 * current level of boss *
 * 
*/

public enum Boss_Name { Pirate, Cavalier, Ranger };
public enum Skills { attack, defend };

public abstract class Boss : MonoBehaviour
{
    static int base_health = 10;
    static int base_speed = 1;
    //private fields
    private int health;
    private Skills[] skill_set;
    private float xpos, ypos;
    private bool is_dead;
    private int level;
    private float speed;

    public Boss(Boss_Name bossname, int level)
    {
        switch (bossname)
        {
            case Boss_Name.Pirate:
                //skill_set = 1;
                break;
            case Boss_Name.Cavalier:
                //skill_set = 1;       //placeholder
                break;
            case Boss_Name.Ranger:
                //skill_set = 1;
                break;
        }
        health = level * base_health;
        speed = level * base_speed;
        xpos = Input.GetAxis("Horizontal");
        ypos = Input.GetAxis("Vertical");
        this.level = level;
        is_dead = false;
    }

    public void takeDamage(int dealt_damage)
    {
        health -= dealt_damage;
    }
    public int getHealth()
    {
        return health;
    }
    public void heal()
    {
        health++; //how much to heal
    }
    /*public void move_x(int dir)
    {
        move_x += dir;
    }
    public void move_y(int dir)
    {
        move_y += dir;
    }*/
    public void updatePos()
    {
        xpos = Input.GetAxis("Horizontal");
        ypos = Input.GetAxis("Vertical");
        transform.position = transform.position + new Vector3(xpos * speed * Time.deltaTime, ypos * speed * Time.deltaTime, 0);
    }
    public int getLevel()
    {
        return level;
    }
    public void die()
    {
        is_dead = true;
    }



}
