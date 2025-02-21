using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "PlayerParameters", menuName = "ScriptableObjects/PlayerParameters")]
public class PlayerParameters : ScriptableObject
{
    [Header("Oxygen")]
    public float preGameOxygenTimeAmount;
    public float maximumOxygenTime;
    [Space(5)] 
    [Header("Time related")] 
    public float timeWhenStartExtraLastTimeSlowDown;
    [Range(0, 2)] public float oxygenConsumedMultiplier;
    [Range(0,1)] public float extraLastTimeSlowDown;

    [Space(5)] [Header("Movement")] 
    public float minimumMoveSpeed;
    public float moveSpeed;
    public float rotationSpeed;
}
