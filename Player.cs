using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player obj;
    public int lives = 3;   // ¿ycie postaci

    public bool isGrounded = false;  //dowiemy sie czy postac dotyka podlogi
    public bool isMoving = false;    // dowiemy sie czy postac sie porusza
    public bool isImmune = false;      // cay postac reaguje zgodne z oczekiwaniami

    public float speed = 5f;          // predkosc z jaka postac bedzie sie poruszac
    public float jumpForce = 3f;       // jak wysoko bedzie postac skakac 
    public float movHor;               // wlasciwosc ruchu naszej postaci

    public float immuneTimeCnt = 0f;
    public float immuneTime = 0.5f;

    public LayerMask groundLayer;
    public float radius = 0.3f;
    public float groundRayDist = 0.5f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spr;


     void Awake() // ta funkcja wykonywana jest przed startem
    {
        obj = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Game.obj.gamePaused)  // jesli nasza gra jest wolna, wtedy nasz ruch poziomy wyniesie zero 
        {
            movHor = 0f;
            return;
        }

        movHor = Input.GetAxisRaw("Horizontal");

        isMoving = (movHor != 0f);

        isGrounded = Physics2D.CircleCast(transform.position, radius, Vector3.down, groundRayDist, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space))
            jump();

        if (isImmune)
        {
            spr.enabled = !spr.enabled;
            immuneTimeCnt -= Time.deltaTime;

            if (immuneTimeCnt <=0)
            {
                isImmune = false;
                spr.enabled = true;
            }

        }

        flip(movHor);
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(movHor * speed, rb.velocity.y);
    }

    private void goImmune()
    {
        isImmune= true;
        immuneTimeCnt = immuneTime;
    }
    public void jump()                          // postac skacze
    {
        if (!isGrounded) return;

        rb.velocity = Vector2.up * jumpForce;
        AudioManager.obj.playJump(); //dzwiek skakania
    }

    private void flip(float _xValue)           // postac odwraca sie lewo- prawo
    {
        Vector3 theScale =transform.localScale;

        if (_xValue < 0)
            theScale.x = Mathf.Abs(theScale.x) * -1;

        else
            if (_xValue > 0)
            theScale.x = Mathf.Abs(theScale.x);

        transform.localScale = theScale;
    }

    public void getDamage()  //zmniejszenie liczby ¿yc
    {
        lives--;
        AudioManager.obj.playHit(); //dzwiek kiedy postac dostaje obrazenia

        UIManager.obj.updateLives(); // aktualizacja zycia w panelu
        

        goImmune();
        if (lives <= 0)
            this.gameObject.SetActive(false);
    }

    public void addLive()
        {
        lives++;  //zwiekszamy licznik twojego zdrowia
        if (lives > Game.obj.maxLives)   // jesli zycie gracza jest wieksze niz te okreslane maksymalne
            lives = Game.obj.maxLives;   // to zycie bedzie rowne maksimum



    }
    void onDestroy()
    {
        obj = null;
    }
}
