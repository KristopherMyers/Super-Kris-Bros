using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask jumpablesLayerMask;

    public float playerSpeed = 2.25f;
    public bool facingRight = true;
    public float moveX;

    public int playerJumpPower = 200;
    public bool isGrounded;

    public float timeRemaining = 300;
    public int playerScore = 0;
    public GameObject timeRemainingUI;
    public GameObject playerScoreUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        timeRemaining -= Time.deltaTime;
        timeRemainingUI.GetComponent<TMP_Text>().text = ("Time \n" + (int)timeRemaining);
        if (timeRemaining <= 0)
        {
            TimeDeath();
        }
        playerScoreUI.GetComponent<TMP_Text>().text = ("Score \n" + playerScore);
    }
    void PlayerMove()
    {
        //CONTROLS
        moveX = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
        else if (Input.GetButtonDown("Jump") && !isGrounded)
        {
            Debug.Log("Jumping currently disabled");
        }
        //ANIMATIONS
        //PLAYER DIRECTION
        if (moveX < 0.0f && facingRight == true)
        {
            FlipPlayer();
        }
        else if (moveX > 0.0f && facingRight == false)
        {
            FlipPlayer();
        }
        //PHYSICS
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }
    void Jump()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
    }
    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("KillableEnemy"))
        {
            Jump();
            playerScore += 100;
            Debug.Log("Landed on " + collision.gameObject.name + ", which is a killable enemy. 100 score added.");
            Destroy(collision.gameObject);
        }
        else
        {
            Debug.Log("YOU STARTED TOUCHING GROUND");
            isGrounded = true;
        }
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("YOU ARE CURRENTLY TOUCHING GROUND");
        isGrounded = true;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("YOU ARE NO LONGER TOUCHING GROUND");
        isGrounded = false;
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("LevelEnd"))
        {
            Debug.Log("Touched Level End");
            CountScore();
        }
        if (collision.gameObject.CompareTag("Coin"))
        {
            playerScore += 100;
            Debug.Log("Touched Coin, 100 score added");
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("DeathZone"))
        {
            FallDeath();
        }
    }
    private void OnDestroy()
    {
        SceneManager.LoadScene("SampleScene");
    }
    void FallDeath()
    {
        Debug.Log("Player fell out of the world");
        SceneManager.LoadScene("SampleScene");
    }
    void TimeDeath()
    {
        Debug.Log("Player ran out of time");
        SceneManager.LoadScene("SampleScene");
    }
    void CountScore()
    {
        playerScore += (int)timeRemaining * 10;
        Debug.Log(playerScore);
    }
}
