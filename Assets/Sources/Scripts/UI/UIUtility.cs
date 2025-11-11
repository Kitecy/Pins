using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class UIUtility
{
    public static bool IsPointerOverUIAtPosition(Vector2 screenPosition)
    {
        if (EventSystem.current == null)
            return false;

        var pointerData = new PointerEventData(EventSystem.current)
        {
            position = screenPosition
        };

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);
        return results.Count > 0;
    }
}
