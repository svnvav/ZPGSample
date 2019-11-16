using UnityEngine;

namespace Svnvav.Samples
{
    public class SearchFood : StateComponent
    {
        [SerializeField] private float _searchRadius;
        private Vector3 _wanderPoint;
        
        public override void Enter(AnimalWithFsm animal)
        {
            _wanderPoint = NewWanderPoint(animal.transform.position);
            animal.NavMeshAgent.SetDestination(_wanderPoint);
        }
        
        public override void GameUpdate(AnimalWithFsm animal)
        {
            var positionDif = animal.transform.position - _wanderPoint;
            
            if (positionDif.x * positionDif.x + positionDif.z * positionDif.z < 0.01f)
            {
                _wanderPoint = NewWanderPoint(animal.transform.position);
                animal.NavMeshAgent.SetDestination(_wanderPoint);
            }
            CheckNearPlant(animal);
        }
        
        private void CheckNearPlant(AnimalWithFsm animal)
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
                if (plant != null && plant.IsAlive)
                {
                    animal.Target = plant.transform;
                    animal.StateMachine.MoveNext(Command.FoundFood);
                }
            }
        }
        
        private Vector3 NewWanderPoint(Vector3 current)
        {
            var wanderPointXY = Random.insideUnitCircle.normalized * _searchRadius;
            return (current + new Vector3(wanderPointXY.x, 0, wanderPointXY.y));
        }

        public override void Exit(AnimalWithFsm animal)
        {
        }
    }
}