// Holden Ernest - 8/16/2026 - This represets an object to store ALL Character Stats
//                             Nothing here is saved or parsed. this just holds a cache of the ever changing stats for modification
using System;
using System.Collections.Generic;
using System.ComponentModel;
using AdvCore.Effects;

namespace AdvCore.StatCore;

public class StatSheet {

    private readonly float conMult = 0.08f;
    private readonly float conLevelMult = 0.03f; // base hp increase for 
    private readonly float strMult = 0.05f;
    private readonly float strWgtMult = -2f; // reduces the integer weight directly.
    private readonly float intMult = 0.05f;
    private readonly float tecMult = -0.01f;
    private readonly float agiEvaMult = 0.005f;
    private readonly float agiSpdMult = 0.02f;
    private readonly float AmrMult = 100f; // armor resist is calculated differently. a/(a+amult)

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
        PostStatChange(changes);
    }
    public void RemoveStatMod(IStatMod parentModifier) {
        if (!allMods.ContainsKey(parentModifier)) return;

        currentStats.RemoveStats(allMods[parentModifier]);

        PostStatChange(allMods[parentModifier]);
        allMods.Remove(parentModifier);
    }
    private void PostStatChange(StatModifier changedStats) {
        // these could be added or removed, the actual values mean nothing.
        if (changedStats.stats.ContainsKey("con")) {
            healthManager.UpdateMaxHealth(CalcMaxHealth());
        }
    }
    // END STAT MODIFICATION

    public string GetStatsString() {
        return currentStats.ToString();
    }



    // START DAMAGE CALCULATIONS
    public void TakeDamage(Character caster, int damage, DamageType type) {
        // caster applies their damage buffs
        caster.statSheet.CalcDamageCast(ref damage, type);
        // target applies their damage resists
        CalcDamageResist(ref damage, type);

        // TODO: calculate real damage then send that instead
        healthManager.TakeDamage(caster, damage);
    }

    private int CalcMaxHealth() {
        // TODO: health from level + health from CON
        int baseHP = 100;
        int lvl = levelStats.getLevel();
        baseHP += (int)(baseHP * conLevelMult * (lvl == 0 ? 1 : lvl));
        return baseHP + (int)(baseHP * conMult * currentStats.stats["con"]);
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
                damage += (int)MathF.Round(damage * strMult * currentStats.stats["str"]);
                break;
            case DamageType.GAS:
                damage += (int)MathF.Round(damage * intMult * currentStats.stats["int"]);
                break;
            case DamageType.LIQUID:
                damage += (int)MathF.Round(damage * intMult * currentStats.stats["int"]);
                break;
            case DamageType.SOLID:
                damage += (int)MathF.Round(damage * intMult * currentStats.stats["int"]);
                break;
            default:
                Console.WriteLine("Damage type not found.. " + damageType);
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
                float resist = currentStats.stats["amr"] / (currentStats.stats["amr"] + AmrMult);
                damage -= (int)(damage * resist); // 100(d) -= 100(d) * (0.5|0.01)
                break;
            case DamageType.GAS:
                damage -= (int)(damage * currentStats.stats["r_gas"]);
                break;
            case DamageType.LIQUID:
                damage -= (int)(damage * currentStats.stats["r_lqd"]);
                break;
            case DamageType.SOLID:
                damage -= (int)(damage * currentStats.stats["r_sld"]);
                break;
            case DamageType.HEALING:
                // 0 morality = -50% healing ; 100 morality = +50% healing
                damage = -(int)MathF.Max(1,damage * (moral + 0.5f));
                break;
            default:
                Console.WriteLine("Damage type not found.. " + damageType);
                break;
        }

    }
    // END DAMAGE CALCULATIONS
}