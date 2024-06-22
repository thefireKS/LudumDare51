using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractableItemsParameters", menuName = "ScriptableObjects/InteractableItemsParameters")]
public class InteractableItemsParameters : ScriptableObject
{
    [Header("Oxygen")] 
    [Min(1)]public float oxygenAmount;
    public int scoreOnOxygenCollected;

    [Space(5), Header("Star")] 
    public int scoreOnStarCollected;

    [Space(5), Header("Speed and Slow boosts")]
    public float positiveSpeedBonusPercentage;
    public float positiveSpeedBonusTime;
    [Space(5)]
    public float negativeSpeedBonusPercentage;
    public float negativeSpeedBonusTime;

    [Space(5), Header("Swamp")] 
    public float swampSpeedBonusPercentage;

    [Space(5), Header("Dog and Dog House")]
    public float dogSpeedBonusPercentage;
    public int scoreOnDogDelivery;
}
