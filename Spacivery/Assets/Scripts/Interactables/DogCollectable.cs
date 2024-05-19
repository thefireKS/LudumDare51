using System;
using UnityEngine;

public class DogCollectable : MonoBehaviour
{
    [SerializeField] private float speedBonusPercentage;

    public static Action <float> DogWasTaken;

    private void OnTriggerEnter(Collider collided)
    {
        if (!collided.CompareTag("Player")) return;
        
        DogWasTaken?.Invoke(speedBonusPercentage);
        Destroy(gameObject);
    }
}
