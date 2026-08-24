// Holden Ernest - 8/24/2026 - Base Effect class. This is used as THE ONLY way to damage characters / inflict stat buffs/debuffs
//                             Typically these effects are procced at an interval for a certain duration.

using System.Drawing;
using System.Text.Json;

namespace AdvCore.Skills;


public class Effect {

    public int ID {get; init; }
    public int altID {get; init; } // ONLY USED WHEN BUILDING/INIT (this only tells you which ID to derrive from)
    public string name {get; init; }
    public string desc {get; init; }
    public string[] tags {get; init; }
    public bool removable {get; init; }
    public float duration {get; init; }
    public float procInterval {get; init; }
    public float effectMultiplier {get; init; }

    public string iconName {get; init; }
    public string hexColor {get; init; }
    public string animName {get; init; }
    public string audioName {get; init; }

    private Color color;
    private int altDepth = 0; // how many altID records have we parsed for this one effect (so it doesnt recurse into oblivion)

    public static readonly Effect NullEffect = new Effect(0);

    public Effect(int id) {
        ID = id;
        color = ColorTranslator.FromHtml(hexColor);
    }
    public void UpdateAltProperties() {
        // from the list, create a new effect where ID = altID
        
    }
    public Effect Clone() {
        string json = JsonSerializer.Serialize(this); // TODO
        return JsonSerializer.Deserialize<Effect>(json);
    }


}