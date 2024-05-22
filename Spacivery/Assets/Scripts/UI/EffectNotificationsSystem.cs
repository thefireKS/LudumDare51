using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectNotificationsSystem : MonoBehaviour
{
    [SerializeField] private GameObject SpeedUpNotification;
    [SerializeField] private Image SpeedUpFillBar;
    [Space(5)] 
    [SerializeField] private GameObject SlowDownNotification;
    [SerializeField] private Image SlowDownFillBar;
    [Space(5)]
    [SerializeField] private GameObject DogCarryingMessage;

    private float speedUpTimer = 0, maximumSpeedUpTime = 0, slowDownTimer = 0, maximumSlowDownTime = 0;
    private void OnEnable()
    {
        SpeedEffect.AddSpeedEffect += EnableSpeedEffectNotification;
        DogCollectable.DogWasTakenNotification += DogNotificationSwitch;
        DogHouseInteraction.DogWasReleased += DogNotificationSwitch;
    }

    private void OnDisable()
    {
        SpeedEffect.AddSpeedEffect -= EnableSpeedEffectNotification;
        DogCollectable.DogWasTakenNotification -= DogNotificationSwitch;
        DogHouseInteraction.DogWasReleased -= DogNotificationSwitch;
    }

    private void Update()
    {
        SpeedUpTimerProgress();
        SlowDownTimerProgress();
    }

    private void EnableSpeedEffectNotification(float speedBonusPercentage, float speedBonusTime)
    {
        if (speedBonusPercentage > 0)
        {
            SpeedUpNotification.SetActive(true);
            SpeedUpFillBar.fillAmount = 1;
            speedUpTimer = speedBonusTime;
            maximumSpeedUpTime = speedBonusTime;
        }
        else
        {
            SlowDownNotification.SetActive(true);
            SlowDownFillBar.fillAmount = 1;
            slowDownTimer = speedBonusTime;
            maximumSlowDownTime = speedBonusTime;
        }
    }

    private void DogNotificationSwitch()
    {
        DogCarryingMessage.SetActive(!DogCarryingMessage.activeSelf);
    }

    private void SpeedUpTimerProgress()
    {
        if (speedUpTimer <= 0)
        {
            SpeedUpNotification.SetActive(false);
            return;
        }
        
        speedUpTimer -= Time.deltaTime;
        SpeedUpFillBar.fillAmount = speedUpTimer / maximumSpeedUpTime;
    }
    
    
    private void SlowDownTimerProgress()
    {
        if (slowDownTimer <= 0)
        {
            SlowDownNotification.SetActive(false);
            return;
        }
        
        slowDownTimer -= Time.deltaTime;
        SlowDownFillBar.fillAmount = slowDownTimer / maximumSlowDownTime;
    }
}