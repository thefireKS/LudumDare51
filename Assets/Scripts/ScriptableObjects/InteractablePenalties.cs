using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractablePenalties", menuName = "ScriptableObjects/InteractablePenalties")]
public class InteractablePenalties : ScriptableObject
{
    [Header("Oxygen penalties parameters")]
    [Header("Oxygen in each balloon")]
    [Tooltip("Must be negative")]
    public float oxygenPenaltyAmount = -0.1f;
    public float minimumOxygenAmount = 2f;
    [Header("Collected oxygen balloons counters")]
    public int oxygenAmountToStartAddingPenalty = 5;
    public int oxygenAmountToResetPenalty = 15;

    [Space(5),Header("Distance penalty parameters")]
    public int oxygenAmountToAddDistancePenalty = 15;
    public float distancePenalty = 100f;
}
