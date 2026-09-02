// Holden Ernest - 8/24/2026 - An effect which triggers other effects. -- These proc independently of other effects
//                             Examples: Acid(poison+armor_reduction), Shocked(lightning_damage+movement_stop)


// I believe this should proc ALL sub effects, with the PARENTS OWN TIMER (not each on their own)
// BUT things like instant damage + poison should still work. so maybe not

using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AdvCore.Effects;


public class MultiEffect : Effect {

    public EffectStruct[] loadedEffects {get; init; }
    private List<Effect> effects = new List<Effect>();

    public MultiEffect(int id): base(id) {
    }

    protected override void Proc() {
        //TODO (make sure its LoadEffects first)
    }

    public void LoadEffects() {
        foreach (EffectStruct es in loadedEffects) {
            effects.Add(es.ToEffect());
            // TODO should sub effects use the parent DURATION?
        }
    }
}