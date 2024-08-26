using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class OxygenAndPointsManager : MonoBehaviour
{
    [Header("UI objects")]
    [SerializeField] private GameObject EndGameMenu;
    [SerializeField] private GameObject ScoreUI;
    [SerializeField] private Image oxygenLeftBar;
    
    [Header("Text fields")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    [Header("Player related")] [SerializeField]
    private PlayerParameters playerParameters;

    public static Action<int> ScoreChanged;
    
    private int _score;
    private int score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            ScoreChanged?.Invoke(_score);
        }
    }
    
    private float timer;

    private void OnEnable()
    {
        timer = playerParameters.oxygenTime;

        OxygenItem.AddOxygen += OxygenCollected;
        Collectable.AddScore += AddScoreOnCollectableCollected;
    }

    private void OnDisable()
    {
        OxygenItem.AddOxygen -= OxygenCollected;
        Collectable.AddScore -= AddScoreOnCollectableCollected;
    }

    private void Update()
    {
        if (timer > 3f)
            timer -= Time.deltaTime;
        else
            timer -= Time.deltaTime * playerParameters.extraLastTimeSlowDown;
        
        if (timer <= 0f)
            GameEnd();

        oxygenLeftBar.fillAmount = timer / playerParameters.maximumOxygenTime;
        scoreText.text = "SCORE: " + score;
    }
    
    private void OxygenCollected(float oxygenGivenTime)
    {
        timer += oxygenGivenTime;
        if (timer > playerParameters.maximumOxygenTime)
            timer = playerParameters.maximumOxygenTime;
    }
    
    private void AddScoreOnCollectableCollected(int scoreGiven)
    {
        score += scoreGiven;
    }
    
    private void GameEnd()
    {
        Time.timeScale = 0f;
        
        finalScoreText.text = "Your score: " + score;
        
        ScoreUI.SetActive(false);
        
        EndGameMenu.SetActive(true);
        
        if (score > PlayerPrefs.GetInt("HighScore", 0))
            PlayerPrefs.SetInt("HighScore", score);
    }
}
