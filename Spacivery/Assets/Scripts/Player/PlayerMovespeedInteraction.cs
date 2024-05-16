using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovespeedInteraction : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private float defaultPlayerSpeed;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        defaultPlayerSpeed = playerMovement.moveSpeed;
    }

    private void OnEnable() => SpeedEffect.AddSpeedEffect += ChangeSpeedTimedEffect;

    private void OnDisable() => SpeedEffect.AddSpeedEffect -= ChangeSpeedTimedEffect;
    
    private void ChangeSpeedTimedEffect(float percentage, float effectTime)
    {
        ChangeSpeed(percentage);
        StartCoroutine(ChangedSpeedCoroutine(effectTime));
    }

    private IEnumerator ChangedSpeedCoroutine(float effectTime)
    {
        yield return new WaitForSeconds(effectTime);
        ChangeSpeedToDefault();
    }

    public void ChangeSpeed(float percentage)
    {
        playerMovement.moveSpeed *= (percentage + 100f) / 100f;
    }

    public void ChangeSpeedToDefault()
    {
        playerMovement.moveSpeed = defaultPlayerSpeed;
    }
}
