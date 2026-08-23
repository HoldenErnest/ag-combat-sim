// Holden Ernest - 8/23/2026 - Specialized methods for an Orthographic camera.

using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace AdvCore.Input;

public class CameraMod {
    private OrthographicCamera camera;

    private float zoomSpeed = 1f;

    private float camSpeed = 2f;
    private float minDist = 15f; // distance the camera is allowed to be away from its target (box)

    private Vector2 target = Vector2.Zero;
    private Vector2 position = Vector2.Zero;
    private Vector2 velocity = Vector2.Zero;

    public CameraMod(OrthographicCamera cam) {
        camera = cam;

        camera.MaximumZoom = 10f;
        camera.MinimumZoom = 1f;
    }

    public Matrix GetViewMatrix() {
        return camera.GetViewMatrix();
    }

    public void Zoom(int deltaScrollValue) {
        camera.Zoom += deltaScrollValue/100f * zoomSpeed;
        // TODO, this sucks, fix
    }

    public void Update(GameTime gameTime) {
        UpdateVelocity();
        UpdatePosition(gameTime);
    }

    private void UpdateVelocity() {
        float distX = Math.Abs(target.X - position.X);
        float distY = Math.Abs(target.Y - position.Y);
        if (distX > minDist)
            velocity = new Vector2(
                camSpeed * (distX-minDist) * ((target.X - position.X)/distX),
                velocity.Y
            ); // camSpeed * distanceFromPlayer * direction
        else velocity = new Vector2(0, velocity.Y);
        if (distY > minDist)
            velocity = new Vector2(
                velocity.X,
                camSpeed * (distY-minDist) * ((target.Y - position.Y)/distY)
            );
        else velocity = new Vector2(velocity.X, 0);
    }

    private void UpdatePosition(GameTime gameTime) {
        // based on velocity.
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

        position += velocity * dt;

        camera.LookAt(position);
    }

    public void SetTarget(Vector2 pos) {
        // a smooth transition to look at the following thing (usually this is centered on the player)
        target = pos;
    }
    public Vector2 WorldToScreen(Vector2 worldPos) {
        return camera.WorldToScreen(worldPos);
    }
}