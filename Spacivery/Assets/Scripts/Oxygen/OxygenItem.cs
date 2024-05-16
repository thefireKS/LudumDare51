using System;
using UnityEngine;

public class OxygenItem : Collectable
{
    [SerializeField] private float oxygenAmount;
    
    public static Action<float> addOxygen;

    private void OnTriggerEnter(Collider collided)
    {
        if (!collided.CompareTag("Player")) return;
        
        addOxygen?.Invoke(oxygenAmount);
        addScore?.Invoke(scoreAmount);
        Destroy(gameObject);
    }

    
}
