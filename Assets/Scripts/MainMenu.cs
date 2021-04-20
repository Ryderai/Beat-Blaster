using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public TextMeshProUGUI highscore;

    void Start() {
        highscore.SetText("Highscore: " + PlayerPrefs.GetInt("HighScore", 0));
    }

    public void PlayGame() {
        SceneManager.LoadScene(1);
    }

    public void QuitGame() {
        Application.Quit();
    }

}