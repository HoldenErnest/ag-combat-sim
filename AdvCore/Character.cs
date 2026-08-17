// Holden Ernest - 8/16/2026 - This contains everything that has to do with a specific Character

using AdvCore.Graphics;
using AdvCore.Input;
using AdvCore.Skills;
using AdvCore.StatCore;
using AdvCore.UI;
using AdvCore.Items;
using Microsoft.Xna.Framework.Graphics;
using System.Numerics;

namespace AdvCore;

public class Character
{
    private CharacterModel model;
    private Controller controls;
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
    public void LoadContent()
    {
        model.LoadContent();
    }

    public void Draw()
    {
        model.Draw(controls.getPosition(), new Vector2(1,1));
    }
}