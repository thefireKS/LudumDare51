using System;
using UnityEngine;

public class OxygenItem : MonoBehaviour
{
    [SerializeField] private float oxygenAmount;
    
    public static Action<float> addOxygen;

    private void OnTriggerEnter(Collider collided)
    {
        if(collided.CompareTag("Player"))
            addOxygen?.Invoke(oxygenAmount);
    }
}
