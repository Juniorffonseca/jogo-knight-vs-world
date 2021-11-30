using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool isPaused;
    public static bool timeIsPaused;
    public GameObject pauseMenu;

    void Start()
    {
        
    }

    void Update()
    {
        if (isPaused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }

        if (!isPaused)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
    }

    public void Unpause()
    {
        isPaused = !isPaused;
    }
}
