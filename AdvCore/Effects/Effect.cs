// Holden Ernest - 8/24/2026 - Base Effect class. This is used as THE ONLY way to damage characters / inflict stat buffs/debuffs
//                             Typically these effects are procced at an interval for a certain duration.

using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using AdvCore.Builders;
using AdvCore.Data;
using AdvCore.StatCore;
using Microsoft.Xna.Framework;

namespace AdvCore.Effects;


public class Effect : Object {

    public int ID {get; init; }
    public string name {get; init; }
    public string desc {get; init; }
    public string[] tags {get; init; }
    public bool removable {get; init; } // can the character themselves remove this effect (for anoying effects that you might not want on all the time)
    public float duration {get; init; } = 0f;
    public float procInterval {get; init; } = 0f;
    public float effectMultiplier {get; set; } = 1f;

    public string iconName {get; init; }
    public string hexColor {get; init; }
    public string animName {get; init; }
    public string audioName {get; init; } // play on each proc


    private Color color;
    private bool isActive = false;
    protected int procCount = 0;

    private TimeSpan finishTime;
    private TimeSpan nextProcTime;
    protected Character target, caster;

    public static readonly Effect NullEffect = new Effect(0);

    public Effect(int id) {
        ID = id;
        //color = ColorTranslator.FromHtml(hexColor);
    }
    public virtual Effect Clone() {
        string json = JsonSerializer.Serialize(this);
        return JsonSerializer.Deserialize<Effect>(json);
    }

    public void RefreshDuration(TimeSpan currentTime, Effect newestEffect = null) {
        if (!isActive) {
            // just in case a refresh is called when its not active.
            BeginEffect(currentTime, target, caster);
            return;
        }
        nextProcTime = currentTime;
        finishTime = TimeSpan.FromSeconds(duration) + currentTime;

        UpdateRefreshValues(newestEffect);

        Proc(); 

        if (duration == 0) EndEffect();
    }

    private void UpdateRefreshValues(Effect e) {
        if (e is null) return;
        // TODO: UPDATE THE effectMultiplier to use the higher damage?????
        // the thing is two different casters mightve used the same effect on this character
        // so the damage might not be so cut and dry as the effectMultiplier
    }

    public void BeginEffect(TimeSpan currentTime, Character tar, Character cas) {
        target = tar;
        caster = cas;
        isActive = true;

        RefreshDuration(currentTime);
    }
    public void EndEffect() {
        // TEMP -----------------
        Core.chat.SendDebugMessage("EFFECT " + name + " ENDED with " + procCount + " procs");
        procCount = 0;
        isActive = false;
    }
    public bool IsActive() {
        return isActive;
    }


    public void Update(GameTime gameTime) {
        // when this effect is active, Update its effect here.
        if (!isActive) return;
        
        TimeSpan currentTime = gameTime.TotalGameTime;

        if (nextProcTime < currentTime) {
            Proc();
        }

        if (finishTime < currentTime) EndEffect();

    }

    public virtual void UpdateEffectTriggers(Effect triggerEffect) {
        // Some effects trigger events from other effects (damage stacking)
        // Do Nothing here
    }

    protected virtual void Proc() {
        // Child Proc stuff
        RefreshProcInterval();
        procCount++;
    }

    private void RefreshProcInterval() {
        if (nextProcTime == TimeSpan.MaxValue) return;
        if (procInterval > 0) {
            nextProcTime += TimeSpan.FromSeconds(procInterval);

            // just to be safe that it cant proc more than intended (using hardware buffering)
            if (nextProcTime > finishTime) nextProcTime = TimeSpan.MaxValue;
        } else {
            nextProcTime = TimeSpan.MaxValue;
        }
    }


}



public class EffectStruct {
    // use a specific effectmultiplier with an effect
    // these are typically used for subeffect loading (effects found within other objects)
    public int ID {get; init; }
    public float effectMultiplier {get; set; } = 1f;

    public EffectStruct() {
        
    }

    public Effect ToEffect() {
        Effect e = new EffectBuilder().FromID(ID);
        e.effectMultiplier = effectMultiplier;
        return e;
    }
}