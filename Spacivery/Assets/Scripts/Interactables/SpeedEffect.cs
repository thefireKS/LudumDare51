using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEffect : MonoBehaviour
{
    [SerializeField] private float speedBonusPercentage;
    [SerializeField] private float speedBonusTime;

    public static Action<float, float> AddSpeedEffect;
    
    private void OnTriggerEnter(Collider collided)
    {
        if (!collided.CompareTag("Player")) return;
        
        AddSpeedEffect?.Invoke(speedBonusPercentage, speedBonusTime);
        Destroy(gameObject);
    }
}
