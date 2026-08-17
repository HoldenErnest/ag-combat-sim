// Holden Ernest - 8/16/2026 - This is an item

namespace AdvCore.Items;

public class Item
{

    public readonly int ID;
    public int amount = 1;
    private float dropChance = 1f;
    private string defaultName;
    private string desc;
    private string iconName; // TODO -- these might need to be split up on load. Just store on file stuff

    public Item()
    {
        
    }
    public Item(int id)
    {
        ID = id;
    }
}