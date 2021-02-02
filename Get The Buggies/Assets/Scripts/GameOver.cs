using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void Start() {
        Destroy(GameObject.FindGameObjectWithTag("MenuMusic"));
        //GameObject.FindGameObjectWithTag("HardMusic").GetComponent<MusicPlayer>().StopMusic();
        Destroy(GameObject.FindGameObjectWithTag("HardMusic"));
        //GameObject.FindGameObjectWithTag("CasualMusic").GetComponent<MusicPlayer>().StopMusic();
        Destroy(GameObject.FindGameObjectWithTag("CasualMusic"));
    } 
        
    public void ReplayGame() {
        SceneManager.LoadScene(0);
        
    }

    public void QuitGame() {
        Application.Quit();
    }
}
