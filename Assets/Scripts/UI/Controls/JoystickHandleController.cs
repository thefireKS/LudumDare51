using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;

public class JoystickHandleController : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    private RectTransform stickRectTransform;
    
    [SerializeField] private float stickSpeed;
    [Space(10)]
    [SerializeField] private RectTransform stickZoneRectTransform;
    [SerializeField] private RectTransform stickParentRectTransform;
    private IPointerDownHandler pointerDownHandlerImplementation;
    
    // Start is called before the first frame update
    void Start()
    {
        stickRectTransform = GetComponent<RectTransform>();
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.LogWarning("Pointer Down");
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.LogError(calculateDirection());
        stickParentRectTransform.anchoredPosition += calculateDirection() * stickSpeed * Time.deltaTime;
    }

    private Vector2 calculateDirection()
    {
        var direction = (stickRectTransform.anchoredPosition - stickZoneRectTransform.rect.center).normalized;
        return direction;
    }
}
