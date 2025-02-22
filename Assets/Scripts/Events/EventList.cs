using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventList", menuName = "ScriptableObjects/EventList")]
public class EventList : ScriptableObject
{
    public float timeToStartEvent;
    
    public EventLogic[] events;
}
