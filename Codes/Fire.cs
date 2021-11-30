using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject screen;
    public float speed = 50.0f;
    private Rigidbody2D rb;
    private Vector2 screenBounds;
    public GameObject explosion;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed, 0);
        screenBounds = Camera.main.ScreenToWorldPoint( new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void Update()
    {
            Destroy(this.gameObject, 4);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 13)
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
