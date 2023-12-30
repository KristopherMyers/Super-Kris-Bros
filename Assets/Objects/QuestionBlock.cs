using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestionBlock : MonoBehaviour
{
    public string containedItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReleaseObject()
    {
        if (containedItem == "Coin")
        {
            Debug.Log("There is a coin in here");
            gameObject.GetComponent<PlayerController>().AddCoin();
        }
        else if (containedItem == "GrowthPower")
        {
            Debug.Log("There is a power up in here");
        }
        else if (containedItem == "Vine")
        {
            Debug.Log("There is a vine in here");
        }
        else if (containedItem == "")
        {
            Debug.Log("Nothing has been set in this block");
        }

    }
}
