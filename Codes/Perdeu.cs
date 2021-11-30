using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Perdeu : MonoBehaviour
{
    public static bool perdeu;

    public GameObject Jogador;

    public GameObject perdeuMenu;

    void Start()
    {

    }

    void Update()
    {
        if (perdeu == true)
        {
            perdeuMenu.SetActive(true);
            Time.timeScale = 0f;
            GetComponent<Pause>().enabled = false;
        }
        if (perdeu == false)
        {
            perdeuMenu.SetActive(false);
            GetComponent<Pause>().enabled = true;
        }
    }

    public void Recomecar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        perdeu = false;
        Player.vidas = 3;
        Player.moedas = 0;
    }

    public void Sair()
    {
        Application.Quit();
    }
}
