// Holden Ernest - 8/16/2026 - This holds all information about the inventory / items / equips
//      -- this is also a pretty important piece for saving the game.

namespace AdvCore.Items;

public class Inventory
{

    private EquipManager equipManager;
    private Item[] items;
    // TODO some kind of inventory UI


    public static Inventory FromSaveFile(int id) {
        //TODO CharacterBuilder.cs

        //load a list with the id. That list will contain savable aspects for Items only with its ID.
        // from there each item must be loaded to get its original stored data as well.
        // TODO ItemBuilder
        return new Inventory();
    }
    public Inventory()
    {
        equipManager = new EquipManager();
    }
    public Inventory(int id)
    {
        equipManager = new EquipManager();
    }

    
}