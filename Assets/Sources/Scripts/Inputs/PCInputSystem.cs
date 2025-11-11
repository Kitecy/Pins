using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class PCInputSystem : MonoBehaviour, IInputSystem
{
    [SerializeField] private InputSystemUIInputModule _inputModule;
    [SerializeField, Min(0.01f)] private float _sensivity = 0.01f;
    [SerializeField] private float _deathZone = 0.01f;
    [SerializeField] private bool _inverted;

    private InputSystem _inputSystem;

    private bool _isScrolling;
    private Vector2 _startPosition;

    private bool _scrollingFreezed;

    private Mouse _mouse;

    public event Action<Vector2> Scrolling;
    public event Action<Vector2> Holded;
    public event Action HoldEnded;
    public event Action<Vector2> Clicked;

    public Vector2 MousePosition => _mouse.position.ReadValue();

    private void Awake()
    {
        _inputSystem = new InputSystem();
        _mouse = Mouse.current;
    }

    private void OnEnable()
    {
        _inputSystem.Enable();
        _inputSystem.Player.Click.performed += OnClick;
        _inputSystem.Player.Hold.performed += OnHold;
        _inputSystem.Player.Press.started += OnPressStarted;
        _inputSystem.Player.Press.canceled += OnPressEnded;
        _inputSystem.Player.Position.performed += OnPositionPerformed;
        _inputSystem.Player.Hold.canceled += OnHoldCanceled;
    }

    private void OnDisable()
    {
        _inputSystem.Disable();
        _inputSystem.Player.Click.performed -= OnClick;
        _inputSystem.Player.Hold.performed -= OnHold;
    }

    public void FreezeScrolling()
    {
        _scrollingFreezed = true;
    }

    public void UnfreezeScrolling()
    {
        _scrollingFreezed = false;
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        Vector2 screenPosition = _mouse.position.ReadValue();
        Clicked?.Invoke(screenPosition);
    }

    private void OnHold(InputAction.CallbackContext context)
    {
        Holded?.Invoke(_mouse.position.ReadValue());
    }

    private void OnPositionPerformed(InputAction.CallbackContext context)
    {
        if (_isScrolling == false || _scrollingFreezed)
            return;

        Vector2 current = context.ReadValue<Vector2>();
        Vector2 difference = _startPosition - current;

        if (difference.sqrMagnitude > _deathZone)
        {
            _startPosition = current;
            Scrolling?.Invoke(_inverted ? difference * _sensivity : -difference * _sensivity);
        }
    }

    private void OnPressStarted(InputAction.CallbackContext context)
    {
        _isScrolling = true;
        _startPosition = _mouse.position.ReadValue();
    }

    private void OnPressEnded(InputAction.CallbackContext context)
    {
        _isScrolling = false;
    }

    private void OnHoldCanceled(InputAction.CallbackContext context)
    {
        HoldEnded?.Invoke();
    }
}
