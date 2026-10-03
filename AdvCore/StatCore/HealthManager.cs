// Holden Ernest - 8/16/2026 - Manages ALL health transactions for a Character

using System;
using AdvCore.UI;

namespace AdvCore.StatCore;

public class HealthManager {

    private UICharacterManager ui;
    private Character character;
    private StatsManager stats;

    private int hp = 100;
    private int maxhp = 100;


    public HealthManager(Character c) {
        character = c;
        stats = c.statsManager;
        ui = c.uiManager;
    }

    public void LoadFromID(int id) {
        //TODO implement
    }

    public void TakeDamage(Character caster, int damage) {
        // Damage Resist calculations are done in the statsheet.
        this.hp -= damage;

        ui.UpdateHeath(maxhp, hp);
    }

    public void UpdateMaxHealth(int newMax) {
        if (newMax == maxhp) return;
        if (newMax < hp) {
            hp = newMax;
            maxhp = newMax;
            return;
        }
        // if current hp is less than the new max, update it as a percentage
        float pct = (hp * 1.0f) / maxhp;

        hp = (int)(pct * newMax);
        maxhp = newMax;

        if (hp > maxhp) hp = maxhp; // redundency for conversion issues
    }
}