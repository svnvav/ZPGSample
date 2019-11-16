using UnityEngine;

namespace Svnvav.Samples
{
    public class GoToFoodBehaviour : AnimalBehaviour
    {
        public override AnimalBehaviourType BehaviorType => AnimalBehaviourType.SearchFood;

        private Plant _target;

        public void Initialize(Animal animal, Plant target)
        {
            _target = target;
            animal.NavMeshAgent.SetDestination(_target.transform.position);
        }

        public override bool GameUpdate(Animal animal)
        {
            if (!_target.IsAlive)
            {
                animal.AddBehaviour<SearchFoodBehaviour>().Initialize(animal);
                return false;
            }
            
            var positionDif = animal.transform.position - _target.transform.position;
            
            if (positionDif.x * positionDif.x + positionDif.z * positionDif.z < 0.5f)
            {
                animal.AddBehaviour<EatingBehaviour>()
                    .Initialize(animal, _target);
                return false;
            }
            return true;
        }

        public override void Recycle()
        {
            AnimalBehaviourPool<GoToFoodBehaviour>.Reclaim(this);
        }
        
    }
}