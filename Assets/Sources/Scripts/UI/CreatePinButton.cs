using UnityEngine;

public class CreatePinButton : ActionButton
{
    [SerializeField] private ContextWindow _window;
    [SerializeField] private string _defaultHeader;
    [SerializeField] private string _defaultDescription;

    protected override void OnClick()
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(_window.CurrentClickPosition);
        PinsHandler.Instance.Create(worldPosition, _defaultHeader, _defaultDescription);
        _window.Close();
    }
}
