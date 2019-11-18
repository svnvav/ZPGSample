using UnityEngine;

namespace Svnvav.Samples
{
    public class GoblinGoToTarget : StateComponent
    {
        [SerializeField] private float _enemyDetectionRadius;
        
        private Goblin _goblin;

        public override void Enter(Creature goblin)
        {
            _goblin = (Goblin) goblin;
        }
        
        public override void GameUpdate(Creature creature)
        {
            CheckNearCreatures(_goblin);
            if (_goblin.Target == null)
            {
                _goblin.StateMachine.MoveNext(Command.TargetLost);
            }

            var positionDif = transform.position - _goblin.Target.position;
            
            if (positionDif.x * positionDif.x + positionDif.z * positionDif.z < 1.5f)
            {
                OnTargetReach();
            }
            else
            {
                _goblin.NavMeshAgent.SetDestination(_goblin.Target.position);
            }
        }
        
        private void OnTargetReach()
        {
            Skyvan skyvan;
            if (_goblin.Target.TryGetComponent(out skyvan))
            {
                _goblin.StateMachine.MoveNext(Command.EnemyFound);
            }
            Item item;
            if (_goblin.Target.TryGetComponent(out item))
            {
                _goblin.StateMachine.MoveNext(Command.ItemFound);
            }
            Plant plant;
            if (_goblin.Target.TryGetComponent(out plant))
            {
                _goblin.StateMachine.MoveNext(Command.FoodFound);
            }
        }
        
        private void CheckNearCreatures(Goblin goblin)
        {
            var position = goblin.transform.position;
            var colliders = Physics.OverlapSphere(
                position, _enemyDetectionRadius
            );

            foreach (var collider in colliders)
            {
                var enemy = collider.GetComponent<Skyvan>();
                if (enemy != null && enemy.IsAlive)
                {
                    goblin.Target = enemy.transform;
                    break;
                }
            }
        }

        public override void Exit(Creature goblin)
        {
        }
    }
}