using System;
using UnityEngine;

public class OxygenItem : Collectable
{
    [SerializeField] private InteractableItemsParameters itemsParameters;

    public static Action OnOxygenCollected;
    public static Action<float> AddOxygen;

    private void OnEnable()
    {
        scoreAmount = itemsParameters.scoreOnOxygenCollected;
    }

    private void OnTriggerEnter(Collider collided)
    {
        if (!collided.CompareTag("Player")) return;

        OnOxygenCollected?.Invoke();
        AddOxygen?.Invoke(itemsParameters.oxygenAmount);
        AddScore?.Invoke(scoreAmount);
        Destroy(gameObject);
    }
}
