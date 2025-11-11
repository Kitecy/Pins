using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

public class ContextWindow : MonoBehaviour
{
    private IInputSystem _inputSystem;

    public Vector2 CurrentClickPosition { get; private set; }

    private void Awake()
    {
        _inputSystem = FindFirstObjectByType<PCInputSystem>();
    }

    private void Start()
    {
        _inputSystem.Clicked += OnClicked;
        _inputSystem.Scrolling += OnScrolling;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _inputSystem.Clicked -= OnClicked;
        _inputSystem.Scrolling -= OnScrolling;
    }

    public void Open(Vector2 screenPosition)
    {
        if (screenPosition.x < 0 || screenPosition.y < 0)
            throw new System.InvalidOperationException();

        transform.position = screenPosition;
        gameObject.SetActive(true);
        CurrentClickPosition = screenPosition;
    }

    public void Close()
    {
        gameObject.SetActive(false);
        CurrentClickPosition = Vector2.zero;
    }

    private void OnClicked(Vector2 screenPosition)
    {
        if (UIUtility.IsPointerOverUIAtPosition(screenPosition))
            return;

        if (gameObject.activeInHierarchy)
        {
            Close();
            return;
        }

        if (Raycaster.GetHit(screenPosition).collider != null)
            return;

        Open(screenPosition);
    }

    private void OnScrolling(Vector2 delta)
    {
        Close();
    }
}
