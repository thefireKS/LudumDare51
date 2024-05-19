using System;
using UnityEngine;

public class DogHouseInteraction : Collectable
{
    public static Action DogWasReleased;
    private void OnTriggerEnter(Collider collided)
    {
        if (!collided.CompareTag("Player") && !PlayerDogCarrying.hasDog) return;
        
        AddScore?.Invoke(scoreAmount);
        DogWasReleased?.Invoke();
        Destroy(gameObject);
    }
}
