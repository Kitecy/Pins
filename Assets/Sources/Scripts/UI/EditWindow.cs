using TMPro;
using UnityEngine;

public class EditWindow : MonoBehaviour
{
    [SerializeField] private TMP_InputField _headerInputField;
    [SerializeField] private TMP_InputField _descriptionInputField;
    [SerializeField] private DeletePinButton _deletebutton;

    private PinView _pinView;

    public bool IsOpened => gameObject.activeInHierarchy;

    public void Open(PinView pin, Vector2 screenPosition)
    {
        gameObject.SetActive(true);
        transform.position = screenPosition;

        _pinView = pin ?? throw new System.ArgumentNullException(nameof(pin));

        _deletebutton.SetPin(_pinView);

        _headerInputField.onValueChanged.AddListener(_pinView.Pin.SetHeader);
        _descriptionInputField.onValueChanged.AddListener(_pinView.Pin.SetDescription);

        ShowInfo();
    }

    public void Close()
    {
        gameObject.SetActive(false);

        _headerInputField.onValueChanged.RemoveAllListeners();
        _descriptionInputField.onValueChanged.RemoveAllListeners();
    }

    private void ShowInfo()
    {
        _headerInputField.SetTextWithoutNotify(_pinView.Pin.Header);
        _descriptionInputField.SetTextWithoutNotify(_pinView.Pin.Description);
    }
}
