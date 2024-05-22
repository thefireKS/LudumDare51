using System;
using UnityEngine;

public class DogCollectable : MonoBehaviour
{
    [SerializeField] private float speedBonusPercentage;

    public static Action <float> DogWasTaken;
    public static Action DogWasTakenNotification;

    private void OnTriggerEnter(Collider collided)
    {
        if (!collided.CompareTag("Player")) return;
        
        DogWasTaken?.Invoke(speedBonusPercentage);
        DogWasTakenNotification?.Invoke();
        Destroy(gameObject);
    }
}
