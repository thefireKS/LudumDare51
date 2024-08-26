using System.Collections;
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
        StartCoroutine(ChangedSpeedCoroutine(percentage, effectTime));
    }

    private IEnumerator ChangedSpeedCoroutine(float percentage, float effectTime)
    {
        ChangeSpeed(percentage);
        yield return new WaitForSeconds(effectTime);
        ChangeSpeed(-1f * percentage);
    }

    public void ChangeSpeed(float percentage)
    {
        var newSpeed = defaultPlayerSpeed * ((percentage + 100f) / 100f) - defaultPlayerSpeed;
        playerMovement.moveSpeed += newSpeed;
    }

    public void ChangeSpeedToDefault()
    {
        playerMovement.moveSpeed = defaultPlayerSpeed;
    }
}
