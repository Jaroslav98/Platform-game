using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Life : MonoBehaviour
{
    public int scoreGive = 30;  //odpowiada wynikowi, które doda nas do wyniku
    void OnTriggerEnter2D(Collider2D collision) // wykrywamy kiedy wchodzi z wyzwalaczem
    {
        if (collision.gameObject.CompareTag("Player")) // gracz zderza sie z tym obiektem(serce), a nie z innym
        {
            Game.obj.addScore(scoreGive);
            Player.obj.addLive();
            gameObject.SetActive(false);
            AudioManager.obj.playCoin();

            UIManager.obj.updateScore(); // aktualizujemy na panelu liczbe pkt
            UIManager.obj.updateLives(); // aktualizujemy na panelu liczbe zyc

            
        }
    }
}



