using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    [SerializeField] private LayerMask jumpablesLayerMask;
    
    public float playerSpeed = 2.25f;
    public bool facingRight = true;
    public float moveX;

    public int playerJumpPower = 200;
    public bool isGrounded;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        //CONTROLS
        moveX = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump")) {
            Jump();
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
        if (isGrounded)
        {
            this.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
        }
        else
        {
            Debug.Log("Can't jump right now");
        }
        
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
        Debug.Log("YOU STARTED TOUCHING GROUND");
        isGrounded = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("YOU ARE TOUCHING GROUND");
        isGrounded = true;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("YOU ARE NO LONGER TOUCHING GROUND");
        isGrounded = false;
    }
}
