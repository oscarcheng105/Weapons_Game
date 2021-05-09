using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    private bool paused = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            paused = togglePause();
        }
    }

    void OnGUI()
    {
        if (paused)
        {
            GUILayout.Box("Game is Paused");
            if (GUILayout.Button("Click me to return to main menu"))
            {
                paused = togglePause();
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    bool togglePause()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            return (false);
        }
        else
        {
            Time.timeScale = 0f;
            return (true);
        }
    }
}
