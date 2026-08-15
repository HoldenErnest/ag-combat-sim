// Holden Ernest 8/15/2026 -- manages all input for the game. - a static instance of this class is created and updated from the game initialization
using System;
using System.Security.Cryptography;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;
using MonoGame.Extended.Input.InputListeners;


namespace AdvCore.Input;

public class Controller {
    MouseListener mouseListener;

    public Controller() {
        InitListeners();
    }
    
    public void Update(GameTime gameTime) {
        KeyboardExtended.Update();
        KeyboardStateExtended keyboardState = KeyboardExtended.GetState();
        MouseExtended.Update();
        MouseStateExtended mouseState = MouseExtended.GetState();
        mouseListener.Update(gameTime);

        //Console.WriteLine("mousePOS: " + mouseState.Position.ToString());

        

    }

    private void InitListeners() {
        MouseListenerSettings mls = new MouseListenerSettings{DoubleClickMilliseconds = 200, DragThreshold = 8};

        mouseListener = mls.CreateListener();
        mouseListener.MouseClicked += (sender, args) => {Console.WriteLine("CLICKED MOUSE");};
        
    }

}