using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;    // biblioteka sluzy do zarzadzania scenami

public class Game : MonoBehaviour   

{
    public static Game obj;  //zmienna statyczna do obiektu Game

    public int maxLives = 3; //maksymalna lczba zyc postaci

    public bool gamePaused = false;   // zmienna jesli gra zostanie wstrzymana
    public int score = 0;      //wynik gracza

    void Awake()
    {
        obj = this;  
    }
    // Start is called before the first frame update
    void Start()
    {
        gamePaused = false;   // gra nie zostanie wsztrzymana
        UIManager.obj.startGame(); // pokazuje panel na poczatku gry
    }

    public void addScore(int scoreGive) // fukcja ktora sluzy do dodawaniu punktacji
    {
        score = scoreGive;
    }

    public void gameOver() // gameOver bedzie oznaczac ponowne uruchomienie gry
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // mozemy zaladowac scene
    }

     void OnDestroy()
    {
        obj = null;
    }
}
