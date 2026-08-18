// Holden Ernest - 8/16/2026 - This is an item

namespace AdvCore.Items;

public class Item {
    public static readonly Item NullItem = new Item(-1);

    public readonly int ID;

    // Dynamic properties (SAVABLE) (loaded from save state (default from lookup))
    public int amount = 1;
    public string name;
    public float dropChance = 1f;

    // Immutable properties (loaded from lookup)
    public string desc;
    public string iconFile;
    
    public Item(int id) {
        ID = id;
    }
}