using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraBounds : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    public Rect GetBounds()
    {
        float height = _camera.orthographicSize * 2f;
        float width = height * _camera.aspect;

        float left = _camera.transform.position.x - width / 2f;
        float right = _camera.transform.position.x + width / 2f;
        float bottom = _camera.transform.position.y - height / 2f;
        float top = _camera.transform.position.y + height / 2f;

        return new Rect(left, bottom, width, height);
    }
}
