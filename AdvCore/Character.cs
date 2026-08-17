// Holden Ernest - 8/16/2026 - This contains everything that has to do with a specific Character

using AdvCore.Graphics;
using AdvCore.Input;
using AdvCore.Skills;
using AdvCore.StatCore;
using AdvCore.UI;
using AdvCore.Items;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AdvCore;

public class Character
{
    public CharacterModel model;
    protected Controller controls;
    private Inventory inventory;
    private StatsManager statsManager;
    private Skillbook skillbook;
    private CharacterUI ui;
    private HealthManager healthManager;

    private bool savable;
    private string name;
    private string title;
    private string desc;



    public Character()
    {
        model = new CharacterModel(0);
    }
    public Character(Controller c)
    {
        controls = c;
        model = new CharacterModel(0);
    }


    // START - IMPORTANT UPDATE FUNCTIONS
    public void Update(GameTime gameTime)
    {
        controls.Update(gameTime);
        model.Update(gameTime);
    }
    public void LoadContent()
    {
        model.LoadContent();
    }
    public void Draw()
    {
        model.Draw(controls.getPosition(), new Vector2(1,1));
    }
    // END -IMPORTANT UPDATE FUNCTIONS

    public Vector2 getPosition()
    {
        // returns the world position of this character.
        return controls.getPosition();
    }
}