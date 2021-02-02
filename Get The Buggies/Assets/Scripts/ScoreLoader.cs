using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreLoader : MonoBehaviour
{
    private int playerScore;
    private float timeSurvived, minutesSurvived, remainingSecondsSurvived;
    public GameObject playerScoreUI, playerHardModePlayedUI, playerCasualModePlayedUI, playerTimeSurvivedUI;

    void Start()
    {
        playerScore = PlayerPrefs.GetInt("score");
        timeSurvived = PlayerPrefs.GetFloat("Survival Time");

        minutesSurvived = Mathf.FloorToInt(timeSurvived / 60F);
        remainingSecondsSurvived = Mathf.FloorToInt(timeSurvived - minutesSurvived * 60);
        string niceTime = string.Format("{0:0}:{1:00}", minutesSurvived, remainingSecondsSurvived);

        playerScoreUI.gameObject.GetComponent<Text>().text = ("'Buggies' Collected:  " + playerScore);
        playerTimeSurvivedUI.gameObject.GetComponent<Text>().text = ("Time Survived:  " + niceTime);

        if (PlayerPrefs.GetInt("Difficulty") == 1)
        {
            playerHardModePlayedUI.SetActive(true);
            playerCasualModePlayedUI.SetActive(false);
        }
        else
        {
            playerHardModePlayedUI.SetActive(false);
            playerCasualModePlayedUI.SetActive(true);
        }
            
    }

    void Update()
    {
        
    }
}
