using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectOxygenBallonsEvent : EventLogic
{
    [SerializeField] private TextMeshProUGUI eventText;
    [SerializeField] private int target = 10;
    private int currentOxygenCollected = 0;

    private void OnEnable()
    {
        OxygenItem.OnOxygenCollected += OnOxygenCollected;
    }

    private void OnDisable()
    {
        OxygenItem.OnOxygenCollected -= OnOxygenCollected;
    }

    public override void StartEvent()
    {
        eventText.text = currentOxygenCollected + " / " + target;

        OnEventStart?.Invoke();
    }

    public override void EndEvent()
    {
        OnEventEnd?.Invoke();

        OxygenItem.OnOxygenCollected -= OnOxygenCollected;
        Destroy(transform.gameObject);
    }

    private void OnOxygenCollected()
    {
        currentOxygenCollected += 1;
        
        if(currentOxygenCollected >= target)
            EndEvent();
        
        eventText.text = currentOxygenCollected + " / " + target;
    }
}
