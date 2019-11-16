using UnityEngine;
using UnityEngine.AI;

namespace Svnvav.Samples
{
    public class EatingBehaviour : AnimalBehaviour
    {
        public override AnimalBehaviourType BehaviorType => AnimalBehaviourType.SearchFood;
        
        
        private Plant _target;

        public void Initialize(Animal animal, Plant target)
        {
            _target = target;
        }

        public override bool GameUpdate(Animal animal)
        {
            _target.PassEffects(animal);
            _target.Die();
            return false;
        }

        public override void Recycle()
        {
            AnimalBehaviourPool<EatingBehaviour>.Reclaim(this);
        }
        
    }
}