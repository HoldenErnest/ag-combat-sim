// Holden Ernest - 8/24/2026 - An effect which increases/decreases a characters statistics
//                             Examples: ArmorReduction, Learned, Swoll, Slow

using AdvCore.StatCore;

namespace AdvCore.Effects;


public class StatEffect : Effect{

    public StatModifier statChange {get; init; }
    
    public StatEffect(int id): base(id) {
        
    }
}