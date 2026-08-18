// Holden Ernest - 8/17/2026 -- abstraction for any Object in the scene. 
// this allows tracking all entities

namespace AdvCore;

public class GameObject {

    public readonly long UUID;
    private static long UIDCounter = 0;

    public GameObject() {
        UUID = UIDCounter;
        GameObject.UIDCounter++;
    }

    public virtual void Destroy() {
        //! TODO: SETUP DESTRUCTOR FOR ALL OBJECTS 
        
    }
}