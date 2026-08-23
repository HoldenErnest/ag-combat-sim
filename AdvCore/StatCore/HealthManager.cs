// Holden Ernest - 8/16/2026 - Manages ALL health transactions for a Character

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
        // Damage Resist calculations are done here.
        // TODO: make actual calculations with the resist values
        this.hp -= damage;

        ui.UpdateHeath(maxhp, hp);
    }

    public void UpdateMaxHealth() {
        // TODO: everything that changes max health will be in the StatsManager.
    }
}