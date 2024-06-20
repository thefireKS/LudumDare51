using System;
using UnityEngine;

public class DogHouseInteraction : Collectable
{
    [SerializeField] private InteractableItemsParameters itemsParameters;
    
    public static Action DogWasReleased;

    private void OnEnable()
    {
        scoreAmount = itemsParameters.scoreOnDogDelivery;
    }

    private void OnTriggerEnter(Collider collided)
    {
        if (!collided.CompareTag("Player")) return;
        if (!PlayerDogCarrying.hasDog) return;
        
        AddScore?.Invoke(scoreAmount);
        DogWasReleased?.Invoke();
        Destroy(gameObject);
    }
}
