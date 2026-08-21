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


namespace AdvCombat;

public class Game1 : Core
{
    private static Player player;
    private static MainScreen mainScreen;

    public Game1() : base("Adventure Combat", 1280, 720, false)
    {

    }

    protected override void Initialize()
    {
        base.Initialize();

        GumUI.Initialize(this, "GumUI/AdvUI.gumx");
        ShapeRenderer.Self.Initialize(); // Recommended, optional: shape fill/gradient/shadow
        Gum.Wireframe.CustomSetPropertyOnRenderable.InMemoryFontCreator =
            new KernSmith.Gum.KernSmithFontCreator(GraphicsDevice);

        mainScreen = new MainScreen();
        GumUI.Root.AddChild(mainScreen);

    }

    protected override void LoadContent()
    {
        Debug.Assert(GraphicsDevice != null);

        // content loading happens AFTER all init
        Database.LoadLists();
        player = new Player();
        player.LoadContent();

        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {   
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        player.Update(gameTime);

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
        
        player.Draw();

        RectangleF rect = new RectangleF(0,0,16,16);
        SpriteBatch.DrawRectangle(rect, Color.White);

        SpriteBatch.End();

        GumUI.Draw();
        base.Draw(gameTime);
    }
}