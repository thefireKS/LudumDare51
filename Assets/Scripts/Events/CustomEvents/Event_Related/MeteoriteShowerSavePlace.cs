using System;
using UnityEngine;

public class MeteoriteShowerSavePlace : MonoBehaviour
{
    public static Action onPlayerEnteredSavePlace;
    public static Action onPlayerExitedSavePlace;

    private bool isPlayerInSavePlace;

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player")) return;
        onPlayerEnteredSavePlace?.Invoke();

        isPlayerInSavePlace = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if(!other.CompareTag("Player")) return;
        onPlayerExitedSavePlace?.Invoke();

        isPlayerInSavePlace = false;
    }

    public bool GivePlayerPosition()
    {
        return isPlayerInSavePlace;
    }
}
