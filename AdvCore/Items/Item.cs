// Holden Ernest - 8/16/2026 - This is an item

using System.Text.Json.Serialization;

namespace AdvCore.Items;

public class Item {
    public static readonly Item NullItem = new Item(0);

    public readonly int ID;

    // Dynamic properties (SAVABLE) (loaded from save state (default from lookup))
    public int amount = 1;
    public string name;
    public float dropChance = 1f;

    // Immutable properties (loaded from lookup)
    public string desc {get; init; }
    public string iconFile {get; init; }
    
    public Item(int id) {
        ID = id;
    }

    public override string ToString() {
        string s = $"ID: {ID}, name: {name}, desc: {desc}, iconFile: {iconFile}";
        return "[" + this.GetType() + "] " + s;
    }
}