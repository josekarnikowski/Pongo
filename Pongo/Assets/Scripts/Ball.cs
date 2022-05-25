using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private float ballVelocity;
    [SerializeField]
    private float maxBallSpeed;

    [SerializeField]
    private AudioClip ballHitSound;
    [SerializeField]
    private Transform AudioListener;

    [SerializeField]
    private Vector2 direction;
    private int randomDirection;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        //determinando a direção que a bola iniciará e aplicando velocidade;
        StartCoroutine(StartBallDirection());
    }
    void FixedUpdate()
    {
        rb.velocity = direction * ballVelocity;
    }

    private void Update()
    {
        VelocityControl();
    }

    IEnumerator StartBallDirection()
    {
        yield return new WaitForSeconds(2f);
        //Criando 4 direções possíveis para iniciar;
        randomDirection = Random.Range(0, 4);

        //aplicando a direção randomizada da Bola;
        if (randomDirection == 0)
        {
            direction = new Vector2(1, 1);
        }
        else if (randomDirection == 1)
        {
            direction = new Vector2(-1, 1);
        }
        else if (randomDirection == 2)
        {
            direction = new Vector2(-1, -1);
        }
        else
        {
            direction = new Vector2(1, -1);
        }

        ballVelocity = 5;
    }

    //Fazendo Pontos;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ScoreP1"))
        {
            GameManager.instance.P1Scored();
            GameManager.instance.MatchFinal();
            //GameManager.instance.RestartAfterScore();
            
            ResetBallPosition();
        }
        else if (other.CompareTag("ScoreP2"))
        {
            GameManager.instance.P2Scored();
            GameManager.instance.MatchFinal();

            ResetBallPosition();
        }
    }

    //Resetando a posicao da bola apos pontuar;
    public void ResetBallPosition()
    {
        if ((GameManager.instance.scoreNowP1 || GameManager.instance.scoreNowP2) && !GameManager.instance.matchEnd)
        {
            ballVelocity = 0;
            rb.velocity = Vector2.zero;
            transform.position = Vector2.zero;
            StartCoroutine(StartBallDirection());
        }
    }
 
  
    //adicionando um Som de impacto da Bola;
    private void OnCollisionEnter2D(Collision2D other)
    {
        AudioSource.PlayClipAtPoint(ballHitSound, AudioListener.position);

        if (other.gameObject.CompareTag("Wall"))
        {
            direction.y = -direction.y;
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            direction.x = -direction.x;
            ballVelocity = ballVelocity + 0.1f;
        }
    }

    //limitar a velocidade da bola;
    public void VelocityControl()
    {
       if(rb.velocity.magnitude > maxBallSpeed)
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxBallSpeed);
        }
    }
}
