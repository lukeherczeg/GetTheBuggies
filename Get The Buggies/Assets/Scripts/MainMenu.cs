using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    void Update() {
        GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<MusicPlayer>().PlayMusic();
    }
    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void HardMode() {
        PlayerPrefs.SetInt("Difficulty", 1);
        PlayGame();
    }

    public void CasualMode()
    {
        PlayerPrefs.SetInt("Difficulty", 0);
        PlayGame();
    }

}
