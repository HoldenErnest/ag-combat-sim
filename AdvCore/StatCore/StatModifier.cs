// Holden Ernest - 9/4/2026 - This represets a transferable object to update Character Stats

// All stats must be floats so they can be used as multipliers if needed.

namespace AdvCore.StatCore;

public class StatModifier
{
    public bool useAsMultipliers = false;

    // Level Speccing
    public float constitution = 0;
    public float strength = 0;
    public float intelligence = 0;
    public float evasion = 0;
    public float speed = 0;

    // No Level Speccing
    public float armor;
    public float weight;
    public float gas_resist;
    public float liquid_resist;
    public float solid_resist;
    public float reflect;
}