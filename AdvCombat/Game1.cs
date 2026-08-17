// Holden Ernest - 8/15/2026 -- This initializes and RUNS the game (game loop)

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AdvCore;
using System;
using AdvCore.Input;
using MonoGame.Extended.Graphics;

namespace AdvCombat;

public class Game1 : Core
{
    private static Controller controller;
    private static Player player;

    private float posx = 0f; // TEMP

    public Game1() : base("Adventure Combat", 1280, 720, false)
    {

    }

    protected override void Initialize()
    {
        player = new Player();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        player.LoadContent();
        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {   
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        controller.Update(gameTime);

        posx += 0.1f * gameTime.ElapsedGameTime.Milliseconds;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);


        // OPTIMIZE SPRITEBATCH DRAW ORDER: group textures together (all _img go before calling another texture to be drawn)
        // specify source rectanges for spritesheets (TextureAtlas class (dict<animframe 1/"walking sprite", the rectange to draw>))

        SpriteBatch.Begin();
        
        player.Draw();

        SpriteBatch.End();

        base.Draw(gameTime);
    }
}