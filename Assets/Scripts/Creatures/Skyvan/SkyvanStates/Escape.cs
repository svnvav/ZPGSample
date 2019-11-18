using UnityEngine;

namespace Svnvav.Samples
{
    public class Escape : StateComponent
    {
        [SerializeField] private Goblin _enemy;
        
        private Skyvan _skyvan;
        
        [SerializeField] private float _escapeDistance;
        private Vector3 _escapePoint;
        
        public override void Enter(Creature creature)
        {
            _skyvan = (Skyvan) creature;
            _enemy = _skyvan.Target.GetComponent<Goblin>();
            _escapePoint = NewWanderPoint(_skyvan.transform.position);
            _skyvan.NavMeshAgent.SetDestination(_escapePoint);
        }

        public override void GameUpdate(Creature creature)
        {
            if (_enemy == null)
            {
                creature.StateMachine.MoveNext(Command.TargetLost);
            }

            var positionDif = transform.position - _enemy.transform.position;
            
            if (positionDif.x * positionDif.x + positionDif.z * positionDif.z > _escapeDistance * _escapeDistance)
            {
                creature.StateMachine.MoveNext(Command.Escaped);
            }
            
            positionDif = transform.position - _escapePoint;
            if(positionDif.x * positionDif.x + positionDif.z * positionDif.z < 0.5f)
            {
                _escapePoint = NewWanderPoint(_skyvan.transform.position);
                _skyvan.NavMeshAgent.SetDestination(_escapePoint);
            }
        }

        private Vector3 NewWanderPoint(Vector3 current)
        {
            var wanderPointXY = Random.insideUnitCircle.normalized * _escapeDistance;
            return (current + new Vector3(wanderPointXY.x, 0, wanderPointXY.y));
        }
        
        public override void Exit(Creature creature)
        {
            
        }
    }
}