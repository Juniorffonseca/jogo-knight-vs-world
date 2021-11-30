using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public float speed;
    public float speeddaFase;
    private float distance;

    private bool movingRight = true;
    private bool tocouPlayer = false;

    public Transform groundDetection;
    public Animator anim;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if(groundInfo.collider == true && groundInfo.collider.tag != "Player")
        {
            if(movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.layer == 16)
        {
            Destroy(gameObject, 1);
        }

        if(coll.gameObject.tag == "Player")
        {
            speed = 1;
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            speed = speeddaFase;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        GetComponent<Animator>().SetFloat("life", currentHealth);

        if (currentHealth <= 0)
        {
            Destroy(gameObject, 1);
        }

    }

}
