// Holden Ernest - 9/7/2026 - Holds any stats that can be manually selected from levelups
//                            (This is saved)

using System;

namespace AdvCore.StatCore;

public class LevelStats : IStatMod {
    
    // Level Speccing
    public int constitution = 0;
    public int strength = 0;
    public int intelligence = 0;
    public int agility = 0;
    public int technique = 0;

    public int totalXP = 0;
    public int attrPoints = 0; // unused points.

    private int xp;
    private int level;

    public LevelStats() {
        
    }

    public void LoadContent() {
        // TODO: Load from json
        setLevel(1);
    }

    public int getXpToNextLevel() {
        return 0;
    }

    // LEVEL/XP
    public void setLevel(int l) { // set current xp based on a specified wanted level.
        totalXP = (int)Math.Pow(l - 1, 3);
        level = l;
        fitAllStats();
    }
    public int updateLevel() { // returns how many levels were added: (-) if lost levels somehow
        int oldLevel = level;
        level = (int)Math.Floor(Math.Cbrt(xp)) + 1;
        return level - oldLevel;
    }
    public void addXp(int amt) {
        xp += amt;
        int levelsAdded = updateLevel();
        if (levelsAdded > 0) { // level up
            attrPoints += (levelsAdded)*2;
        } else if (levelsAdded < 0) { // character somehow lost levels
            resetAllAttr();
        }
        //Debug.Log($"added {amt} xp");
    }
    public int getXp() { // xp gained in the current level (!! NOT TOTAL XP !!)
        return xp - (int)Math.Floor(Math.Pow(level - 1, 3));
    }
    public int getMaxXp() { // total xp getting to a new level would take
        return (int)Math.Pow(level, 3) - (int)Math.Pow(level - 1, 3);
    }
    public int getLevel() {
        return level;
    }
    public int getNeededXp() { // xp currently needed to level up
        return (int)Math.Pow(level, 3) - xp;
    }
    public int getOnKillXp() { // only used to represent how much xp someone gets for killing this character
        int specifiedXp = (int)MathF.Ceiling(getMaxXp() / 10); // MIGHT NEED TO FIX THIS! kill avg of 20 guys the same level as you to level up or maybe 9 or so, 1 level up.
        // TODO: RANDOM XP -- int addRandom = (int)MathF.R((int)MathF.Floor(-specifiedXp / 5), (int)MathF.Ceiling(specifiedXp / 5));
        return specifiedXp;
    }
    public int getTotalAttr() {// Hypothetical ammount to available if playing by the rules
        return getLevel()*2;
    }
    public int getTotalAttrUsed() {
        return constitution+strength+intelligence+agility+technique;
    }
    public bool maxedStats() { // return if youve maxed out the tree
        return getAvailableAttr() <= 0;
    }
    public int getAvailableAttr() {
        return (getTotalAttr() - getTotalAttrUsed());
    }
    //END LEVEL/XP

    public void resetAllAttr() {
        constitution = 0;
        strength = 0;
        intelligence = 0;
        agility = 0;
        technique = 0;

        attrPoints = getTotalAttr();
    }
    private void fitAllStats() { // change some levels to make sure that you arent using over your allotted points
        // overflow will be 0 when youve used up all points (- when you are over)
        int overflow = getAvailableAttr();
        if (overflow < 0) {
            for (int i = 0; overflow < 0; i++) { // loop through every stat and dec until you are 0
                ref int n = ref getStatFromIndex(i);
                overflow += n;
                n = 0;
            }
        }
    }

    private ref int getStatFromIndex(int index) {
        switch (index) {
            case 0:
                return ref constitution;
            case 1:
                return ref strength;
            case 2:
                return ref intelligence;
            case 4:
                return ref agility;
            case 5:
                return ref technique;
        }
        throw new Exception("invalid index for a stat");
    }
}