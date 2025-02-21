using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;

public class JoystickHandleController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,  IPointerDownHandler
{
    private RectTransform stickRectTransform;
    
    [SerializeField] private float stickSpeed;
    [SerializeField] private float moveHandlePenalty;
    [Space(10)]
    [SerializeField] private RectTransform stickZoneRectTransform;
    [SerializeField] private RectTransform stickParentRectTransform;
    
    private IPointerDownHandler pointerDownHandlerImplementation;
    private Vector2 handlePosition, pointerPosition;
    private bool isDragged = false;
    
    // Start is called before the first frame update
    void Start()
    {
        stickRectTransform = GetComponent<RectTransform>();
        
        handlePosition = new Vector2();
        pointerPosition = new Vector2();
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.LogWarning("Pointer Down");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragged = true;
    }
    public void OnDrag(PointerEventData eventData)
    {
        pointerPosition.x = eventData.position.x;
        pointerPosition.y = eventData.position.y;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragged = false;
    }

    private void Update()
    {
        if (!isDragged) return;
        
        handlePosition.x = stickRectTransform.position.x; 
        handlePosition.y = stickRectTransform.position.y;
        
        //Debug.Log((pointerPosition - handlePosition).magnitude);
        
        if((pointerPosition - handlePosition).magnitude > moveHandlePenalty)
        {
            stickParentRectTransform.anchoredPosition += calculateDirection() * (stickSpeed * Time.deltaTime);
        }
    }

    private Vector2 calculateDirection()
    {
        var direction = (stickRectTransform.anchoredPosition - stickZoneRectTransform.rect.center).normalized;
        return direction;
    }
}
