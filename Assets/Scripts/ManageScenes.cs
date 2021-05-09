using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ManageScenes : MonoBehaviour
{
    
    int bossKills;
    Hashtable isBossDead = new Hashtable();
    Hashtable hasWeapon = new Hashtable();


    void Awake()
    {
        DontDestroyOnLoad(this);

        bossKills = 0;


        isBossDead.Add(Cons.BossName.Pirate, false);
        isBossDead.Add(Cons.BossName.Ranger, false);
        isBossDead.Add(Cons.BossName.Cavalier, false);

        hasWeapon.Add(Cons.Weapons.Sword, false);
        hasWeapon.Add(Cons.Weapons.Bow, false);
        hasWeapon.Add(Cons.Weapons.Spear, false);
    }

    public void setBossDead(Cons.BossName boss, bool isDead)
    {
        isBossDead[boss] = isDead;
    }

    public bool BossStatus(Cons.BossName boss)
    {
        return (bool)isBossDead[boss];
    }

    public void setHasWeapon(Cons.Weapons weapon, bool acquired)
    {
        hasWeapon[weapon] = acquired;
    }

    public bool WeaponStatus(Cons.Weapons weapon)
    {
        return (bool)hasWeapon[weapon];
    }

    public void BossKilled()
    {
        bossKills++;
    }

    public int getBossKills()
    {
        return bossKills;
    }
}
