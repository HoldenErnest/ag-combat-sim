// Holden Ernest - 8/24/2026 - An effect which Damages the target.
//                             Examples: poison, instant_physical_damage, MANY things

namespace AdvCore.Effects;


public class DamageEffect {

    public int damage {get; init; }
    public string type {get; init; }
    public string[] stackTags {get; init; }
    public int maxStacks {get; init; } = 1;
    public float stackMultiplier {get; init; } = 1f;
    public float backMultiplier {get; init; } = 1f;

    private int stacks;

    public DamageEffect() {
        
    }
}