// Holden Ernest - 8/16/2026 - This represets an object to store ALL Character Stats
//                             Nothing here is saved or parsed. this just holds a cache of the ever changing stats for modification
using System;
using System.Collections.Generic;
using System.ComponentModel;
using AdvCore.Effects;

namespace AdvCore.StatCore;

public class StatSheet {

    private HealthManager healthManager;
    private Character character;
    private LevelStats levelStats;
    

    public int memory = 0;
    public float moral = 0.5f; // these stats ARE savable. But they only change through story events so they are not included within the modifier

    private StatModifier currentStats;

    private Dictionary<IStatMod, StatModifier> allMods = new();


    public StatSheet(Character c) {
        character = c;
        currentStats = new();
        healthManager = new HealthManager(c);
        levelStats = new LevelStats();
    }

    public void LoadContent() {
        levelStats.LoadContent();
        // TODO: update current stats based on level stats
    }

    // START STAT MODIFICATION
    public void AddStatMod(IStatMod parentModifier, StatModifier changes) {

        StatModifier additiveChanges = changes.AsAdditiveStats(currentStats);
        
        currentStats.AddStats(additiveChanges);
        
        if (allMods.ContainsKey(parentModifier)) {
            // if there is a key for this event triggered stat effect, just add the changes to the existing one
            // some effects may have "stacking stats" like more armor every hit, just update this existing one since ALL these stats should get removed together
            allMods[parentModifier].AddStats(additiveChanges);
        } else {
            allMods[parentModifier] = additiveChanges;
        }
    }
    public void RemoveStatMod(IStatMod parentModifier) {
        if (!allMods.ContainsKey(parentModifier)) return;

        currentStats.RemoveStats(allMods[parentModifier]);

        allMods.Remove(parentModifier);
    }
    // END STAT MODIFICATION

    public string GetStatsString() {
        return currentStats.ToString();
    }


    // START DAMAGE CALCULATIONS
    public void TakeDamage(Character caster, int damage, DamageType type) {

        caster.statSheet.CalcDamageCast(ref damage, type);
        CalcDamageResist(ref damage, type);

        // TODO: calculate real damage then send that instead
        healthManager.TakeDamage(caster, damage);
    }

    private void CalcDamageCast(ref int damage, DamageType damageType) {
        // update damage based on caster damage modifiers
        switch (damageType) {
            case DamageType.NONE:
                damage = 0;
                break;
            case DamageType.TRUE:
                break;
            case DamageType.PHYSICAL:
                damage = (int)MathF.Round(damage * currentStats.stats["str"]);
                break; // TODO these all suck, figure out good formulas to convert
            case DamageType.GAS:
                damage = (int)MathF.Round(damage * currentStats.stats["int"]);
                break;  //TODO technique stat?
            case DamageType.LIQUID:
                damage = (int)MathF.Round(damage * currentStats.stats["int"]);
                break;
            case DamageType.SOLID:
                damage = (int)MathF.Round(damage * currentStats.stats["int"]);
                break;
        }
    }

    private void CalcDamageResist(ref int damage, DamageType damageType) {
        // update damage based on resists
        switch (damageType) {
            case DamageType.NONE:
                damage = 0;
                break;
            case DamageType.TRUE:
                break;
            case DamageType.PHYSICAL:
                damage -= (int)(damage * currentStats.GetArmorResist()); // TODO determine percent resist from armor
                break;
            case DamageType.GAS:
                damage -= (int)MathF.Round(damage * currentStats.stats["r_gas"]);
                break;
            case DamageType.LIQUID:
                damage -= (int)MathF.Round(damage * currentStats.stats["r_liquid"]);
                break;
            case DamageType.SOLID:
                damage -= (int)MathF.Round(damage * currentStats.stats["r_solid"]);
                break;
            case DamageType.HEALING:
                // 0 morality = -50% healing ; 100 morality = +50% healing
                damage = -(int)MathF.Max(1,damage * (moral + 0.5f));
                break;
        }

    }
    // END DAMAGE CALCULATIONS
}