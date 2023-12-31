using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    public bool isDead = false;

    public float timeRemaining = 400;
    public int playerScore = 0;
    public GameObject timeRemainingUI;
    public GameObject playerScoreUI;
    public GameObject levelEndUI;
    public GameObject replayButtonUI; 

    Rigidbody2D ridgidBody;
    Collider2D bodyCollider;
    Collider2D feetCollider;

    private void Awake()
    {
        var colliders = gameObject.GetComponents<Collider2D>();
        ridgidBody = gameObject.GetComponent<Rigidbody2D>();
        bodyCollider = colliders[0];
        feetCollider = colliders[1];
    }

    void Start()
    {
        levelEndUI.SetActive(false);
        replayButtonUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        if (!isDead)
        {
            timeRemaining -= Time.deltaTime;
            timeRemainingUI.GetComponent<TMP_Text>().text = ("Time \n" + (int)timeRemaining);
        }
        
        if (timeRemaining <= 0)
        {
            Death("Time");
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
        ridgidBody.velocity = new Vector2(moveX * playerSpeed, ridgidBody.velocity.y);
    }
    void Jump()
    {
        ridgidBody.AddForce(Vector2.up * playerJumpPower);
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
        
        isGrounded = true;
        if (collision.gameObject.CompareTag("DeathZone"))
        {
            Death("Fell");
        }
        if (collision.gameObject.CompareTag("LevelEnd"))
        {
            Debug.Log("Touched Level End");
            CountScore();
            Win();
        }
        if (collision.gameObject.CompareTag("Coin"))
        {
            AddCoin();
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        isGrounded = true;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

    }
    
    void CountScore()
    {
        playerScore += (int)timeRemaining * 10;
    }
    void ShowEndScreen()
    {
        levelEndUI.SetActive(true);
        replayButtonUI.SetActive(true);
    }
    
    public void Death(string reason)
    {
        isDead = true;
        ShowEndScreen();
        playerSpeed = 0;
        playerJumpPower = 0;
        ridgidBody.freezeRotation = false;
        bodyCollider.enabled = false;
        feetCollider.enabled = false;
        gameObject.GetComponentInChildren<PlayerHead>().StopColliding();
        ridgidBody.AddForce(Vector2.up * 200);
        ridgidBody.AddTorque(999f);
        string fullString = "";
        if (reason == "Fell")
        {
            fullString += "You Fell";
        }
        else if (reason == "Time") {
            fullString += "Out of Time";
        }
        else
        {
            fullString += "You died to a " + reason + " ";
        }
        fullString += "\n Final Score: " + playerScore;
        levelEndUI.GetComponent<TMP_Text>().text = fullString;
    }
    void Win()
    {
        ShowEndScreen();
        playerSpeed = 0;
        playerJumpPower = 0;
        string fullString = "";
        fullString += "Level Complete!";
        fullString += "\n Final Score: " + playerScore;
        levelEndUI.GetComponent<TMP_Text>().text = fullString;
    }

    public void AddCoin()
    {
        playerScore += 100;
        Debug.Log("Touched Coin, 100 score added");
    }
    public void HeadBump()
    {
        ridgidBody.velocity = new Vector2(ridgidBody.velocity.x, -0.1f);
    }
}
