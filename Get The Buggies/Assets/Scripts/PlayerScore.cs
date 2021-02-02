using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    private int playerScore = 0;
    public GameObject playerScoreUI;
    private AudioSource audioSource;
    private float timeSinceBuggy = 0;


    void Start() {
        audioSource = gameObject.GetComponent<AudioSource>();
    }


    void Update()
    {        
        playerScoreUI.gameObject.GetComponent<Text>().text = ("'Buggies' Collected:  " + playerScore);

        // Stop playing the menu music
        GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<MusicPlayer>().StopMusic();

        // Depending on difficulty, play the associated music!
        if (PlayerPrefs.GetInt("Difficulty") == 1)
        {
            // Hard mode!
            GameObject.FindGameObjectWithTag("HardMusic").GetComponent<MusicPlayer>().PlayMusic();
            GameObject.FindGameObjectWithTag("CasualMusic").GetComponent<MusicPlayer>().StopMusic();
        }
        else {
            GameObject.FindGameObjectWithTag("CasualMusic").GetComponent<MusicPlayer>().PlayMusic();
            GameObject.FindGameObjectWithTag("HardMusic").GetComponent<MusicPlayer>().StopMusic();
        }
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
            if (Time.time - timeSinceBuggy > .01)
            {
                playerScore += 1;
                audioSource.PlayOneShot(audioSource.clip, .5f);
                timeSinceBuggy = Time.time;
            }

            Destroy(trig.gameObject);
        }
    }
}
