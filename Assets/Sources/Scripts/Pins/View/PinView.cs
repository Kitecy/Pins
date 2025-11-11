using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
public class PinView : MonoBehaviour
{
    [SerializeField] private TMP_Text _header;

    private Pin _pin = null;

    public Pin Pin => _pin;

    private void OnDisable()
    {
        if (_pin != null)
            _pin.Updated -= UpdateInfo;
    }

    public void SetPin(Pin pin)
    {
        if (_pin != null)
            throw new System.InvalidOperationException("The pin has already been set and cannot be changed for this object.");

        _pin = pin;
        _pin.Updated += UpdateInfo;
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        if (_pin == null)
            return;

        _header.text = _pin.Header;
    }
}
