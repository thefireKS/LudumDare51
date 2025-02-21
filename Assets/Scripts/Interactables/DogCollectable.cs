using System;
using UnityEngine;

public class DogCollectable : Collectable
{
    public static Action <float> DogWasTaken;
    public static Action DogWasTakenNotification;

    private void OnTriggerEnter(Collider collided)
    {
        if (!collided.CompareTag("Player")) return;
        
        DogWasTaken?.Invoke(itemsParameters.dogSpeedBonusPercentage);
        DogWasTakenNotification?.Invoke();
        Destroy(gameObject);
    }
}
