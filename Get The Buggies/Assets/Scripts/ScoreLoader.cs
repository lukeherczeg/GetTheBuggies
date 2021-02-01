using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreLoader : MonoBehaviour
{
    private int playerScore;
    public GameObject playerScoreUI;

    void Start()
    {
        playerScore = PlayerPrefs.GetInt("score");
    }

    void Update()
    {
        playerScoreUI.gameObject.GetComponent<Text>().text = ("'Buggies' Collected:  " + playerScore);
    }
}
