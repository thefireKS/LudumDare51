using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class MeteorShowerEvent : EventLogic
{
    [Header("Game Parameters")]
    [SerializeField] private GameObject savePlace;
    [SerializeField] private float timeToFindSavePlace;
    [SerializeField] private float timeToMeteoriteShowerToGo;
    [Space(10)] 
    [SerializeField, Range(1, 5)] private float oxygenPenalty;
    [Space(20), Header("Event UI")] 
    [SerializeField] private Image countDownSlider;
    [SerializeField] private TextMeshProUGUI textCountDown;
    [SerializeField] private TextMeshProUGUI eventDescription;
    [Space(10)] 
    [SerializeField] private string beforeShowerText;
    [SerializeField] private string duringShowerText;

    private MeteoriteShowerSavePlace cachedSavePlace;
    private bool isShowerGoing;

    public static Action checkPlayerPosition;
    public static Action<float> affectPlayerOxygen;
    
    private void OnEnable()
    {
        InteractablesSpawningManager.GiveRandomPosition += InstantiateSavePlace;

        MeteoriteShowerSavePlace.onPlayerEnteredSavePlace += AffectPlayerOnSavePlaceEntered;
        MeteoriteShowerSavePlace.onPlayerExitedSavePlace += AffectPlayerOnSavePlaceExited;
    }

    private void OnDisable()
    {
        InteractablesSpawningManager.GiveRandomPosition -= InstantiateSavePlace;
        
        MeteoriteShowerSavePlace.onPlayerEnteredSavePlace -= AffectPlayerOnSavePlaceEntered;
        MeteoriteShowerSavePlace.onPlayerExitedSavePlace -= AffectPlayerOnSavePlaceExited;
    }

    public override void StartEvent()
    {
        GetInstancePosition?.Invoke();
        
        StartCoroutine(MeteoriteShowerCountdown());
        
        eventDescription.text = beforeShowerText;
        textCountDown.text = ((int)timeToFindSavePlace).ToString();
    }

    public override void EndEvent()
    {
        Destroy(cachedSavePlace);
        cachedSavePlace = null;
        
        Destroy(transform.gameObject);
    }

    private void InstantiateSavePlace(Vector3 position)
    {
        cachedSavePlace = Instantiate(savePlace, position, quaternion.identity).GetComponent<MeteoriteShowerSavePlace>();
    }

    private IEnumerator MeteoriteShowerCountdown()
    {
        var currentTime = timeToFindSavePlace;
        var previousInteger = timeToFindSavePlace;
        
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            countDownSlider.fillAmount = currentTime / timeToFindSavePlace;
            yield return null;
            
            if (!(currentTime < previousInteger - 1)) continue;
            textCountDown.text = ((int)currentTime).ToString();
            previousInteger -= 1;
        }

        StartCoroutine(MeteoriteShowerOngoing());
        eventDescription.text = duringShowerText;
    }

    private IEnumerator MeteoriteShowerOngoing()
    {
        isShowerGoing = true;
        AffectPlayerOnMeteorShowerStart(cachedSavePlace.GivePlayerPosition());
        
        var currentTime = timeToMeteoriteShowerToGo;
        var previousInteger = timeToMeteoriteShowerToGo;
        
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            countDownSlider.fillAmount = currentTime / timeToMeteoriteShowerToGo;
            //idk shake camera god save me
            yield return null;
            
            if (!(currentTime < previousInteger - 1)) continue;
            textCountDown.text = ((int)currentTime).ToString();
            previousInteger -= 1;
        }

        isShowerGoing = false;
        EndEvent();
    }

    private void AffectPlayerOnSavePlaceEntered()
    {
        if(!isShowerGoing) return;
        affectPlayerOxygen?.Invoke(1);
    }

    private void AffectPlayerOnSavePlaceExited()
    {
        if(!isShowerGoing) return;
        affectPlayerOxygen?.Invoke(oxygenPenalty);
    }

    private void AffectPlayerOnMeteorShowerStart(bool shouldAffect)
    {
        if (!shouldAffect)
            AffectPlayerOnSavePlaceExited();
    }
}
