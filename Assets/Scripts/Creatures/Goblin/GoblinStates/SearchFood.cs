using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Svnvav.Samples
{
    public class SearchFood : StateComponent
    {
        [SerializeField] private float _searchRadius;
        private Vector3 _wanderPoint;

        private Goblin _goblin;

        public override void Enter(Creature goblin)
        {
            _goblin = (Goblin) goblin;
            _wanderPoint = NewWanderPoint(goblin.transform.position);
            _goblin.NavMeshAgent.SetDestination(_wanderPoint);
        }
        
        public override void GameUpdate(Creature goblin)
        {
            var positionDif = goblin.transform.position - _wanderPoint;
            
            if (positionDif.x * positionDif.x + positionDif.z * positionDif.z < 0.01f)
            {
                _wanderPoint = NewWanderPoint(goblin.transform.position);
                _goblin.NavMeshAgent.SetDestination(_wanderPoint);
            }
            CheckNearCreatures(_goblin);
        }
        
        private void CheckNearCreatures(Goblin goblin)
        {
            var position = goblin.transform.position;
            var colliders = Physics.OverlapSphere(
                position, _searchRadius
            );

            foreach (var collider in colliders)
            {
                var enemy = collider.GetComponent<Skyvan>();
                if (enemy != null && enemy.IsAlive)
                {
                    goblin.Target = enemy.transform;
                    goblin.StateMachine.MoveNext(Command.EnemyFound);
                    break;
                }
                var plant = collider.GetComponent<Plant>();
                if (plant != null && plant.IsAlive)
                {
                    goblin.Target = plant.transform;
                    goblin.StateMachine.MoveNext(Command.FoundFood);
                    break;
                }
            }
        }
        
        private Vector3 NewWanderPoint(Vector3 current)
        {
            var wanderPointXY = Random.insideUnitCircle.normalized * _searchRadius;
            return (current + new Vector3(wanderPointXY.x, 0, wanderPointXY.y));
        }

        public override void Exit(Creature goblin)
        {
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _searchRadius);
        }
    }
}