using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractablePenalties", menuName = "ScriptableObjects/InteractablePenalties")]
public class InteractablePenalties : ScriptableObject
{
    [Header("Oxygen penalties parameters")]
    public float oxygenPenaltyAmount = 0.1f;
    public int oxygenAmountToResetPenalty = 15;
    public float minimumOxygenAmount = 2f;

    [Space(5),Header("Distance penalty parameters")]
    public int oxygenAmountToAddDistancePenalty = 15;
    public float distancePenalty = 100f;
}
