// Holden Ernest - 8/16/2026 - The look of a character - can be modified by equipment. (the characters EquipManager)

// Add various methods to setup scenarios for different Sprites.

using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;

namespace AdvCore.Graphics;

public class CharacterModel
{
    private Sprite sprite; // current sprite to be drawn (this can be an animated sprite)
    private SpriteSheet bodySheet;

    private string textureFile = "HumanMale1";

    public CharacterModel(int id)
    {
        
    }

    public void UpdateMovement(System.Numerics.Vector2 prevVel, System.Numerics.Vector2 curVel)
    {
        if (prevVel == curVel) return;


    }

    public void UpdateModel(GameTime gameTime)
    {
        if (sprite is AnimatedSprite animatedSprite)
            animatedSprite.Update(gameTime);
    }
    public void Draw(System.Numerics.Vector2 pos, System.Numerics.Vector2 scale)
    {

        // TODO convert from world position to camera position
        sprite.Draw(Core.SpriteBatch, pos, 0f, scale);
    }

    public void LoadContent()
    {
        Texture2D tex = Core.Content.Load<Texture2D>(textureFile);

        Texture2DAtlas atlas = Texture2DAtlas.Create("Atlas/Cards", tex, 32, 32);

        bodySheet = new SpriteSheet($"SpriteSheet/CharacterBase/{textureFile}", atlas);

        sprite = bodySheet.CreateSprite(0);
    }
}