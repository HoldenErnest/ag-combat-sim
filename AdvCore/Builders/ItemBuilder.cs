// Holden Ernest - 8/17/2026 -- Builder for any Item object

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using AdvCore.Items;

namespace AdvCore.Builders;

public class ItemBuilder {
    
    private static readonly string listFile = "";
    private static Dictionary<int,Item> dict = [];

    public ItemBuilder() {
        if (dict.Count < 1) {
            throw new Exception("Item list has not been loaded before attempting to build");
        }
    }

    public Item FromID(int id) {
        if (dict.ContainsKey(id)) return Item.NullItem;

        return dict[id];
    }
    public static void LoadList() {
        // loaded from Database
        Core.Content.Load<Character>(listFile);
    }
}