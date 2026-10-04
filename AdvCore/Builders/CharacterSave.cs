// Holden Ernest - 10/4/2026 - A template for saving and loading a character since Characters are generally more complex than the 1:1s I've been doing.
//                             Its job is to push and pull data from its character attributes, from itself. when saving and loading

using System.Collections.Generic;
using AdvCore;
using Microsoft.Xna.Framework;

public class CharacterSave {

    public Character character {get;set;}
    public Vector2 worldPosition {get;set;}
    public List<ItemSave> allItems;
    public List<EquipSave> allEquips;
    //TODO QUESTS

    
    public CharacterSave() {
        
    }

    public void PushData() {
        
    }
    public void PullData() {
        //TODO
    }
}