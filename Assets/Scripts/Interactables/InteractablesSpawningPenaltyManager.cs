using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablesSpawningPenaltyManager : MonoBehaviour
{
    [SerializeField] private SpawnableInteractables spawnableInteractables;
    [SerializeField] private InteractableItemsParameters interactableItemsParameters;
    [SerializeField] private InteractablePenalties interactablePenalties;
    
    private int _collectedOxygenCount = 0;
    private float _currentOxygenPenalty = 0;
    
    [HideInInspector] public float _currentDistancePenalty = 0;
    private void OnEnable()
    {
        OxygenItem.OnOxygenCollected += ChangeOxygenPenalty;
    }

    private void OnDisable()
    {
        OxygenItem.OnOxygenCollected -= ChangeOxygenPenalty;
    }

    private void ChangeOxygenPenalty()
    {
        _collectedOxygenCount++;
        if (_collectedOxygenCount % interactablePenalties.oxygenAmountToResetPenalty == 0)
        {
            ResetOxygenPenalty();
            return;
        }

        if (_collectedOxygenCount > interactablePenalties.oxygenAmountToAddDistancePenalty)
            _currentDistancePenalty += interactablePenalties.distancePenalty;
        
        if (interactableItemsParameters.oxygenAmount - _currentOxygenPenalty < interactablePenalties.minimumOxygenAmount)
            _currentOxygenPenalty = interactableItemsParameters.oxygenAmount - interactablePenalties.minimumOxygenAmount;
    }

    public void AddOxygenPenalty(OxygenItem oxygenItem)
    {
        if(_collectedOxygenCount < interactablePenalties.oxygenAmountToStartAddingPenalty)
            return;
        
        _currentOxygenPenalty += interactablePenalties.oxygenPenaltyAmount;
        oxygenItem.OxygenAmount += _currentOxygenPenalty;
    }

    private void ResetOxygenPenalty()
    {
        _currentOxygenPenalty = 0;
    }
}