using UnityEngine;

namespace Svnvav.Samples
{
    public class SearchStealTarget : StateComponent
    {
        [SerializeField] private float _searchRadius;
        private Vector3 _wanderPoint;
        
        private Skyvan _skyvan;
        public override void Enter(Creature creature)
        {
            _skyvan = (Skyvan) creature;
            _wanderPoint = NewWanderPoint(_skyvan.transform.position);
            _skyvan.NavMeshAgent.SetDestination(_wanderPoint);
        }

        public override void GameUpdate(Creature creature)
        {
            var positionDif = creature.transform.position - _wanderPoint;
            
            if (positionDif.x * positionDif.x + positionDif.z * positionDif.z < 0.01f)
            {
                _wanderPoint = NewWanderPoint(creature.transform.position);
                _skyvan.NavMeshAgent.SetDestination(_wanderPoint);
            }
            CheckNearCreatures(_skyvan);
        }

        public override void Exit(Creature creature)
        {
            
        }
        
        private void CheckNearCreatures(Skyvan skyvan)
        {
            var position = skyvan.transform.position;
            var colliders = Physics.OverlapSphere(
                position, _searchRadius
            );

            foreach (var collider in colliders)
            {
                var enemy = collider.GetComponent<Goblin>();
                if (enemy != null)
                {
                    skyvan.Target = enemy.transform;
                    skyvan.StateMachine.MoveNext(Command.FoundStealTarget);
                    break;
                }
            }
        }
        
        private Vector3 NewWanderPoint(Vector3 current)
        {
            var wanderPointXY = Random.insideUnitCircle.normalized * _searchRadius;
            return (current + new Vector3(wanderPointXY.x, 0, wanderPointXY.y));
        }
    }
}