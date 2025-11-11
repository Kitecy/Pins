using System;
using UnityEngine;

public interface IInputSystem
{
    event Action<Vector2> Scrolling;
    event Action<Vector2> Holded;
    event Action<Vector2> Clicked;
    event Action HoldEnded;

    Vector2 MousePosition { get; }

    void FreezeScrolling();

    void UnfreezeScrolling();
}
