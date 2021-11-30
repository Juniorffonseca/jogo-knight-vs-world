using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate : MonoBehaviour
{
    public Rigidbody2D fogo;
    public GameObject player;
    public float speed = 4;
    void Start()
    {
        
    }

    void Update()
    {
        Rigidbody2D p = Instantiate(fogo, transform.position, transform.rotation);
        p.velocity = transform.forward * speed;
    }
}
