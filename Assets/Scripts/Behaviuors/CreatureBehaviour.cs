

using UnityEngine;

namespace Svnvav.Samples
{
    public abstract class CreatureBehaviour
    {
        public abstract BehaviourType BehaviorType { get; }
        
        public abstract bool GameUpdate(Creature creature);
        
        public abstract void Recycle();
    }
}