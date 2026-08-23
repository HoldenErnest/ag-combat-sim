// Holden Ernest - 8/22/2026 - Controls any UI interactions from the character states

// Since this has a UI component, it will be updated with the GumUI in Core.

using System;
using AdvCore.UI.Components;
using Gum.DataTypes.Variables;
using Microsoft.Xna.Framework;
using RenderingLibrary.Graphics;

namespace AdvCore.UI;

public class UICharacterManager {

    private HealthBar healthBar;
    private Character character;


    // IMPORTANT -- these variables dont actually control your life or anything.
    // They just act as a buffer for animations.
    private int maxHealth = 100;
    private int currentHealth = 100;
    private float currentSliderHealth = 100; // this is where the animation is at

    private readonly float lerpSpeed = 2f;

    public UICharacterManager(Character c) {
        if (c.ID == 0) { // this is the player.
            healthBar = new HealthBar(); // TODO: assign this to the main screens healthbar.
        } else {
            healthBar = new HealthBar();
        }
        character = c;

        Core.GumUI.Root.AddChild(healthBar);
    }

    public void Update(GameTime gameTime) {
        positionOnCharacter();
        attemptHPDiffLerp(gameTime);
        
    }

    public void UpdateHeath(int actualmaxhp, int actualhp) {
        // the idea here is to update health increases faster than decreases. // CALLED from the healthManager
        if (actualmaxhp != maxHealth) {
            maxHealth = actualmaxhp;
        }

        currentHealth = actualhp;

        // animation will never be below the actual hp level.
        if (actualhp > currentSliderHealth) currentSliderHealth = actualhp;

        UpdateSliderUI();
    }
    private void UpdateSliderUI() {
        healthBar.HealthPercentage = getHealthPercentage();
        // TODO update vertical lines based on max health
    }
    private void positionOnCharacter() {
        //if (character.ID == 0) return;
        Vector2 worldCoords = character.getPosition();
        worldCoords.Y -= 30;
        Vector2 screenCoords = Core.camera.WorldToScreen(worldCoords);
        healthBar.X = screenCoords.X;
        healthBar.Y = screenCoords.Y;
    }
    private void attemptHPDiffLerp(GameTime gameTime) {
        // if actual hp and animated HP differ, do a lerp to get them closer together.
        if (currentHealth != currentSliderHealth) {
            
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            currentSliderHealth = Single.Lerp(currentSliderHealth, currentHealth, lerpSpeed * dt);

            if (currentSliderHealth < currentHealth + 1f && currentSliderHealth > currentHealth - 1f) {
                currentSliderHealth = currentHealth;
            }

            if (currentSliderHealth > currentHealth) {
                healthBar.DamageChangeState = HealthBar.DamageChange.Damaged;
            } else if (currentSliderHealth < currentHealth) {
                // This state is never used. -- included only if wanted later
                healthBar.DamageChangeState = HealthBar.DamageChange.Healing;
            } else {
                healthBar.DamageChangeState = HealthBar.DamageChange.None;
            }

            healthBar.DiffPercentage = (currentSliderHealth * 1.0f / maxHealth) * 100;
        }
    }

    private float getHealthPercentage() {
        float pct = (currentHealth * 1.0f / maxHealth) * 100;
        return pct;
    }


}