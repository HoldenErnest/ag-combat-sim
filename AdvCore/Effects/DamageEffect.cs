// Holden Ernest - 8/24/2026 - An effect which Damages the target.
//                             Examples: poison, instant_physical_damage, MANY things

using System;
using System.Collections;
using System.Linq;
using System.Text.Json;
using AdvCore.Chat;

namespace AdvCore.Effects;


public class DamageEffect : Effect {

    public int baseDamage {get; init; } // base damage
    public DamageType type {get; init; }
    public string[] stackTags {get; init; }
    public int maxStacks {get; init; } = 1;
    public float stackMultiplier {get; init; } = 1f; // stack damage is added to base (5base + (2stacks * 5base * 1.0mult) = 15 damage)
    public float backMultiplier {get; init; } = 1f; // maybe this should be in a skill instead (or just calculate back on the initial proc)

    private int stacks = 0;

    public DamageEffect(int id) : base(id) {
        
    }

    public override DamageEffect Clone() {
        // would be nice to have this as a generic in the parent class
        string json = JsonSerializer.Serialize(this);
        return JsonSerializer.Deserialize<DamageEffect>(json);
    }

    protected override void Proc() {
        Core.chat.SendDebugMessage(name + " procced, dealing " + CalcPassedDamage() + " to " + target.name);
        base.Proc();
    }

    // send only the minimum - updated damage + type and caster to the target for processing
    private int CalcPassedDamage() {

        int actualBaseDamage = (Int32)(baseDamage * effectMultiplier);

        int newDamage = actualBaseDamage;

        newDamage += (Int32)(stacks * stackMultiplier * actualBaseDamage);

        // back multiplier or whatever

        return newDamage;
    }

    public override void UpdateEffectTriggers(Effect triggerEffect) {
        if (stacks < maxStacks) {
            stacks += stackTags.Intersect(triggerEffect.tags).Count();
            if (stacks > maxStacks) stacks = maxStacks;
        }
    }


}

public enum DamageType {
    NONE,
    TRUE,
    PHYSICAL,
    GAS,
    LIQUID,
    SOLID,
    HEALING
}