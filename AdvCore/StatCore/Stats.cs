// Holden Ernest - 8/16/2026 - This represets a modifiable/transferable object to store and update Character Stats

namespace AdvCore.StatCore;

public class Stats
{
    public bool useAsMultipliers = false;

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
}