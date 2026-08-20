// Holden Ernest - 8/16/2026 - This contains everything that has to do with a specific Character

using System;
using AdvCore.Input;
using MonoGame.Extended;

namespace AdvCore;

public class Player : Character
{
    private static Player player;

    public Player() : base(0)
    {
        if (player != null) {
            throw new InvalidOperationException($"Only a single Player instance can be created");
        }
        player = this;
        controls = new PlayerController(player);
        // TODO: Init Character with everything that the Player needs
        // this can be done in base()
    }
    public override void Destroy() {
        Console.WriteLine("ERROR: Attempted to destroy Player object, that makes me sad");
    }
}