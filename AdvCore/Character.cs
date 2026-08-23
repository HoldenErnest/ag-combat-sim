// Holden Ernest - 8/16/2026 - This contains everything that has to do with a specific Character

using AdvCore.Graphics;
using AdvCore.Input;
using AdvCore.Skills;
using AdvCore.StatCore;
using AdvCore.UI;
using AdvCore.Items;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using AdvCore.UI.Components;

namespace AdvCore;

public class Character : GameObject
{
    public CharacterModel model;
    protected Controller controls;
    protected Inventory inventory;
    public StatsManager statsManager;
    private Skillbook skillbook;
    private HealthManager healthManager;
    public UICharacterManager uiManager;

    public readonly int ID;
    private bool savable;
    private string name;
    private string title;
    private string desc;

    public static Character FromSaveFile(int id) {
        // generate a new Character loaded with its save state.
        //TODO CharacterBuilder.cs
        Character car = new Character(id);
        car.inventory = Inventory.FromSaveFile(id);
        car.inventory.SetupUser(car);
        return car;
    }

    public Character(int id)
    {
        model = new CharacterModel();
        inventory = Inventory.FromSaveFile(id);
        inventory.SetupUser(this);
        uiManager = new UICharacterManager(this);
        healthManager = new HealthManager(this);
    }


    // START - IMPORTANT UPDATE FUNCTIONS
    public void Update(GameTime gameTime)
    {
        controls.Update(gameTime);
        model.Update(gameTime);
        uiManager.Update(gameTime);
    }
    public void LoadContent()
    {
        model.LoadContent();
    }
    public void Draw()
    {
        model.Draw(controls.getPosition(), new Vector2(1,1));
    }
    public override void Destroy() {
        //! TODO destroy
        base.Destroy();
    }
    // END -IMPORTANT UPDATE FUNCTIONS

    public Vector2 getPosition()
    {
        // returns the world position of this character.
        return controls.getPosition();
    }



    // START DAMAGE
    public void TakeDamage(Character caster, int damage) {
        healthManager.TakeDamage(caster, damage);
    }
}