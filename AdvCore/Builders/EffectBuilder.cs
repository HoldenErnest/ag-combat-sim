// Holden Ernest - 8/17/2026 -- Builder for any Effect Init

// IMPORTANT FOR INITIALIZATION:     only dict key holds the correct ID. 

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json;
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

        return dict[id];
    }
    public static void LoadList() {
        // Loaded from Database.cs
        string filePath = Path.Combine(Core.Content.RootDirectory, listFile);
        string jsonString = File.ReadAllText(filePath);

        JsonSerializerOptions options = new JsonSerializerOptions {IncludeFields = true};
        EffectList data = JsonSerializer.Deserialize<EffectList>(jsonString, options);

        if (data.effects is null) {
            throw new FileLoadException();
        }

        foreach (Effect e in data.effects) {
            dict.Add(e.ID, e);
        }
    }
}

public struct EffectList {
    public Effect[] effects;
}