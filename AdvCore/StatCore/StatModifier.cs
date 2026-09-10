// Holden Ernest - 9/4/2026 - This represets a transferable object to update Character Stats

// All stats must be floats so they can be used as multipliers if needed.

using System;
using System.Collections.Generic;
using CsvHelper;
using MonoGame.Extended.Particles.Modifiers;

namespace AdvCore.StatCore;

public struct StatModifier {

    public StatModifier() {
        if (stats is null) stats = new();
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

    public Dictionary<string, float> stats {get;set;}


    private void ValidateStats() {
        foreach (string k in stats.Keys) {
            if (!statTypes.Contains(k)) {
                Console.WriteLine("ERROR: stat type not found on load '" + k + "'");
                stats.Remove(k);
                continue;
            }
        }
    }

    public StatModifier AsAdditiveStats(StatModifier baseStats) {
        // return a new stat modifier based on its additive calculation from multiplying it to baseStats
        // EX: 100 base *0.1 = -90
        if (!useAsMultipliers) {
            return this;
        }
        if (baseStats.useAsMultipliers) {
            throw new Exception("Cannot Add to a Stat Modifier of a multiply type");
        }
        StatModifier additiveChanges = new StatModifier();

        foreach (string key in stats.Keys) {
            float baseVal = baseStats.stats.ContainsKey(key) ? baseStats.stats[key] : 0;
            float multVal = stats.ContainsKey(key) ? stats[key] : 1; // (this is the multiplier)
            additiveChanges.stats[key] = baseVal - (multVal * baseVal);
        }
        return additiveChanges;
    }

    public void AddStats(StatModifier changes) {
        StatModifier additiveChanges = changes.AsAdditiveStats(this);

        foreach (string key in changes.stats.Keys) {
            float addVal = additiveChanges.stats.ContainsKey(key) ? additiveChanges.stats[key] : 0;
            float currVal = stats.ContainsKey(key) ? stats[key] : 0;
            stats[key] = currVal + addVal;
        }
        
    }

    public void RemoveStats(StatModifier changes) {
        if (changes.useAsMultipliers || useAsMultipliers) {
            throw new Exception("Cannot use multiply stats when Removing stats");
        }
        foreach (string key in changes.stats.Keys) {
            float addVal = changes.stats.ContainsKey(key) ? changes.stats[key] : 0;
            stats[key] -= addVal;
        }
        
    }

    public override string ToString() {
        if (stats is null) return "Not Initialized";
        if (stats.Count == 0 ) return "No Stats";
        string s = "";
        foreach (string key in stats.Keys) {
            s += key + ": " + stats[key] + ", ";
        }
        return s;
    }

}