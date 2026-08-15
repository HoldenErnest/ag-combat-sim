// Holden Ernest - 8/15/2026 -- This initializes and RUNS the game (game loop)

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AdvCore;
using System;

namespace AdvCombat;

public class Game1 : Core
{
    private Texture2D _img;

    private float posx = 0f; // TEMP

    public Game1() : base("Adventure Combat", 1280, 720, false)
    {

    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _img = Content.Load<Texture2D>("img/doge");

        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        posx += 0.1f * gameTime.ElapsedGameTime.Milliseconds;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);


        // OPTIMIZE SPRITEBATCH DRAW ORDER: group textures together (all _img go before calling another texture to be drawn)
        // specify source rectanges for spritesheets (TextureAtlas class (dict<animframe 1/"walking sprite", the rectange to draw>))

        SpriteBatch.Begin();
        
        SpriteBatch.Draw(
        _img,                      // texture
        new Vector2(                // position
            posx,
            Window.ClientBounds.Height * 0.5f),
        null,                       // sourceRectangle
        Color.White,                // color
        MathHelper.ToRadians(posx),   // rotation
        new Vector2(_img.Width, _img.Height) * 0.5f,               // origin
        (posx % 100) / 100,                       // scale
        SpriteEffects.None,         // effects
        0.0f                        // layerDepth
    );

        SpriteBatch.End();

        base.Draw(gameTime);
    }
}