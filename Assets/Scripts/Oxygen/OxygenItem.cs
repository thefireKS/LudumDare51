using System;
using UnityEngine;

public class OxygenItem : Collectable
{
    [SerializeField] private InteractableItemsParameters itemsParameters;

    [HideInInspector] public float OxygenAmount = 0f;

    public static Action OnOxygenCollected;
    public static Action<float> AddOxygen;

    private void OnEnable()
    {
        scoreAmount = itemsParameters.scoreOnOxygenCollected;
        OxygenAmount += itemsParameters.oxygenAmount;
    }

    private void OnTriggerEnter(Collider collided)
    {
        if (!collided.CompareTag("Player")) return;

        OnOxygenCollected?.Invoke();
        AddOxygen?.Invoke(OxygenAmount);
        AddScore?.Invoke(scoreAmount);
        Destroy(gameObject);
    }
}
