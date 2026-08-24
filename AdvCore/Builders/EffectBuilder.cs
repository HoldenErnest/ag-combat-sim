// Holden Ernest - 8/17/2026 -- Builder for any Effect Init

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using AdvCore.Items;
using AdvCore.Skills;

namespace AdvCore.Builders;

public class EffectBuilder {
    
    private static readonly string listFile = "Data/items.json";
    private static Dictionary<int,Effect> dict = [];

    public EffectBuilder() {
        if (dict.Count < 1) {
            throw new Exception("Item list has not been loaded before attempting to build");
        }
    }

    public Effect FromID(int id) {
        if (id == 0 || dict.ContainsKey(id)) return Effect.NullEffect;

        return dict[id];
    }
    public static void LoadList() {
        // loaded from Database
        //Core.Content.Load<Character>(listFile);
    }
}