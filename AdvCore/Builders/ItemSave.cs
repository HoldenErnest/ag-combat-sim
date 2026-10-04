// Holden Ernest - 10/4/2026 - Save Struct for items.

public struct ItemSave() {
    public int ID {get;set;}

    public int amount {get;set;}
    public string name {get;set;}
    public float dropChance {get;set;}
}

public struct EquipSave {
    public int ID {get;set;}

    public int amount {get;set;}
    public string name {get;set;}
    public float dropChance {get;set;}

    public bool equipped {get;set;}
    public int reforgeCount {get;set;}
}