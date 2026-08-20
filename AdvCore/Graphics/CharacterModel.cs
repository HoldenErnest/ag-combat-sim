// Holden Ernest - 8/16/2026 - The look of a character - can be modified by equipment. (the characters EquipManager)

// Add various methods to setup scenarios for different Sprites.

using System;
using System.Linq;
using AdvCore.Builders;
using AdvCore.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;

namespace AdvCore.Graphics;

public class CharacterModel
{
    private AnimatedSprite sprite; // current sprite to be drawn (this can be an animated sprite)
    private SpriteSheet spriteSheet;
    private TimeSpan duration = TimeSpan.FromSeconds(0.1f); // this can be updated based on player speed.

    private string textureFile = "HumanMale1";
    private Texture2D bodyTexture;
    private Texture2D fullTexture;
    private string[] equipTexFiles;

    public CharacterModel() {
         // TODO: use texture name instead of index to get texture;
    }


    public void UpdateEquipFiles(string[] eTexFiles) {
        equipTexFiles = eTexFiles;
        // TODO: if it is completely loaded then you can update textureAtlas
    }

    private void UpdateTextureAtlas() {
        // these equips come in order of drawlayer (determined in Equipment)
        Console.WriteLine(" UPDATING THE TEXTURE ATLAS");
        Color[] data = new Color[bodyTexture.Width * bodyTexture.Height];
        fullTexture = bodyTexture;
        // TODO make this async? so game doesnt freeze but might take a second to udpate texture
        foreach (string texFile in equipTexFiles.Reverse()) {
            // combine fullTexture with each e texture in order
            if (texFile is null) continue;
            string fullFilePath = $"SpriteSheets/Equips/{texFile}";
            Texture2D temp = Core.Content.Load<Texture2D>(fullFilePath);
            fullTexture = TexMod.CombineTex2D(fullTexture, temp);
        }
        // update the spritesheet(which is currently in use)
        fullTexture.GetData(data);
        spriteSheet.TextureAtlas.Texture.SetData(data);
    }

    public void UpdateMovement(Vector2 prevInputs, Vector2 curInputs) {
        if (prevInputs == curInputs) return;

        UpdateSpriteDir(curInputs);
    }
    private void UpdateSpriteDir(Vector2 dir) {
        dir = Vector2.Round(dir);

        switch (dir.X, dir.Y) {

            case (0,0):
                string curAnim = sprite.CurrentAnimation;
                sprite.SetAnimation("idle-" + curAnim);
                break;
            case (1,0):
                sprite.SetAnimation("runRight");
                break;
            case (-1,0):
                sprite.SetAnimation("runRight");
                break;
            case (0,1):
                sprite.SetAnimation("runDown");
                break;
            case (0,-1):
                sprite.SetAnimation("runUp");
                break;
            case (1,1):
                sprite.SetAnimation("runDownRight");
                break;
            case (-1,1):
                sprite.SetAnimation("runDownRight");
                break;
            case (1,-1):
                sprite.SetAnimation("runUpRight");
                break;
            case (-1,-1):
                sprite.SetAnimation("runUpRight");
                break;

            default:
                Console.WriteLine("ERROR: Character model movement not found");
                return;
        }
        if (dir.X == -1) {
            sprite.Effect = SpriteEffects.FlipHorizontally;
        } else if (dir != Vector2.Zero){
            sprite.Effect = SpriteEffects.None;
        }
    }

    // START - IMPORTANT UPDATE FUNCTIONS
    public void Update(GameTime gameTime) {
        if (sprite is AnimatedSprite animatedSprite)
            animatedSprite.Update(gameTime);
    }
    public void Draw(Vector2 pos, Vector2 scale) {

        // TODO convert from world position to camera position
        sprite.Draw(Core.SpriteBatch, pos, 0f, scale);
    }
    public void LoadContent() {
        string fullFilePath = $"SpriteSheets/CharacterBase/{textureFile}";
        bodyTexture = Core.Content.Load<Texture2D>(fullFilePath);

        Texture2DAtlas atlas = Texture2DAtlas.Create(fullFilePath, bodyTexture, 32, 32);

        spriteSheet = new SpriteSheet(fullFilePath, atlas);
        SetupAnims();
        sprite = new AnimatedSprite(spriteSheet, "idle-runDown");
        sprite.OriginNormalized = new Vector2(0.5f,0.75f);
        UpdateTextureAtlas();
    }
    // END - IMPORTANT UPDATE FUNCTIONS

    private void SetupAnims() {
        SetupAnim("runDown", 0);
        SetupAnim("runUp", 1);
        SetupAnim("runUpRight", 2);
        SetupAnim("runRight", 3);
        SetupAnim("runDownRight", 4);
    }
    private void SetupAnim(string animName, int rowIndex) {
        // the spritesheets are setup as an 8x8 grid of 32x32 sprites
        // so for now at least, these indicies can be retrieved with math
        spriteSheet.DefineAnimation(animName, builder => {
            builder.IsLooping(true)
                .AddFrame(1 + (rowIndex * 8), duration)
                .AddFrame(2 + (rowIndex * 8), duration)
                .AddFrame(1 + (rowIndex * 8), duration)
                .AddFrame(0 + (rowIndex * 8), duration)
                .AddFrame(3 + (rowIndex * 8), duration)
                .AddFrame(4 + (rowIndex * 8), duration)
                .AddFrame(3 + (rowIndex * 8), duration)
                .AddFrame(0 + (rowIndex * 8), duration);

        });
        spriteSheet.DefineAnimation("idle-" + animName, builder => {
            builder.IsLooping(false)
                .AddFrame(0 + (rowIndex * 8), TimeSpan.FromSeconds(0f));
        });
    }
}