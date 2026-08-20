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
        Console.WriteLine("EQUIP PULLED: " + dict[id]);
        
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


// Used strictly to deserialize json
public struct EquipList {
    public Equipment[] equips;
}