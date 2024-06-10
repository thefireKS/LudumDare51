using System;
using UnityEngine;

public class OxygenItem : Collectable
{
    [SerializeField] private float oxygenAmount;

    public static Action OnOxygenCollected;
    public static Action<float> AddOxygen;

    private void OnTriggerEnter(Collider collided)
    {
        if (!collided.CompareTag("Player")) return;

        OnOxygenCollected?.Invoke();
        AddOxygen?.Invoke(oxygenAmount);
        AddScore?.Invoke(scoreAmount);
        Destroy(gameObject);
    }
}
