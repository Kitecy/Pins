using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public static class PinsStorage
{
    private const string SaveKey = "Pins";

    private static readonly List<Pin> s_pins = new();

    public static IReadOnlyList<Pin> Pins => s_pins;

    public static void Add(Pin pin)
    {
        if (s_pins.Contains(pin))
            throw new System.InvalidOperationException("");

        s_pins.Add(pin);

        Save();
    }

    public static void Remove(Pin pin)
    {
        if (s_pins.Remove(pin) == false)
            throw new System.InvalidOperationException("");

        Save();
    }

    public static void Load()
    {
        string jsonData = PlayerPrefs.GetString(SaveKey);

        if (string.IsNullOrWhiteSpace(jsonData))
            return;

        List<Pin> pins = JsonConvert.DeserializeObject<List<Pin>>(jsonData);

        if (pins != null)
            s_pins.AddRange(pins);
    }

    public static void Save()
    {
        PlayerPrefs.SetString(SaveKey, JsonConvert.SerializeObject(s_pins, Formatting.Indented));
    }
}
