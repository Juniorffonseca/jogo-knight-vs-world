using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public static bool teste;
    bool aberto = false;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            GetComponent<Animator>().SetBool("abriu", true);
            Player.vidas ++;
            Destroy(gameObject);
        }

        else
        {
            GetComponent<Animator>().SetBool("abriu", false);
        }

        

    }

}
