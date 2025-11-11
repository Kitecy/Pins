using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PinsHandler : MonoBehaviour
{
    [SerializeField] private EditWindow _editWindow;
    [SerializeField] private PinView _pinViewPrefab;

    private IInputSystem _inputSystem;

    private PinView _replacingPin;

    public static PinsHandler Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        _inputSystem = FindFirstObjectByType<PCInputSystem>();

        PinsStorage.Load();

        string jsonData = PlayerPrefs.GetString("Pins");

        IReadOnlyList<Pin> pins = PinsStorage.Pins;

        for (int i = 0; i < pins.Count; i++)
        {
            Pin pin = pins[i];
            Load(pin);
        }
    }

    private void Update()
    {
        if (_replacingPin == null)
            return;

        _replacingPin.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(_inputSystem.MousePosition);
    }

    private void OnEnable()
    {
        _inputSystem.Clicked += OnClick;
        _inputSystem.Holded += OnHolded;
        _inputSystem.HoldEnded += OnHoldEnded;
    }

    private void OnDisable()
    {
        _inputSystem.Clicked -= OnClick;
        _inputSystem.Holded -= OnHolded;
        _inputSystem.HoldEnded -= OnHoldEnded;
    }

    private void OnApplicationQuit()
    {
        PinsStorage.Save();
    }

    public void Create(Vector2 position, string header, string description)
    {
        PinView pinView = Instantiate(_pinViewPrefab, position, Quaternion.identity);
        Pin pin = new Pin(position, header, description);
        pinView.SetPin(pin);
        PinsStorage.Add(pin);
    }

    public void Delete(Pin pin)
    {
        PinsStorage.Remove(pin);
    }

    private void Load(Pin pin)
    {
        PinView pinView = Instantiate(_pinViewPrefab, pin.Position, Quaternion.identity);
        pinView.SetPin(pin);
    }

    private void OnClick(Vector2 screenPosition)
    {
        if (UIUtility.IsPointerOverUIAtPosition(screenPosition))
            return;

        RaycastHit2D hit = Raycaster.GetHit(screenPosition);

        if (_editWindow.IsOpened)
        {
            _editWindow.Close();
            return;
        }

        if (hit.collider == null)
            return;

        if (hit.collider.TryGetComponent(out PinView pinView) == false)
            return;

        _editWindow.Open(pinView, screenPosition);
    }

    private void OnHolded(Vector2 screenPosition)
    {
        RaycastHit2D hit = Raycaster.GetHit(screenPosition);

        if (hit.collider == null)
            return;

        if (hit.collider.TryGetComponent(out PinView pinView) == false)
            return;

        _inputSystem.FreezeScrolling();

        _replacingPin = pinView;
    }

    private void OnHoldEnded()
    {
        if (_replacingPin == null)
            return;

        _replacingPin.Pin.SetPosition(_replacingPin.transform.position);
        _replacingPin = null;

        _inputSystem.UnfreezeScrolling();
    }
}
