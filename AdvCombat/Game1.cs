// Holden Ernest - 8/15/2026 -- This initializes and RUNS the game (game loop)

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AdvCore;
using MonoGame.Extended;

namespace AdvCombat;

public class Game1 : Core
{
    private static Player player;

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
        // content loading happens AFTER all init
        player.LoadContent();
        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {   
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        player.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        Matrix transformMatrix = camera.GetViewMatrix();

        // OPTIMIZE SPRITEBATCH DRAW ORDER: group textures together (all _img go before calling another texture to be drawn)
        // specify source rectanges for spritesheets (TextureAtlas class (dict<animframe 1/"walking sprite", the rectange to draw>))
        

        SpriteBatch.Begin(transformMatrix: transformMatrix, samplerState: SamplerState.PointClamp);
        
        player.Draw();

        RectangleF rect = new RectangleF(0,0,16,16);
        SpriteBatch.DrawRectangle(rect, Color.White);

        SpriteBatch.End();

        base.Draw(gameTime);
    }
}