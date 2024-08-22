using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highscoreText;
    [SerializeField] private Button startButton;
    private void Start()
    {
        Time.timeScale = 1f;
        highscoreText.text = "Highscore: " + PlayerPrefs.GetInt("HighScore", 0);

        #if !UNITY_ANDROID
        startButton.Select();
        #endif
    }
    public void StartScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}