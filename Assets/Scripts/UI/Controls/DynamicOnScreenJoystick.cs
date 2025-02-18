using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;

public class DynamicOnScreenJoystick : MonoBehaviour, IPointerUpHandler ,IPointerDownHandler, IDragHandler
{
    [SerializeField] private RectTransform joystick;
    [SerializeField] private RectTransform inputHandle;

    private JoystickHandleController joystickHandleController;
    private OnScreenStick onScreenStick;
    
    private IDragHandler dragHandlerImplementation;
    private IPointerDownHandler pointerDownHandlerImplementation;
    private IPointerUpHandler pointerUpHandlerImplementation;

    private void Start()
    {
        joystickHandleController = inputHandle.gameObject.GetComponent<JoystickHandleController>();
        onScreenStick = inputHandle.gameObject.GetComponent<OnScreenStick>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //joystick.gameObject.SetActive(true);
        joystick.position = eventData.pressPosition;
        joystickHandleController.OnPointerDown(eventData);
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        onScreenStick.OnPointerUp(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        joystickHandleController.OnDrag(eventData);
        onScreenStick.OnDrag(eventData);
    }
}
