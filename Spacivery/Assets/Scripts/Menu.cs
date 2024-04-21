using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highscoreText;
    private void Start()
    {
        Time.timeScale = 1f;
        highscoreText.text = "Highscore: " + PlayerPrefs.GetInt("HighScore", 0);
    }
    public void StartScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}