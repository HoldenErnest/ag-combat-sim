// Holden Ernest - 8/16/2026 - This is a special Item type that you can "consume"
// Consuming will give you an effect.

namespace AdvCore.Items;

public class Consumable : Item {

    public int effect;

    public Consumable(int id) : base(id) {
        
    }
}