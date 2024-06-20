using System;
using UnityEngine;

public class Star : Collectable
{
    [SerializeField] private InteractableItemsParameters itemsParameters;

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
