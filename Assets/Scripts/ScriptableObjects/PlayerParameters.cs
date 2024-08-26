using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerParameters", menuName = "ScriptableObjects/PlayerParameters")]
public class PlayerParameters : ScriptableObject
{
    [Header("Oxygen")]
    public float oxygenTime;
    public float maximumOxygenTime;
    [Space(1)]
    [Range(0,1)] public float extraLastTimeSlowDown;

    [Space(5)] [Header("Movement")] 
    public float minimumMoveSpeed;
    public float moveSpeed;
    public float rotationSpeed;
}
