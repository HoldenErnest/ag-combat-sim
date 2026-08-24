// Holden Ernest - 8/23/2026 - The chat manager..

using System.Collections.Generic;
using AdvCore.UI.Components.Chat;
using MonoGame.Extended.Collections;

namespace AdvCore.Chat;

public class ChatManager {

    private ChatInterface chat;
    private ChatCommands commandManager;

    private Queue<ChatItem> chatItems = new Queue<ChatItem>();
    private int chatQueueSize = 20;

    public ChatManager(ChatInterface chatInterface) {
        chat = chatInterface;
        commandManager = new ChatCommands(this);
    }

    public void SendDebugMessage(string message) {
        CreateMessage("[SYSTEM]", message);
    }
    public void SendCommandMessage(string message) {
        CreateMessage("[COMMAND]", message);
    }

    public void SendCharacterMessage(Character c, string message) {
        CreateMessage(getCharacterString(c), message);
    }
    public void SendPlayerMessage(Character c) {
        string message = chat.TextEditor.Text;
        if (message.Length <= 0) return;

        chat.TextEditor.Text = "";

        if (commandManager.IsCommand(message)) {
            commandManager.ParseCommand(c, message);
        } else {
            CreateMessage(getCharacterString(c), message);
        }
    }
    private string getCharacterString(Character c) {
        return c.name + " - " + c.title;
    }

    private void CreateMessage(string sender, string message) {
        ChatItem chatItem = new ChatItem();
        chatItem.SenderName = sender;
        chatItem.MessageData = message;

        chatItems.Enqueue(chatItem);
        if (chatItems.Count >= chatQueueSize) {
            ChatItem removedItem = chatItems.Dequeue();
            chat.ListBox.RemoveChild(removedItem);
        }
        chat.ListBox.AddChild(chatItem);

    }

    public bool EditorFocused() {
        return chat.TextEditor.IsFocused;
    }


}