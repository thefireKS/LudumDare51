using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescueTheDogEvent : EventLogic
{
    public static Action spawnEventDog;

    private void OnEnable()
    {
        DogHouseInteraction.DogWasReleased += EndEvent;
    }

    private void OnDisable()
    {
        DogHouseInteraction.DogWasReleased -= EndEvent;
    }

    public override void StartEvent()
    {
        OnEventStart?.Invoke();
        spawnEventDog?.Invoke();
    }

    public override void EndEvent()
    {
        OnEventEnd?.Invoke();

        DogHouseInteraction.DogWasReleased -= EndEvent;
        Destroy(transform.gameObject);
    }
}
