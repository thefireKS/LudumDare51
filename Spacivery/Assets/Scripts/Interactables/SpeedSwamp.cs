using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedSwamp : MonoBehaviour
{
    [SerializeField] private InteractableItemsParameters itemsParameters;

    private PlayerMovespeedInteraction playerMoveSpeed;

    private void OnEnable()
    {
        playerMoveSpeed = FindObjectOfType<PlayerMovespeedInteraction>();
    }

    private void OnTriggerEnter(Collider other)
    {
        playerMoveSpeed?.ChangeSpeed(itemsParameters.swampSpeedBonusPercentage);
        Debug.Log("Speed affected");
    }

    private void OnTriggerExit(Collider other)
    {
        playerMoveSpeed?.ChangeSpeed(-1f * itemsParameters.swampSpeedBonusPercentage);
        Debug.Log("Speed is no longer affected");
    }
}
