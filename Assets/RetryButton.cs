using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RetryButton : MonoBehaviour
{
public void ButtonPressed()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
