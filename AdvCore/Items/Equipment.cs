// Holden Ernest - 8/16/2026 - This is an equip.

using System.Runtime.Serialization;
using AdvCore.Skills;
using AdvCore.StatCore;
using MonoGame.Extended.Graphics;

namespace AdvCore.Items;

public class Equipment : Item
{
    private bool equipped;
    private int reforgeCount;
    private string gearType;
    private Skill[] skills; // TODO -- these might need to be split up on load. Just store on file stuff
    private Stats statIncrease;

    private SpriteSheet spriteSheet; // generated on Load from ID // TODO


    public Equipment() {}
    public Equipment(int id) : base(id)
    {
        
    }

    private protected void Load()
    {
        // TODO: load from the database using the ID
        //spriteSheet = new SpriteSheet("equip/shirt1");
    }
}