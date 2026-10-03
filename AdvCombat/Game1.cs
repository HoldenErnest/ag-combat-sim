// Holden Ernest - 8/15/2026 -- This initializes and RUNS the game (game loop)

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AdvCore;
using MonoGame.Extended;
using AdvCore.Data;
using AdvCore.UI.Screens;
using System;
using System.Diagnostics;
using MonoGameAndGum.Renderables;
using Gum.Forms.Controls;
using AdvCore.UI.Components;
using AdvCore.Chat;
using AdvCore.GameCore;


namespace AdvCombat;

public class Game1 : Core
{

    private static GameManager gameManager; // THIS SHOULD be instanced behind a menu button at somepoint

    public Game1() : base("Adventure Combat", 1280, 720, false)
    {

    }

    protected override void Initialize()
    {
        GumUI.Initialize(this, "GumUI/AdvUI.gumx");
        gameManager = new GameManager();

        base.Initialize();

        ShapeRenderer.Self.Initialize(); // Recommended, optional: shape fill/gradient/shadow
        Gum.Wireframe.CustomSetPropertyOnRenderable.InMemoryFontCreator =
            new KernSmith.Gum.KernSmithFontCreator(GraphicsDevice);

    }

    protected override void LoadContent()
    {
        Debug.Assert(GraphicsDevice != null);

        // content loading happens AFTER all init
        Database.LoadLists();
        gameManager.LoadContent();

        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {   
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        gameManager.Update(gameTime);

        camera.Update(gameTime);
        GumUI.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {

        Debug.Assert(GraphicsDevice != null);

        GraphicsDevice.Clear(Color.CornflowerBlue);

        Matrix transformMatrix = camera.GetViewMatrix();

        // OPTIMIZE SPRITEBATCH DRAW ORDER: group textures together (all _img go before calling another texture to be drawn)
        // specify source rectanges for spritesheets (TextureAtlas class (dict<animframe 1/"walking sprite", the rectange to draw>))
        

        SpriteBatch.Begin(transformMatrix: transformMatrix, samplerState: SamplerState.PointClamp);
        
        gameManager.Draw(gameTime);

        RectangleF rect = new RectangleF(0,0,16,16);
        SpriteBatch.DrawRectangle(rect, Color.White);

        SpriteBatch.End();

        GumUI.Draw();
        base.Draw(gameTime);
    }
}