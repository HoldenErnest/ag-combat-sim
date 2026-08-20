// Holden Ernest - 8/16/2026 - This holds all information about the inventory / items / equips
//      -- this is also a pretty important piece for saving the game.

using System;
using System.Collections.Generic;
using System.Linq;
using AdvCore.Builders;

namespace AdvCore.Items;

public class Inventory
{

    private EquipManager equipManager;
    private List<Item> items = new List<Item>();
    // TODO some kind of inventory UI


    public static Inventory FromSaveFile(int id) {
        
        Inventory inv = new Inventory();
        if (id == 0) { // PLAYER always has ID 0
            // TEMP
            for (int i = 2; i <= 4; i++) { 
                Equipment eq = EquipBuilder.FromID(i);
                eq.equipped = true;
                inv.items.Add(eq);
            }
        }

        //TODO: load inventory file with ItemBuilder?

        return inv;
    }
    public Inventory()
    {
        equipManager = new EquipManager();
    }

    public void EquipSlot(int slotIndex) {
        // TODO: this may or may not be handled in the Equip itself/(equip inventory UI)
        if (items[slotIndex] is Equipment equip)
            equipManager.Equip(equip);
        else
            Console.WriteLine("ERROR: attempted to equip a non Equipment type");
    }

    public void SetupUser(Character c) {
        // you only need a user for Characters inventories. Not Objects inventories.
        equipManager.SetupUser(c);
        UpdateEquippedItems();
    }

    private void UpdateEquippedItems() {
        foreach (Item i in items) {
            if (i is Equipment e) {
                if (e.equipped)
                    equipManager.Equip(e);
            }
        }
    }

    
}