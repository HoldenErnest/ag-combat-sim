// Holden Ernest - 8/16/2026 - This represets an object to store ALL Character Stats

using AdvCore.Effects;

namespace AdvCore.StatCore;

public class StatSheet {

    private HealthManager healthManager;
    private Character character;
    // TODO level manager
    
    // Level Speccing
    public int constitution = 0;
    public int strength = 0;
    public int intelligence = 0;
    public int evasion = 0;
    public int speed = 0;

    // No Level Speccing
    public int armor;
    public int weight;
    public float gas_resist;
    public float liquid_resist;
    public float solid_resist;
    public float reflect;

    public int memory = 0;

    public StatSheet(Character c) {
        character = c;
        healthManager = new HealthManager(c);
    }


    public void TakeDamage(Character caster, int damage, DamageType type) {

        // TODO: calculate real damage then send that instead
        healthManager.TakeDamage(caster, damage);
    }
}