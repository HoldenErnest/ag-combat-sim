// Holden Ernest 8/15/2026 -- manages all input for the game. - a static instance of this class is created and updated from the game initialization
using System;
using System.IO;
using System.Security.Cryptography;
using AdvCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Input;
using MonoGame.Extended.Input.InputListeners;


namespace AdvCore.Input;

public class PlayerController : Controller {

    private Vector2 inputDir;

    public PlayerController(Character c) : base(c) {
    }
    
    public override void Update(GameTime gameTime) {
        KeyboardExtended.Update();
        KeyboardStateExtended keyboardState = KeyboardExtended.GetState();
        MouseExtended.Update();
        MouseStateExtended mouseState = MouseExtended.GetState();

        //Console.WriteLine("mousePOS: " + mouseState.Position.ToString());
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

        UpdateInputs(keyboardState);

        UpdateVelocity(dt);
        Move(dt);

        UpdateCamera(mouseState);

    }
    public void UpdateCamera(MouseStateExtended mouseState) {
        Core.camera.LookAt(getPosition());

        Core.camera.Zoom += mouseState.DeltaScrollWheelValue/100f;
    }

    private void UpdateInputs(KeyboardStateExtended keysState)
    {
        Vector2 prevInputDir = inputDir;
        inputDir = Vector2.Zero;
        
        if (keysState.IsKeyDown(Keys.W)) {
            inputDir -= Vector2.UnitY;
        }
        if (keysState.IsKeyDown(Keys.S)) {
            inputDir += Vector2.UnitY;
        }
        if (keysState.IsKeyDown(Keys.A)) {
            inputDir -= Vector2.UnitX;
        }
        if (keysState.IsKeyDown(Keys.D)) {
            inputDir += Vector2.UnitX;
        }

        if (inputDir != Vector2.Zero) {
            inputDir = Vector2.Normalize(inputDir);
        }

        // TODO: move this out of this method for AIControllers
        if (prevInputDir != inputDir) {
            character.model.UpdateMovement(prevInputDir, inputDir);
        }
    }

    public void UpdateVelocity(float deltaTime)
    {

        // 4. Smooth out changes in speed (Acceleration / Friction)
        if (inputDir != Vector2.Zero) {
            Vector2 targetVelocity = inputDir * maxSpeed;
            velocity = Vector2.Lerp(velocity, targetVelocity, accel * deltaTime);
        } else {
            velocity = Vector2.Lerp(velocity, Vector2.Zero, deccel * deltaTime);
        }
    }

}