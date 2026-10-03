// Holden Ernest - 10/3/2026 - This represents the playable game session. This should be instanced behind a menu button.
//                             Perhaps for networking, a NetworkGameManager can be made as child?


using AdvCore;
using AdvCore.Chat;
using AdvCore.UI.Screens;
using Microsoft.Xna.Framework;

namespace AdvCore.GameCore;

public class GameManager {

    private Player player;
    public static ChatManager chat;
    private MainScreen mainScreen;

    // game object array for all entities.

    public GameManager() {
        
    }


    private void Init() {
        mainScreen = new MainScreen();
        Core.GumUI.Root.AddChild(mainScreen);
    }

    public void LoadContent() {
        Init();
        // this should load all information about a single session. Most generic information is already preloaded on game init.
        player = new Player();
        player.LoadContent();

        chat = new ChatManager(mainScreen.ChatInterfaceInstance);
    }

    public void Update(GameTime gameTime) {
        player.Update(gameTime);
    }

    public void Draw(GameTime gameTime) {
        player.Draw();
    }
}