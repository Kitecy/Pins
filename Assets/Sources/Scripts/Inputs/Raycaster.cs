using UnityEngine;

public static class Raycaster
{
    private static (Vector2 screenPosition, RaycastHit2D hit) _cachedHit = new();

    public static RaycastHit2D GetHit(Vector2 screenPosition)
    {
        if (screenPosition == _cachedHit.screenPosition)
            return _cachedHit.hit;

        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        _cachedHit.screenPosition = screenPosition;
        _cachedHit.hit = hit;
        return hit;
    }
}
