using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager obj;

    public Text livesLbl;
    public Text scoreLbl;

    public Transform UIPanel;
    private void Awake()
    {
        obj = this;
    }

    public void updateLives ()
    {
        livesLbl.text = "" + Player.obj.lives;
        // funkcja aktualizujaca licznik zdrowia
    }

    public void updateScore ()
    {
        scoreLbl.text = "" + Game.obj.score;
        // funkcja aktualizuj�ca tablice wyniku
    }

    public void startGame() // pocz�tkowy panel podczas uruchamiania gry
    {
        AudioManager.obj.playGui();

        Game.obj.gamePaused = true;
        UIPanel.gameObject.SetActive(true);
    }

    public void hideInitPanel () // funkcja ukrywa g��wny panel
    {
        AudioManager.obj.playGui();
        Game.obj.gamePaused = false;
        UIPanel.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        obj = null;
    }
}