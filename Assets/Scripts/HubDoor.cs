using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubDoor : MonoBehaviour
{

    
    public string sceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        

    }
}
