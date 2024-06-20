using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEffect : MonoBehaviour
{
    [SerializeField] private bool isPositiveSpeedEffect;
    [SerializeField] private InteractableItemsParameters itemsParameters;
    private float speedBonusPercentage, speedBonusTime;

    public static Action<float, float> AddSpeedEffect;

    private void OnEnable()
    {
        if (isPositiveSpeedEffect)
        {
            speedBonusPercentage = itemsParameters.positiveSpeedBonusPercentage;
            speedBonusTime = itemsParameters.positiveSpeedBonusTime;
        }
        else
        {
            speedBonusPercentage = itemsParameters.negativeSpeedBonusPercentage;
            speedBonusTime = itemsParameters.negativeSpeedBonusTime;
        }
    }

    private void OnTriggerEnter(Collider collided)
    {
        if (!collided.CompareTag("Player")) return;
        
        AddSpeedEffect?.Invoke(speedBonusPercentage, speedBonusTime);
        Destroy(gameObject);
    }
}
