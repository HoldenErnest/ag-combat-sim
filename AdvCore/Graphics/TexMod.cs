// Holden Ernest - 8/20/2026 - Apply modifications to textures. (I need something for combining textures really)

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AdvCore.Graphics;

public static class TexMod {
    
    // Draws Texture A UNDER texture B (texA is the base)
    public static Texture2D CombineTex2D(Texture2D texA, Texture2D texB) {
        if (texA.Width != texB.Width || texA.Height != texB.Height) {
            Console.WriteLine("ERROR: texmod cannot combine textures of different sizes");
            return texA;
        }
        int width = texA.Width;
        int height = texA.Height;

        GraphicsDevice graphicsDevice = Core.GraphicsDevice;
        RenderTarget2D target = new RenderTarget2D(
            graphicsDevice,
            width,
            height,
            false,
            SurfaceFormat.Color,
            DepthFormat.None);

        SpriteBatch spriteBatch = Core.SpriteBatch;
        graphicsDevice.SetRenderTarget(target);
        graphicsDevice.Clear(Color.Transparent);

        spriteBatch.Begin(
            SpriteSortMode.Deferred,
            BlendState.AlphaBlend);

        spriteBatch.Draw(texA, Vector2.Zero, Color.White);
        spriteBatch.Draw(texB, Vector2.Zero, Color.White);

        spriteBatch.End();

        graphicsDevice.SetRenderTarget(null);

        return target;
    }
}