using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) // sprawdzamy czy obiekt z ktorym sie zderzamy jest graczem
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Game.obj.gameOver();
        }
    }
}
