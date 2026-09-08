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

    private int constitution = 0;
    private int strength = 0;
    private int intelligence = 0;
    private int evasion = 0;
    private int speed = 0;

    // CACHED PERCENTAGE CALCULATIONS
    private int armor;
    private int weight;
    private float t_phys_resist;
    private float t_gas_resist;
    private float t_liquid_resist;
    private float t_solid_resist;
    private float t_reflect;

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
        // TODO::::
        // IF multiplication, do the math to convert the properties of the StatModifier to addition. (100 base, 0.1* = -90))

        // if key exists, add it to existing key
    }
    public void RemoveStatMod(IStatMod parentModifier) {
        if (!allMods.ContainsKey(parentModifier)) return;

        // TODO::::
        // revert everything (subtract) from that modifier
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