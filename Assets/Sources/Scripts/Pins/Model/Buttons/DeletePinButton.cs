using UnityEngine;

public class DeletePinButton : ActionButton
{
    private PinView _pin;

    public void SetPin(PinView pin)
    {
        _pin = pin ?? throw new System.ArgumentNullException(nameof(pin));
    }

    protected override void OnClick()
    {
        if (_pin == null)
            return;

        PinsStorage.Remove(_pin.Pin);
        Destroy(_pin.gameObject);
    }
}
