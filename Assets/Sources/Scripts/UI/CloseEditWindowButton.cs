using UnityEngine;

public class CloseEditWindowButton : ActionButton
{
    [SerializeField] private EditWindow _window;

    protected override void OnClick()
    {
        _window.Close();
    }
}
