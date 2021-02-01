using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    private int playerScore = 0;
    public GameObject playerScoreUI;
    private AudioSource audioSource;


    void Start() {
        audioSource = gameObject.GetComponent<AudioSource>();
    }


    void Update()
    {        
        playerScoreUI.gameObject.GetComponent<Text>().text = ("'Buggies' Collected:  " + playerScore);
    }

    void OnDisable()
    {
        PlayerPrefs.SetInt("score", playerScore);
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.name == "Star Toy(Clone)" ||
            trig.gameObject.name == "Ball Toy(Clone)" ||
            trig.gameObject.name == "Hairtie Toy(Clone)" ||
            trig.gameObject.name == "Red Dot Toy(Clone)" ||
            trig.gameObject.name == "Mousie Toy(Clone)")
        {
            playerScore += 1;
            audioSource.PlayOneShot(audioSource.clip, .5f);
            Destroy(trig.gameObject);
        }
    }
}
