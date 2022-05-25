using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 myPos;

    [SerializeField]
    private float myXpos;
    [SerializeField]
    private float myYPos;
    //variável que limita o deslocamento do player no eixo Y;
    [SerializeField]
    private float maxYpos;

    [SerializeField]
    private float playerSpeed;

    [SerializeField]
    private bool imPlayer1;

    private float deltaVelocity;

    //controlador automático do player;
    [SerializeField]
    private bool automatic = false;

    //pegar a posição da bola
    [SerializeField]
    private Transform ballYPosition;

    [SerializeField]
    [Range(0.1f, 5f)]
    private float aiVelocity;


    void Update()
    {
        PlayerMovement();
        DeltaVelocityCalculation();
        AvoidPlayerToLeaveScreen();
        AIActivation();
    }

    void PlayerMovement()
    {
        //passando minha posicão X para meu Vector3;
        myPos.x = myXpos;
        //passando minha posicão Y para meu Vector3;
        myPos.y = myYPos;

        //Alterando o a posição do meu player;
        transform.position = myPos;

        if (!automatic)
        {
            if (imPlayer1)
            {
                //pegando INPUT de usuário;
                if (Input.GetKey("w"))
                {
                    myYPos += deltaVelocity;//playerSpeed * Time.deltaTime;
                }

                if (Input.GetKey("s"))
                {
                    myYPos -= deltaVelocity;//playerSpeed * Time.deltaTime;
                }
            }
            else
            {
                //pegando INPUT de usuário;
                if (Input.GetKey("up"))
                {
                    myYPos += deltaVelocity;//playerSpeed * Time.deltaTime;                  
                }

                if (Input.GetKey("down"))
                {
                    myYPos -= deltaVelocity;//playerSpeed * Time.deltaTime;
                }
            }
        }
        //AI do oponente;
        else
        {
            //pegar a posição da bola;
            //float ballYPosition = GetComponent<Ball>().transform.position.y;
            //myYPos = ballYPosition.transform.position.y;
            myYPos = Mathf.Lerp(myYPos, ballYPosition.transform.position.y, aiVelocity/100);
        }

    }
    void DeltaVelocityCalculation()
    {
        float deltaVelocityResult;
        deltaVelocityResult = GetComponent<Player>().deltaVelocity;
        deltaVelocity = playerSpeed * Time.deltaTime;
    }

    void AvoidPlayerToLeaveScreen()
    {
        //controlando o ponto de movimento máximo dos jogadores máximo dos jogadores;
        if (myYPos > maxYpos)
        {
            myYPos = maxYpos;
        }
        if (myYPos < -maxYpos)
        {
            myYPos = -maxYpos;
        }
    }

    void AIActivation()
    {
        if (!imPlayer1 && automatic == true)
        {
            //Saindo da AI
            if (Input.GetKey("up") || Input.GetKey("down"))
            {
                automatic = false;
            }
        }
        if (!imPlayer1 && automatic == false)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                automatic = true;
            }
        }
    }
}

