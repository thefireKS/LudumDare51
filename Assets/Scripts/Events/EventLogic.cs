using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class EventLogic : MonoBehaviour
{
    public static Action OnEventStart, OnEventEnd;

    public abstract void StartEvent();

    public abstract void EndEvent();
}
