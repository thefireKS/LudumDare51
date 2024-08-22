using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button continueButton;

    private void Start() => PlayerInputManager.playerControls.Default.Pause.started += OpenPauseMenuAction;
    private void OnDisable() => PlayerInputManager.playerControls.Default.Pause.started -= OpenPauseMenuAction;

    private void OpenPauseMenuAction(InputAction.CallbackContext callbackContext)
    {
        OpenPauseMenu();
    }

    public void OpenPauseMenu()
    {
        #if !UNITY_ANDROID
        continueButton.Select();
        #endif
        
        if (pausePanel.activeSelf)
        {
            Continue();
            return;
        }
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }
    public void Continue()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }
    
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
