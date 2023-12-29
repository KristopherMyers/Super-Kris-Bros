using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHead : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.GetComponentInParent<PlayerController>().HeadBump();
        if (collision.gameObject.CompareTag("QuestionBlock"))
        {
            collision.gameObject.GetComponent<QuestionBlock>().ReleaseObject();
        }
        if (collision.gameObject.CompareTag("BreakableBrick"))
        {
            collision.gameObject.GetComponent<BreakableBrick>().BreakObject();
        }
    }
    public void StopColliding()
    {
        gameObject.SetActive(false);
    }
}
