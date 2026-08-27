// Holden Ernest - 8/26/2026 - This manages ALL effects for a given Character.
//                             ALL damage is passed through here too, so this is where damage is passed.


using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace AdvCore.Effects;


public class EffectManager {

    private Character character;

    private TimeSpan currentTime = TimeSpan.Zero;

    private Dictionary<int, Effect> activeEffects = new Dictionary<int, Effect>();

    public EffectManager(Character c) {
        character = c;
    }

    public void Update(GameTime gameTime) {
        currentTime = gameTime.TotalGameTime;
        // TODO PROC SYSTEM
        foreach (Effect e in activeEffects.Values) {
            e.Update(gameTime);

            if (!e.IsActive()) activeEffects.Remove(e.ID);
        }
    }

    public void AddEffect(Character caster, Effect e) {
        if (activeEffects.ContainsKey(e.ID)) {
            activeEffects[e.ID].RefreshDuration(currentTime, e);
            return;
        }
        e.BeginEffect(currentTime, character, caster);
        activeEffects.Add(e.ID, e);
    }
}