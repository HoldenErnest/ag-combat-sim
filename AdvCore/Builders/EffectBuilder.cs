// Holden Ernest - 8/17/2026 -- Builder for any Effect Init

// TODO: this should probably just be static. The builder doesnt actually need to do anything (at least rn)

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using AdvCore.Effects;

namespace AdvCore.Builders;

public class EffectBuilder {
    
    private static readonly string listFile = "Data/effects.json";
    private static Dictionary<int,Effect> dict = [];

    public EffectBuilder() {
        if (dict.Count < 1) {
            throw new Exception("Item list has not been loaded before attempting to build");
        }
    }

    public Effect FromID(int id) {
        if (id == 0 || !dict.ContainsKey(id)) return Effect.NullEffect;

        if (dict.Count < 1) {
            throw new Exception("Effect list has not been loaded before attempting to build");
        }

        return dict[id].Clone();
    }
    public static void LoadList() {
        // Loaded from Database.cs
        string filePath = Path.Combine(Core.Content.RootDirectory, listFile);
        string jsonString = File.ReadAllText(filePath);

        JsonSerializerOptions options = new JsonSerializerOptions {IncludeFields = true};
        options.Converters.Add(new JsonStringEnumConverter());
        EffectList data = JsonSerializer.Deserialize<EffectList>(jsonString, options);

        if (data.damage is null) {
            throw new FileLoadException();
        }

        data.AddAllToDict(dict);
    }
}

public class EffectList {
    public DamageEffect[] damage;
    public MultiEffect[] multi;
    public StatEffect[] stat;

    public void AddAllToDict(Dictionary<int, Effect> dict) {
        foreach (DamageEffect e in damage) {
            AddToDict(dict, e.ID, e);
        }
        foreach (MultiEffect e in multi) {
            AddToDict(dict, e.ID, e);
        }
        foreach (StatEffect e in stat) {
            AddToDict(dict, e.ID, e);
        }
    }
    private void AddToDict(Dictionary<int, Effect> dict, int id, Effect e) {
        if (dict.ContainsKey(e.ID)) {
            Console.WriteLine("ERROR: duplicate effect ID");
            return;
        }
        dict.Add(e.ID, e);
    }
}