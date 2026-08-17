// Holden Ernest - 8/16/2026 - This contains everything that has to do with a specific Character

using System;
using AdvCore.Input;

namespace AdvCore;

public class Player : Character
{
    private static Player player;

    public Player() : base(new PlayerController())
    {
        if (player != null) {
            throw new InvalidOperationException($"Only a single Player instance can be created");
        }

        player = this;
        // TODO: Init Character with everything that the Player needs
        // this can be done in base()
    }
}