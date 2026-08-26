// Holden Ernest - 8/26/2026 - This manages ALL effects for a given Character.
//                             ALL damage is passed through here too, so this is where damage is passed.


using Microsoft.Xna.Framework;

namespace AdvCore.Effects;


public class EffectManager {

    private Character character;

    private Effect[] activeEffects = [];

    public EffectManager(Character c) {
        character = c;
    }

    public void Update(GameTime gameTime) {
        // TODO PROC SYSTEM
    }
}