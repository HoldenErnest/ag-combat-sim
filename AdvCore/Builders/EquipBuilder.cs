// Holden Ernest - 8/19/2026 -- Builder for any Equip object

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using AdvCore.Items;

namespace AdvCore.Builders;

public class EquipBuilder {
    
    private static readonly string listFile = "Data/equips.json";
    private static Dictionary<int,Equipment> dict = [];

    public EquipBuilder() {
        if (dict.Count < 1) {
            throw new Exception("Item list has not been loaded before attempting to build");
        }
    }

    public static Equipment FromID(int id) {
        if (id == 0 || !dict.ContainsKey(id)) return Equipment.NullEquip;
        if (dict.Count < 1) {
            throw new Exception("Item list has not been loaded before attempting to build");
        }
        
        return dict[id];
    }
    public static void LoadList() {
        // loaded from Database.cs
        string filePath = Path.Combine(Core.Content.RootDirectory, listFile);
        string jsonString = File.ReadAllText(filePath);

        JsonSerializerOptions options = new JsonSerializerOptions {IncludeFields = true};
        EquipList data = JsonSerializer.Deserialize<EquipList>(jsonString, options);

        if (data.equips is null) {
            throw new FileLoadException();
        }

        foreach (Equipment e in data.equips) {
            dict.Add(e.ID, e);
        }

    }
}

// TODO IMPORTANT> _______>>> determine effect subclass when parsing. (idk best way to do this)
// TODO keep in mind: it will need the same parsing for multiEffects ect. (you probably dont want to just add these sub effects directly to active because they should remain seperate proccing)

// Used strictly to deserialize json
public struct EquipList {
    public Equipment[] equips;
}