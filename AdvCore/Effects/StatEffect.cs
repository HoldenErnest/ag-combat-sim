// Holden Ernest - 8/24/2026 - An effect which increases/decreases a characters statistics
//                             Examples: ArmorReduction, Learned, Swoll, Slow

using System;
using System.Text.Json;
using AdvCore.StatCore;

namespace AdvCore.Effects;


public class StatEffect : Effect, IStatMod {

    public StatModifier statChange {get; init; }
    
    public StatEffect(int id): base(id) {
    }

    public override StatEffect Clone() {
        // would be nice to have this as a generic in the parent class
        string json = JsonSerializer.Serialize(this);
        return JsonSerializer.Deserialize<StatEffect>(json);
    }

    protected override void Proc() {
        Core.chat.SendDebugMessage(name + ": procced, adding " + statChange + " to " + target.name);
        target.statSheet.AddStatMod(this, statChange);
        base.Proc();
    }

    public override void EndEffectEvent() {
        Core.chat.SendDebugMessage(name + ": removing ALL stats I hope from " + target.name);
        target.statSheet.RemoveStatMod(this);
    }
}