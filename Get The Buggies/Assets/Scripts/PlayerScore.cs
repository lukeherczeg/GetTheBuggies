using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    private int playerScore = 0;
    public GameObject playerScoreUI;

    void Update()
    {
        playerScoreUI.gameObject.GetComponent<Text>().text = ("'Buggies' Gotten: " + playerScore);
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
            Destroy(trig.gameObject);
        }
    }
}
