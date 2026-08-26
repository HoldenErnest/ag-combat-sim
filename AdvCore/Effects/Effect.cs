// Holden Ernest - 8/24/2026 - Base Effect class. This is used as THE ONLY way to damage characters / inflict stat buffs/debuffs
//                             Typically these effects are procced at an interval for a certain duration.

using System;
using System.Drawing;
using System.Text.Json;
using AdvCore.Builders;

namespace AdvCore.Effects;


public class Effect {

    public int ID {get; init; }
    public string name {get; init; }
    public string desc {get; init; }
    public string[] tags {get; init; }
    public bool removable {get; init; } // can the character themselves remove this effect (for anoying effects that you might not want on all the time)
    public float duration {get; init; }
    public float procInterval {get; init; }
    public float effectMultiplier {get; init; }

    public string iconName {get; init; }
    public string hexColor {get; init; }
    public string animName {get; init; }
    public string audioName {get; init; } // play on each proc

    private Color color;

    public static readonly Effect NullEffect = new Effect(0);

    public Effect(int id) {
        ID = id;
        color = ColorTranslator.FromHtml(hexColor);
    }
    public Effect Clone() {
        string json = JsonSerializer.Serialize(this); // TODO
        return JsonSerializer.Deserialize<Effect>(json);
    }


}