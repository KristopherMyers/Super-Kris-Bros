using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PatrolMovement : MonoBehaviour
{

    public float enemySpeed = 0.4f;
    public int xMoveDirection = -1 ;
    bool isDead = false;
    Rigidbody2D ridgidBody;
    Collider2D bodyCollider;
    Collider2D headCollider;

    private void Awake()
    {
        var colliders = gameObject.GetComponents<Collider2D>();
        bodyCollider = colliders[0];
        headCollider = colliders[1];
        ridgidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector2(xMoveDirection, 0));
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xMoveDirection, 0) * enemySpeed;
            Color rayColor;
            if (hit.collider != null && hit.distance < 0.1f && !hit.collider.gameObject.CompareTag("Player") && !hit.collider.gameObject.CompareTag("Coin"))
            {
                rayColor = Color.green;
                Flip();
            }
            else
            {
                rayColor = Color.red;
            }
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector2(xMoveDirection, 0), rayColor);
        }

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().Death("Demon");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") )
        {
            Death();
        }
    }

    void Flip()
    {
        xMoveDirection *= -1;
    }

    void Death()
    {
        isDead = true;
        ridgidBody.freezeRotation = false;
        bodyCollider.enabled = false;
        headCollider.enabled = false;
        ridgidBody.AddForce(Vector2.up * 50);
        ridgidBody.AddTorque(999f);
    }
}
