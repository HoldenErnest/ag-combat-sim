// Holden Ernest - 8/16/2026 - This is an equip.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using AdvCore.Skills;
using AdvCore.StatCore;
using MonoGame.Extended.Graphics;

namespace AdvCore.Items;

public class Equipment : Item
{
    public static readonly Equipment NullEquip = new Equipment(0);
    public static readonly Dictionary<string,int> gearIndex = new Dictionary<string,int> {
        ["weapon"] = 0,
        ["helmet"] = 1,
        ["ring"] = 2,
        ["chest"] = 3,
        ["pants"] = 4,
        ["boots"] = 5
        };

    // Dynamic properties (SAVABLE) (loaded from save state (default from lookup))
    public bool equipped = false;
    public int reforgeCount = 0; // increases the stats on load.

    // Immutable properties (loaded from lookup)
    public string gearType {get; init; }
    public int[] skills {get; init; }
    public Stats stats {get; init; }
    public string textureFile {get; init; }

    public Equipment(int id) : base(id)
    {
        
    }

    public static bool ValidGearType(string s) {
        if (s is null || s == "") return false;
        return gearIndex.ContainsKey(s);
    }
    public int GetGearLayer() {
        if (!gearIndex.ContainsKey(gearType)) {
            Console.WriteLine("ERROR getting gear layer index. Character may render improperly");
            return -1;
        }
        return gearIndex[gearType];
    }


    public override string ToString() {
        string s = $", gearType: {gearType}, skills: {skills}, stats: {stats}, textureFile: {textureFile}";
        return base.ToString() + s;
    }
}