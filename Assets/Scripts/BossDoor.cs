using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


///door that takes you into boss room.

public class BossDoor : MonoBehaviour
{

    public string sceneName;
    public Cons.BossName boss;

    private bool activated;
    private bool contacted;
    private float timer;

    private SpriteRenderer renderer;

    private void Start()
    {
        contacted = false;
        timer = 0f;
        activated = true;
        renderer = this.GetComponent<SpriteRenderer>();
        if ( GameObject.Find("GameController").GetComponent<ManageScenes>().BossStatus(boss) ) 
        {
            activated = false;
            renderer.color = new Color(0f, 0f, 0f, 1f);
        }
    }

    
    private void FixedUpdate()
    {
        if (contacted)
        {
            Contacted();
        }
    }

    private void Contacted()
    {
        timer++;
        renderer.color = Color.Lerp(Color.blue, Color.red, Mathf.PingPong(Time.time, 1));
        if (timer > 40)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }

    private void OnTriggerEnter2D()
    {
        /*Debug.Log("contact");
        if(activated)
        {
            timer++;
            //renderer.color = Color.Lerp(Color.red, Color.blue, Mathf.PingPong(Time.time, 1));
            if(timer>40)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
            }
        }
        */
        contacted = true;
        
    }
    private void OnTriggerExit2D()
    {
        renderer.color = Color.red;
        contacted = false;
        timer = 0;
    }

}
