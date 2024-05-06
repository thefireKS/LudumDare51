using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class OxygenManager : MonoBehaviour
{
    [Header("UI objects")]
    [SerializeField] private GameObject EndGameMenu;
    [SerializeField] private GameObject ScoreUI;
    [SerializeField] private Image oxygenLeftBar;
    
    [Header("Text fields")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    
    [Header("Player related")]
    [SerializeField] private PlayerPointerArrow playerPointArrow;
    [SerializeField] private float oxygenTime;

    private int score;
    private float timer;

    private void OnEnable()
    {
        timer = oxygenTime;

        OxygenItem.addOxygen += OxygenCollected;
    }

    private void OnDisable()
    {
        OxygenItem.addOxygen -= OxygenCollected;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
            GameEnd();

        oxygenLeftBar.fillAmount = timer / oxygenTime;
    }
    
    private void OxygenCollected(float oxygenGivenTime)
    {
        int bonus = (int) (timer % oxygenTime);
        score += 1 + bonus;

        timer += oxygenGivenTime;
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
