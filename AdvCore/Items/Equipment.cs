// Holden Ernest - 8/16/2026 - This is an equip.

using System.Collections.Generic;
using System.Runtime.Serialization;
using AdvCore.Skills;
using AdvCore.StatCore;
using MonoGame.Extended.Graphics;

namespace AdvCore.Items;

public class Equipment : Item
{
    public static HashSet<string> gearTypes = new HashSet<string> {
        "weapon",
        "helmet",
        "ring",
        "chest",
        "pants",
        "boots"
        };


    // Dynamic properties (SAVABLE) (loaded from save state (default from lookup))
    public bool equipped = false;
    public int reforgeCount; // increases the stats on load.

    // Immutable properties (loaded from lookup)
    public string gearType = "";
    public int[] skills;
    public Stats stats;
    public string textureFile;

    public Equipment(int id) : base(id)
    {
        
    }

    private bool VerifyGearType(string s) {
        return gearTypes.Contains(s);
    }

    private protected void Load()
    {
        // TODO: load from the database using the ID
        //spriteSheet = new SpriteSheet("equip/shirt1");
    }
}