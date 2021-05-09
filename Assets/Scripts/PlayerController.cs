using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{


    public int currentHealth;
    private int maxHealth;
    public int startingMaxHealth;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = startingMaxHealth;
        currentHealth = startingMaxHealth;
    }
    
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if(i < maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    void IncreaseMaxHealth()
    {
        maxHealth++;
    }

    int getMaxHealth()
    {
        return maxHealth;
    }

    int getCurrentHealth()
    {
        return currentHealth;
    }

    void damagePlayer()
    {
        currentHealth--;
    }
}
