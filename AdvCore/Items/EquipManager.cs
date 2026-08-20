// Holden Ernest - 8/16/2026 - Manage all Equips -- then ensure everything is properly updated such as the Model

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;

namespace AdvCore.Items;

public class EquipManager
{

    private static Dictionary<string,Equipment> equipment = [];

    private Character user;

    public EquipManager()
    {
        
    }

    
    public void Equip(Equipment e)
    {
        if (!Equipment.ValidGearType(e.gearType)) {
            Console.WriteLine("ERROR: tried to equip an invalid geartype");
            return;
        }
        if (equipment.ContainsKey(e.gearType)) {
            equipment[e.gearType].equipped = false;
        }

        equipment[e.gearType] = e;
        equipment[e.gearType].equipped = true;

        UpdateGearTexture();
        UpdateGearUI();
    }
    public void Unequip(Equipment e)
    {
        
    }

    private void UpdateGearTexture() {
        string[] textureFilesInOrder = new string[equipment.Count];

        textureFilesInOrder = equipment
            .OrderBy(pair => pair.Value.GetGearLayer())
            .Select(pair => pair.Value.textureFile)
            .ToArray();

        user.model.UpdateEquipFiles(textureFilesInOrder);
    }
    private void UpdateGearUI() {
        // TODO: update the inventory to show/hide equipped items.. perhaps a slotID should be passed in as well
    }

    public void SetupUser(Character c) {
        user = c;
    }
}