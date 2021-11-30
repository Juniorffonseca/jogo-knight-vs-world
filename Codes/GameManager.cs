using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void LoadScene ()
    {
        SceneManager.LoadScene("Fase01");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
