// Holden Ernest - 9/4/2026 - This represets a transferable object to update Character Stats

// All stats must be floats so they can be used as multipliers if needed.

using System;
using System.Collections.Generic;
using MonoGame.Extended.Particles.Modifiers;

namespace AdvCore.StatCore;

public struct StatModifier {

    public StatModifier() {
        
    }

    private readonly HashSet<string> statTypes = new() {
        "con",
        "str",
        "int",
        "evade",
        "speed",
        "weight",
        "armor",
        "r_gas",
        "r_liquid",
        "r_solid",
        "reflect"
    };

    public bool useAsMultipliers = false;

    public Dictionary<string, float> stats;


    private void ValidateStats() {
        foreach (string k in stats.Keys) {
            if (!statTypes.Contains(k)) {
                Console.WriteLine("ERROR: stat type not found on load '" + k + "'");
                stats.Remove(k);
                continue;
            }
        }
    }

}