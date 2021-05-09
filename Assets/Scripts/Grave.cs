using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave : MonoBehaviour
{
    private int hitPoints;
    [SerializeField] GameObject ghost;
    private Transform myTransform;
    private int ghostNum;
    [SerializeField] int maxGhosts;
    [SerializeField] float spawnTime;

    public Sprite slightDamaged;
    public Sprite veryDamaged;

    [SerializeField] bool spawnWithDelay;

    private void Awake()
    {
        myTransform = transform;
        ghostNum = 0;
        hitPoints = 3;
    }

    void Start()
    {
        if (spawnWithDelay)
        {
            StartCoroutine(spawnGhostDelay());
        }
    }

    private void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "hitbox")
        {
            getHit();
        }
    }

    void getHit()
    {
        hitPoints--;
        if (hitPoints == 2)
        {
            this.GetComponent<SpriteRenderer>().sprite = slightDamaged;
        }
        if (hitPoints == 1)
        {
            this.GetComponent<SpriteRenderer>().sprite = veryDamaged;
        }
        if (hitPoints == 0)
        {
            spawnGhost();
            Destroy(this);
        }
    }

    void spawnGhost()
    {
        Instantiate(ghost, myTransform.position, myTransform.rotation);
        ghostNum++; 
    }

    IEnumerator spawnGhostDelay()
    {
        yield return new WaitForSeconds(spawnTime);
    }


}
