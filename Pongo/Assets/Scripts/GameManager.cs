using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [SerializeField]
    private int player1Score = 0;
    [SerializeField]
    private int player2Score = 0;

    [SerializeField]
    private Transform BallPosition;

    [SerializeField]
    private float ballXMaxPos;

    //[SerializeField]
    public bool scoreNowP1 = false;
    //[SerializeField]
    public bool scoreNowP2 = false;

    
    public bool matchEnd = false;

    private void Awake()
    {
        //para evitar mais de um GameManager - clausula de guarda;
        if(instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
    }
    void Update()
    {
        MatchFinal();
    }

    public void MatchFinal()
    {
        if (player1Score == 3 || player2Score == 3)
        {
            matchEnd = true;
            //acaba o jogo!
        }
    }

    //Marcando pontos;
    public void P1Scored()
    {
        player1Score++;
        Debug.Log("Player 1 Score's! = " + player1Score);
        scoreNowP1 = true;

    }

    public void P2Scored()
    {
        player2Score++;
        Debug.Log("Player 2 Score's! = " + player2Score);
        scoreNowP2 = true;
    } 

    public void RestartAfterScore()
    {
        if(scoreNowP1 || scoreNowP2)
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
