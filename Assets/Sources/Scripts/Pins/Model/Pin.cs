using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable]
public class Pin
{
    [JsonProperty("Position")] private Vector2ForJson _position;

    [JsonConstructor]
    public Pin(Vector2ForJson position, string header, string description)
    {
        if (string.IsNullOrWhiteSpace(header))
            throw new ArgumentNullException(nameof(header));

        _position = position;
        Header = header;
        Description = description;
    }

    public Pin(Vector2 position, string header, string description)
    {
        if (string.IsNullOrWhiteSpace(header))
            throw new ArgumentNullException(nameof(header));

        _position = new Vector2ForJson(position);
        Header = header;
        Description = description;
    }

    public event Action Updated;

    [JsonIgnore] public Vector2 Position => _position.ToVector2();
    [JsonProperty("Header")] public string Header { get; private set; }
    [JsonProperty("Description")] public string Description { get; private set; }

    public void SetPosition(Vector2 position)
    {
        _position = new Vector2ForJson(position);

        Debug.Log(this == PinsStorage.Pins[0]);
    }

    public void SetHeader(string header)
    {
        if (string.IsNullOrWhiteSpace(header))
            throw new ArgumentNullException(nameof(header));

        Header = header;

        Updated?.Invoke();
    }

    public void SetDescription(string description)
    {
        Description = description;

        Updated?.Invoke();
    }

    [Serializable]
    public struct Vector2ForJson
    {
        public float X;
        public float Y;

        public Vector2ForJson(Vector2 v)
        {
            X = v.x;
            Y = v.y;
        }

        public Vector2 ToVector2() =>
            new Vector2(X, Y);

        public static Vector2ForJson FromVector2(Vector2 v) =>
            new Vector2ForJson(v);

        public override string ToString()
        {
            return $"Vector2({X}, {Y})";
        }
    }
}
