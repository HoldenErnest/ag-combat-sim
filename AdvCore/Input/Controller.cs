// Holden Ernest 8/15/2026 -- manages all input for the game. - a static instance of this class is created and updated for each character

using System.Net;
using AdvCore.Graphics;
using Microsoft.Xna.Framework;

namespace AdvCore.Input;

public class Controller {

    protected Vector2 worldPosition = Vector2.Zero;
    protected Vector2 velocity = Vector2.Zero;
    protected float maxSpeed = 100f; // TODO: these are all increased with the 'speed' stat
    protected float accel = 30f;
    protected float deccel = 12f;
    protected Character character;

    public Controller(Character c) {
        character = c;
    }
    
    public virtual void Update(GameTime gameTime) {}

    // TODO: ICE before updating velocity you can change deccel for sliding mechanics

    public void Move(float deltaTime)
    {
        worldPosition += velocity * deltaTime;
    }

    public Vector2 getPosition()
    {
        // get world position of this controller.
        return worldPosition;
    }

}