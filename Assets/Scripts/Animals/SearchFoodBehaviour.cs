using UnityEngine;
using UnityEngine.AI;

namespace Svnvav.Samples
{
    public class SearchFoodBehaviour : AnimalBehaviour
    {
        public override AnimalBehaviourType BehaviorType => AnimalBehaviourType.SearchFood;
        
        private float _searchRadius;

        private Vector3 _wanderPoint;

        private NavMeshAgent _navMeshAgent;

        public void Initialize(Animal animal, float searchRadius)
        {
            _searchRadius = searchRadius;
            
            _navMeshAgent = animal.GetComponent<NavMeshAgent>();
            
            _wanderPoint = NewWanderPoint(animal.transform.position);
            _navMeshAgent.SetDestination(_wanderPoint);
        }

        public override bool GameUpdate(Animal animal)
        {
            _navMeshAgent.speed = animal.MoveSpeed;
            var positionDif = animal.transform.position - _wanderPoint;

            if (positionDif.x * positionDif.x + positionDif.z * positionDif.z < 0.01f)
            {
                _wanderPoint = NewWanderPoint(animal.transform.position);
                _navMeshAgent.SetDestination(_wanderPoint);
                
            }
            return true;
        }
        
        private Vector3 NewWanderPoint(Vector3 current)
        {
            var wanderPointXY = Random.insideUnitCircle * _searchRadius;
            return (current + new Vector3(wanderPointXY.x, 0, wanderPointXY.y));
        }

        public override void Recycle()
        {
            AnimalBehaviourPool<SearchFoodBehaviour>.Reclaim(this);
        }
        
    }
}