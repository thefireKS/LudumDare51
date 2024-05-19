using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedSwamp : MonoBehaviour
{
    [SerializeField] private float speedBonusPercentage;
    private PlayerMovespeedInteraction playerMoveSpeed;

    private void OnEnable()
    {
        playerMoveSpeed = FindObjectOfType<PlayerMovespeedInteraction>();
    }

    private void OnTriggerEnter(Collider other)
    {
        playerMoveSpeed?.ChangeSpeed(speedBonusPercentage);
        Debug.Log("Speed affected");
    }

    private void OnTriggerExit(Collider other)
    {
        playerMoveSpeed?.ChangeSpeedToDefault();
        Debug.Log("Speed is no longer affected");
    }
}
