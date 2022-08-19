using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int scoreGive = 100;  //odpowiada wynikowi, które doda nas do wyniku

    void OnTriggerEnter2D(Collider2D collision) // wykrywamy kiedy wchodzi z wyzwalaczem
    {
        if (collision.gameObject.CompareTag("Player")) // gracz zderza sie z tym obiektem (moneta), a nie z innym
        {
          Game.obj.addScore(scoreGive);

            AudioManager.obj.playCoin(); // dodajemy dzwiek

            UIManager.obj.updateScore(); // gdy dostaniemy monete, dodamy do panelu liczbe
            
            gameObject.SetActive(false);
        }
    }
}
