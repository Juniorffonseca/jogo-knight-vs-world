using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    public GameObject firePrefab;
    public TextMeshProUGUI lifes, placa, coins, ammunition;
    private AudioSource audioPulo;
    public AudioSource coinSound;
    public AudioSource tiroSound;
    public AudioSource espadaSound;
    public static int moedas = 0, vidas = 3;
    [Header("Oque é chão")]
    public LayerMask whatIsGround;
    public LayerMask whatIsEnemy;
    [Header("Forças")]
    public  float jumpForce;
    public float municao = 0;
    public  float speedForce;
    [Header("Checando chão e paredes")]
    public Transform groundCheck;
    public Transform rightCheck;
    public Transform leftCheck;
    public Transform swordCheck;
    public bool isJumping = false;
    public bool estaPulando = false;
    bool isOnFloor = false;
    bool blockedRight = false;
    bool blockedLeft = false;
    bool swordRange = false;
    public float attackRange = 0.5f;
    Rigidbody2D body;
    SpriteRenderer sprite;
    private Animator anim;
    Renderer a;
    public GameObject inimigo;
    
    void Start()
    {
        a = gameObject.GetComponent<Renderer>();
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        audioPulo = GetComponent<AudioSource>(); 
        anim = GetComponent<Animator>(); 
    }

    void Update()
    {
        HUD();
        isOnFloor = Physics2D.Linecast (transform.position, groundCheck.position, whatIsGround);
        blockedRight = Physics2D.Linecast(transform.position, rightCheck.position, whatIsGround);
        blockedLeft = Physics2D.Linecast(transform.position, leftCheck.position, whatIsGround);



        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Jumping"))
        {
            estaPulando = true;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walking") || (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") || (anim.GetCurrentAnimatorStateInfo(0).IsName("Stoped"))))
        {
            estaPulando = false;
        }


        if (Input.GetButtonDown("Jump") && isOnFloor == true && !estaPulando && !Pause.isPaused)
        {
            isJumping = true;
            audioPulo.Play();
        }

        if (isJumping)
        {
            body.AddForce(new Vector2 (0, jumpForce));
            isJumping = false;
            GetComponent<Animator>().SetBool("jumping", true);
            GetComponent<Animator>().SetBool("walking", false);
        }


        else
        {
            GetComponent<Animator>().SetBool("jumping", false);
        }

        float move = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(move * speedForce, body.velocity.y);

        if ((move > 0 && sprite.flipX == true) || (move < 0 && sprite.flipX == false))
        {
            Flip();
        }

        if (move != 0 && !blockedRight && !blockedLeft)
        {
            GetComponent<Animator>().SetBool("walking", true);
        }

        else
        {
            GetComponent<Animator>().SetBool("walking", false);
        }

        if (vidas < 1)
        {
            Perdeu.perdeu = true;
        }

        if (Input.GetKeyDown("p") && municao > 0)
        {
            fire();
            municao--;
            tiroSound.Play();
        }

        if (Input.GetKeyDown("k"))
        {
            Attack();
            GetComponent<Animator>().SetBool("attack", true);
        }

        else
        {
            GetComponent<Animator>().SetBool("attack", false);
        }

    }
    
    public void Attack()
    {
        espadaSound.Play();

        Collider2D[] swordRange = Physics2D.OverlapCircleAll(swordCheck.position, attackRange, whatIsEnemy);

        foreach(Collider2D enemy in swordRange)
        {
            enemy.GetComponent<Enemy>().TakeDamage(100);
        }
    }

    public void fire()
    {
        GameObject b = Instantiate(firePrefab) as GameObject;
        b.transform.position = gameObject.transform.position;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.layer == 10)
        {
            moedas++;
            Destroy(coll.gameObject);
            coinSound.Play();
        }
        
        if (coll.gameObject.layer == 11)
        {
            placa.text = "Use W para pular e K para atacar.";
        }

        if (coll.gameObject.layer == 17)
        {
            placa.text = "Use a tecla P para atirar.";
            municao = 5;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.layer == 11)
        {
            placa.text = "";
        }
        
        if (coll.gameObject.layer == 17)
        {
            placa.text = "";
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.layer == 13 && vidas > 0 && Player2.imortal == false)
        {
            vidas = vidas - 1;
        }

        if (coll.gameObject.layer == 15)
        {
            vidas = 0;
        }

        if (coll.gameObject.layer == 13 && Player.vidas > 0)
        {
            StartCoroutine ("GetInvulnerable");
        }

    }

    void OnDrawGizmosSelected()
    {
        if (swordCheck == null)
            return;
        Gizmos.DrawWireSphere(swordCheck.position, attackRange);
    }

    void Flip()
    {
        sprite.flipX = !sprite.flipX;
    }

    void HUD()
    {
        lifes.text = "" + vidas;
        coins.text = "" + moedas;
        ammunition.text = "" + municao;
    }

        IEnumerator GetInvulnerable()
    {
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), inimigo.GetComponent<Collider2D>(), true);
        a.material.SetColor("_Color", Color.white);
        yield return new WaitForSeconds (3f);
        a.material.SetColor("_Color", Color.white);
        Physics2D.IgnoreCollision (gameObject.GetComponent<Collider2D>(), inimigo.GetComponent<Collider2D>(), false);

    }

}
