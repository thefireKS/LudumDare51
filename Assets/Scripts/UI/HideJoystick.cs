using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideJoystick : MonoBehaviour
{
    private void Awake()
    {
        #if !UNITY_ANDROID
            gameObject.SetActive(false);
        #endif
    }
}
