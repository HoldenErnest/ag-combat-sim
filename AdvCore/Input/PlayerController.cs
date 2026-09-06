// Holden Ernest 8/15/2026 -- manages all input for the game. - a static instance of this class is created and updated from the game initialization
using System;
using System.IO;
using System.Security.Cryptography;
using AdvCore.Chat;
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
        UpdateExtraInputs(keyboardState);

        UpdateVelocity(dt);
        Move(dt);

        UpdateCamera(mouseState);

    }
    public void UpdateCamera(MouseStateExtended mouseState) {
        Core.camera.SetTarget(character.getPosition());
        if (mouseState.DeltaScrollWheelValue == 0) return;
        Core.camera.Zoom(mouseState.DeltaScrollWheelValue);
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
    
    private void UpdateEnterPress(KeyboardStateExtended keysState) {
        // The enter key can be used in different ways depending on context.
        if (!keysState.WasKeyPressed(Keys.Enter)) return;

        ChatManager chat = Core.chat;

        if (chat.EditorFocused()) {
            chat.SendPlayerMessage(character);
        }
    }

    private void UpdateExtraInputs(KeyboardStateExtended keysState) {

        UpdateEnterPress(keysState);
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