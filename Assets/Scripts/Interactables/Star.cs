using System;
using UnityEngine;

public class Star : Collectable
{
    private void OnEnable()
    {
        scoreAmount = itemsParameters.scoreOnStarCollected;
    }

    private void OnTriggerEnter(Collider collided)
    {
        if (!collided.CompareTag("Player")) return;
        
        AddScore?.Invoke(scoreAmount);
        Destroy(gameObject);
    }
}
