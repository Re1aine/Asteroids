using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class MouseHelper
{
    private static readonly List<RaycastResult> _raycastBuffer = new(10);
    
    public static bool IsOverUI()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            return IsOverUI(Input.GetTouch(0).position);
        
        return IsOverUI(Input.mousePosition);
    }
    
    private static bool IsOverUI(Vector2 screenPosition)
    {
        if (EventSystem.current == null || !EventSystem.current.IsActive())
        {
            Debug.LogWarning("EventSystem is null or not active.");
            return false;
        }
        
        var eventData = new PointerEventData(EventSystem.current)
        {
            position = screenPosition
        };
        
        _raycastBuffer.Clear();
        EventSystem.current.RaycastAll(eventData, _raycastBuffer);
        
        for (int i = 0; i < _raycastBuffer.Count; i++)
        {
            if (_raycastBuffer[i].gameObject.TryGetComponent(out UnityEngine.UI.Graphic graphic))
            {
                if (graphic.raycastTarget && graphic.color.a > 0.01f)
                    return true;
            }
        }
        
        return false;
    }
}