// Holden Ernest - 8/23/2026 - Chat commands, must be admin to perform any of these


using System;
using System.Collections.Generic;
using AdvCore;
using AdvCore.Chat;
using AdvCore.Effects;

public class ChatCommands {

    private static ChatCommands instance;
    private static ChatManager chat;

    public ChatCommands(ChatManager cm) {
        if (instance != null)
        {
            throw new InvalidOperationException($"Only a single Chat Command instance can be created");
        }
        instance = this;
        chat = cm;
    }

    private bool CharacterIsAdmin(Character c) {
        // TODO: This is poor, do a better job.
        return c.ID == 0;
    }
    public bool IsCommand(string s) {
        return s.StartsWith("/");
    }

    public void ParseCommand(Character c, string commandString) {
        if (!CharacterIsAdmin(c)) return;
        if (!IsCommand(commandString)) return;

        string[] args = commandString.TrimStart('/').Split(" ");

        switch (args[0]) {
            case "give":
                GiveCommand(c, args);
                break;
            case "damage":
                DamageCommand(c, args);
                break;
            case "effect":
                EffectCommand(c, args);
                break;
            case "stats":
                StatsCommand(c, args);
                break;
            default:
                chat.SendDebugMessage("Invalid Command '" + args[0] + "'");
                break;
        }
    }

    private void GiveCommand(Character c, string[] args) {
        // TODO somaEtianfkcakjnsf
    }
    private void DamageCommand(Character c, string[] args) {
        // TODO :uhh, I need a list of characters before this. then use their name to select their character object
        Character target;
        if (args[1] == "self") {
            target = c;
        } else {
            chat.SendCommandMessage("error: target not found '" + args[1] + "'");
            return;
        }

        int damage = 0;

        if (!Int32.TryParse(args[2], out damage)) {
            chat.SendCommandMessage("error: invalid damage '" + args[2] + "'");
            return;
        }

        target.TakeDamage(c, damage, DamageType.TRUE);
    
    }

    private void EffectCommand(Character c, string[] args) {
        Character target;
        if (args[1] == "self") {
            target = c;
        } else {
            chat.SendCommandMessage("error: target not found '" + args[1] + "'");
            return;
        }

        int effectID = 0;

        if (!Int32.TryParse(args[2], out effectID)) {
            chat.SendCommandMessage("error: invalid effect ID '" + args[2] + "'");
            return;
        }

        target.effectManager.AddEffect(c, effectID);
    
    }

    private void StatsCommand(Character c, string[] args) {
        Character target;
        if (args[1] == "self") {
            target = c;
        } else {
            chat.SendCommandMessage("error: target not found '" + args[1] + "'");
            return;
        }

        chat.SendCommandMessage(c.name + " stats -- " + target.statSheet.GetStatsString());
    
    }
    
}