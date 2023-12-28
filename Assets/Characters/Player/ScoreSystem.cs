using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ScoreSystem : MonoBehaviour
{
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
        timeRemaining -= Time.deltaTime;
        timeRemainingUI.GetComponent<TMP_Text>().text = ("Time \n" + (int)timeRemaining);
        if (timeRemaining <= 0)
        {
            Death();
        }
        playerScoreUI.GetComponent<TMP_Text>().text = ("Score \n" + playerScore);
    }
    void Death()
    {
        Debug.Log("Player ran out of time");
        SceneManager.LoadScene("SampleScene");
    }
    private void OnTriggerEnter2D(Collider2D collision)
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
        if (collision.gameObject.CompareTag("KillableEnemy"))
        {
            playerScore += 100;
            Debug.Log("Landed on " + collision.gameObject.name + ", which is a killable enemy. 100 score added.");
            Destroy(collision.gameObject);
        }
    }
    void CountScore()
    {
        playerScore += (int)timeRemaining * 10;
        Debug.Log(playerScore);
    }
}
