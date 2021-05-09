using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{

    public Cons.BossName boss;
    public Cons.Weapons weapon;

    private GameObject gamecontroller;

    private void Start()
    {
        gamecontroller = GameObject.Find("GameController");
        if (gamecontroller.GetComponent<ManageScenes>().getBossKills() > 0)
        {
            SpriteRenderer renderer;
            renderer = this.GetComponent<SpriteRenderer>();
            renderer.color = new Color(0f, 0f, 0f, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        this.transform.gameObject.SetActive(false);
        gamecontroller.GetComponent<ManageScenes>().setBossDead(boss, true);
        gamecontroller.GetComponent<ManageScenes>().BossKilled();
        gamecontroller.GetComponent<ManageScenes>().setHasWeapon(weapon, true);
    }
}
