using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class playerhealth : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.position.y < -1.2)
        {
            Death();
        }
    }

    void Death()
    {
        Debug.Log("Player fell out of the world");
        SceneManager.LoadScene("SampleScene");
    }
 
}
