using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PatrolMovement : MonoBehaviour
{

    public float enemySpeed = 0.4f;
    public int xMoveDirection = -1 ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector2(xMoveDirection, 0));
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xMoveDirection, 0) * enemySpeed;
        Color rayColor;
        if (hit.collider != null && hit.distance < 0.1f && !hit.collider.gameObject.CompareTag("Player"))
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Killed Player");
            Destroy(collision.gameObject);
        }
    }
        void Flip()
    {
        xMoveDirection *= -1;
    }
}
