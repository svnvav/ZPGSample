using UnityEngine;

namespace Svnvav.Samples
{
    public class SearchFoodBehaviour : AnimalBehaviour
    {
        public override AnimalBehaviourType BehaviorType => AnimalBehaviourType.SearchFood;
        
        private float _searchRadius;

        private Vector3 _wanderPoint;

        public void Initialize(Animal animal)
        {
            _searchRadius = animal.Config.searchFoodRadius;

            _wanderPoint = NewWanderPoint(animal.transform.position);
            animal.NavMeshAgent.SetDestination(_wanderPoint);
            animal.NavMeshAgent.speed = animal.Config.moveSpeed;
        }

        public override bool GameUpdate(Animal animal)
        {
            var positionDif = animal.transform.position - _wanderPoint;
            
            if (positionDif.x * positionDif.x + positionDif.z * positionDif.z < 0.01f)
            {
                _wanderPoint = NewWanderPoint(animal.transform.position);
                animal.NavMeshAgent.SetDestination(_wanderPoint);
            }
            return CheckNearPlant(animal);
        }

        private bool CheckNearPlant(Animal animal)
        {
            var position = animal.transform.position;
            Vector3 top = position;
            top.y += 3f;
            var colliders = Physics.OverlapCapsule(
                position, top, _searchRadius
            );

            foreach (var collider in colliders)
            {
                var plant = collider.GetComponent<Plant>();
                if (plant != null)
                {
                    animal.AddBehaviour<GoToFoodBehaviour>()
                        .Initialize(animal, plant);
                    return false;
                }
            }
            
            return true;
        }
        
        private Vector3 NewWanderPoint(Vector3 current)
        {
            var wanderPointXY = Random.insideUnitCircle.normalized * _searchRadius;
            return (current + new Vector3(wanderPointXY.x, 0, wanderPointXY.y));
        }

        public override void Recycle()
        {
            AnimalBehaviourPool<SearchFoodBehaviour>.Reclaim(this);
        }
        
    }
}