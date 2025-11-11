using UnityEngine;

[RequireComponent(typeof(IInputSystem))]
public class CameraMover : MonoBehaviour
{
    [SerializeField] private CameraBounds _bounds;
    [SerializeField] private SpriteRenderer _background;
    [SerializeField] private Transform _minPosition;
    [SerializeField] private Transform _maxPosition;

    private IInputSystem _inputSystem;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
        _inputSystem = GetComponent<IInputSystem>();
    }

    private void OnEnable()
    {
        _inputSystem.Scrolling += OnScrolling;
    }

    private void LateUpdate()
    {
        ClampPosition();
    }

    private void OnDisable()
    {
        _inputSystem.Scrolling -= OnScrolling;
    }

    private void OnScrolling(Vector2 move)
    {
        _transform.position = new Vector3(_transform.position.x + move.x, _transform.position.y + move.y, _transform.position.z);
    }

    private void ClampPosition()
    {
        Bounds bounds = _background.bounds;
        Rect cameraRect = _bounds.GetBounds();

        float cameraHalfWidth = cameraRect.width / 2f;
        float cameraHalfHeight = cameraRect.height / 2f;

        float minX = bounds.min.x + cameraHalfWidth;
        float maxX = bounds.max.x - cameraHalfWidth;
        float minY = bounds.min.y + cameraHalfHeight;
        float maxY = bounds.max.y - cameraHalfHeight;

        Vector3 pos = _transform.position;

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        _transform.position = pos;
    }
}
