// Holden Ernest - 8/16/2026 - This represets an object to store ALL Character Stats
//                             Nothing here is stored or parsed. this just holds a cache of the ever changing stats for modification
using System.Collections.Generic;
using System.ComponentModel;
using AdvCore.Effects;

namespace AdvCore.StatCore;

public class StatSheet {

    private HealthManager healthManager;
    private Character character;
    private LevelStats levelStats;
    

    public int memory = 0;

    private StatModifier currentStats;

    private Dictionary<IStatMod, StatModifier> allMods = new();

    

    public StatSheet(Character c) {
        character = c;
        healthManager = new HealthManager(c);
    }

    public void LoadContent() {
        levelStats.LoadContent();
    }

    // START STAT MODIFICATION
    public void AddStatMod(IStatMod parentModifier, StatModifier changes) {

        StatModifier additiveChanges = changes.AsAdditiveStats(currentStats);
        
        currentStats.AddStats(additiveChanges);
        
        // TODO: update allMods, if key exists, add to it.

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


    // START DAMAGE CALCULATIONS
    public void TakeDamage(Character caster, int damage, DamageType type) {

        // TODO: calculate real damage then send that instead
        healthManager.TakeDamage(caster, damage);
    }

    private void CalcDamageCast(ref int damage, Character caster, DamageType damageType) {
        // update damage based on caster damage modifiers
    }

    private void CalcDamageResist(ref int damage, DamageType damageType) {
        // update damage based on resists
        if (damageType == DamageType.NONE) {damage = 0; return; }
        if (damageType == DamageType.TRUE) return;

    }
    // END DAMAGE CALCULATIONS
}