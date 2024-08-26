using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FocusOnEndGameButton : MonoBehaviour
{
    [SerializeField] private Button PlayAgainButton;
    private void OnEnable()
    {
        #if !UNITY_ANDROID
        PlayAgainButton.Select();
        #endif
    }
}
