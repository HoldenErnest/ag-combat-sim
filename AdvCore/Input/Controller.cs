// Holden Ernest 8/15/2026 -- manages all input for the game. - a static instance of this class is created and updated for each character

using Microsoft.Xna.Framework;
using System.Numerics;

namespace AdvCore.Input;

public class Controller {

    private System.Numerics.Vector2 worldPosition = new System.Numerics.Vector2(0,0);

    public Controller() {
    }
    
    public virtual void Update(GameTime gameTime) { // TODO: make this not virtual -- only the called methods should be virtual (all controllers have the same update sequence?)
        UpdateMovement();
    }

    public virtual void UpdateMovement()
    {
        
    }

    public System.Numerics.Vector2 getPosition()
    {
        // get world position of this controller.
        return worldPosition;
    }

}