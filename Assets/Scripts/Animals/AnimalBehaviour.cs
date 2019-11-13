
namespace Svnvav.Samples
{
    public abstract class AnimalBehaviour
    {
        public abstract AnimalBehaviourType BehaviorType { get; }

        public virtual bool GameUpdate(Animal animal){return true;}
        
        public virtual void OnDisable(Animal animal){}
        
        public abstract void Recycle();
    }
}