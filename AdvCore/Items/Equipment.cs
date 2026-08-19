// Holden Ernest - 8/16/2026 - This is an equip.

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using AdvCore.Skills;
using AdvCore.StatCore;
using MonoGame.Extended.Graphics;

namespace AdvCore.Items;

public class Equipment : Item
{
    public static readonly Equipment NullEquip = new Equipment(0);
    public static readonly HashSet<string> gearTypes = new HashSet<string> {
        "weapon",
        "helmet",
        "ring",
        "chest",
        "pants",
        "boots"
        };

    // Dynamic properties (SAVABLE) (loaded from save state (default from lookup))
    public bool equipped = false;
    public int reforgeCount = 0; // increases the stats on load.

    // Immutable properties (loaded from lookup)
    public readonly string gearType = "";
    public readonly int[] skills;
    public readonly Stats stats;
    public readonly string textureFile;

    public Equipment(int id) : base(id)
    {
        
    }

    private bool VerifyGearType(string s) {
        return gearTypes.Contains(s);
    }

    public override string ToString() {
        return base.ToString() + ". TYPE: " + gearType;
    }
}