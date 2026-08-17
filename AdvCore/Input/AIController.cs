// Holden Ernest 8/15/2026 -- a static instance of this class is created and updated from the game initialization
using System;
using System.Security.Cryptography;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;
using MonoGame.Extended.Input.InputListeners;


namespace AdvCore.Input;

public class AIController : Controller {
    MouseListener mouseListener;

    public AIController()
    {
        
    }
    
    public override void Update(GameTime gameTime) {

        //Console.WriteLine("mousePOS: " + mouseState.Position.ToString());

        

    }

}