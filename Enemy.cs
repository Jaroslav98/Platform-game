using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Rigidbody2D rb;

    public float movHor = 0f;
    public float speed = 3f;

    public  bool isGroundFloor = true; //zmienna czy dotyka podlogi
    public  bool isGroundFront = false;

    public LayerMask groundLayer; // czym jest podloga
    public float frontGrndRayDist = 0.25f;
    public float floorCheckY = 0.52f;
    public float frontCheck = 0.51f;
    public float frontDist = 0.001f;

    // te  funckje posluza nam do kontrolowania zachowania postaci
    public int scoreGive = 50;  //zmienna mowi nam ile pkt da nam przeciwnik

    private RaycastHit2D hit;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Game.obj.gamePaused) // jesli nasza gra jest wolna, wtedy nasz ruch poziomy wyniesie zero 
        {
            
            return;
        }

        // unikniecie upadku klifu 
        isGroundFloor = (Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y - floorCheckY, transform.position.z),
            new Vector3(movHor, 0,0), frontGrndRayDist, groundLayer));

        if (isGroundFloor)
            movHor = movHor * -1;

       // zderzenie sie ze sciana
       if (Physics2D.Raycast(transform.position, new Vector3(movHor, 0, 0), frontCheck, groundLayer))
            movHor = movHor * -1;

        // starcie z innym wrogiem
        hit = Physics2D.Raycast(new Vector3(transform.position.x + movHor*frontCheck, transform.position.y, transform.position.z),
            new Vector3(movHor, 0, 0), frontDist);

        if (hit != null)
            if (hit.transform != null)
                if (hit.transform.CompareTag("Enemy"))
                    movHor = movHor * -1;
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(movHor * speed, rb.velocity.y); // zarzadzanie ruchem postaci
    }

     void OnCollisionEnter2D(Collision2D collision)
    {
        // funkcja kolizyjna, ktora zniszczy nasz� postac- destrukcja gracza

        if (collision.gameObject.CompareTag("Player"))
        {
            //destrukcja gracza
            Player.obj.getDamage(); // obrazenia postaci
        }
     }
     void OnTriggerEnter2D(Collider2D collision)
    {
        // funkcja kolizyjna, zniszczenie tego wroga - destrukcja wroga
        if (collision.gameObject.CompareTag("Player"))
        {
            //destrukcja wroga
            AudioManager.obj.playEnemyHit(); //kiedy przeciwnik dostaje obrazenia
            getKilled();
        }
    }
      private void getKilled() //funkcja gdy wrog zginie
    {
        gameObject.SetActive(false);
    }
}
