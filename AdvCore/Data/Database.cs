// Holden Ernest - 8/16/2026 - Database -- controls pulling files and whatnot

namespace AdvCore.Data;

using System;
using System.Collections.Generic;
using System.Text.Json;
using AdvCore.Builders;
using AdvCore.Items;

public static class Database
{
    //TODO this -- determine what exactly is the function of the database.
    // Builders are for building the objects, so they should hold the dictionaries for ID lookup
    

    public static void LoadLists() {
        // Load any lookup dicts
        ItemBuilder.LoadList();
        EquipBuilder.LoadList();
    }
}