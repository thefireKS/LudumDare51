using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] private GameObject EventStatusHolder;
    [SerializeField] private EventList eventList;

    private EventLogic ongoingEvent = null;
    
    private Dictionary<EventLogic,bool> currentEvents = new Dictionary<EventLogic, bool>();
    
    //TODO: REMOVE THIS, TEMPORARY
    public static Action endGameOnAllQuestsCompleted;

    private void Start()
    {
        StartCoroutine(StartFirstEvent());
    }

    private void OnEnable()
    {
        foreach (var ev in eventList.events)
        {
            currentEvents.Add(ev,false);
        }

        EventLogic.OnEventEnd += MarkEventAsCompleted;
    }

    private void OnDisable()
    {
        EventLogic.OnEventEnd -= MarkEventAsCompleted;
    }

    private IEnumerator StartFirstEvent()
    {
        yield return new WaitForSeconds(eventList.timeToStartEvent);
        StartNextEvent();
    }

    private void MarkEventAsCompleted()
    {
        currentEvents[ongoingEvent] = true;
        StartNextEvent();
    }

    private void StartNextEvent()
    {
        if(GetNextUncompletedEvent() == null)
            endGameOnAllQuestsCompleted?.Invoke();
        else
            ongoingEvent.StartEvent();
    }

    private EventLogic GetNextUncompletedEvent()
    {
        foreach (var ev in currentEvents)
        {
            if (ev.Value) continue;
            ongoingEvent = Instantiate(ev.Key.gameObject,EventStatusHolder.transform).GetComponent<EventLogic>();
            return ongoingEvent;
        }

        return null;
    }
}