using UnityEngine;

namespace Svnvav.Samples
{
    public class GoblinGoToTarget : StateComponent
    {
        [SerializeField] private float _enemyDetectionRadius;
        [SerializeField] private float _targetReachedRadius;
        
        private Goblin _goblin;

        public override void Enter(Creature creature)
        {
            _goblin = (Goblin) creature;
            creature.Animator.Play("Walk");
        }
        
        public override void GameUpdate(Creature creature)
        {
            CheckNearCreatures(_goblin);
            if (_goblin.Target == null)
            {
                _goblin.StateMachine.MoveNext(Command.TargetLost);
            }

            var positionDif = transform.position - _goblin.Target.position;
            
            if (positionDif.x * positionDif.x + positionDif.y * positionDif.y < _targetReachedRadius * _targetReachedRadius)
            {
                OnTargetReach();
            }
            else
            {
                _goblin.NavTileAgent.SetDestination(_goblin.Target.position);
            }
        }
        
        private void OnTargetReach()
        {
            _goblin.NavTileAgent.Stop();
            Skyvan skyvan;
            if (_goblin.Target.TryGetComponent(out skyvan))
            {
                _goblin.StateMachine.MoveNext(Command.EnemyFound);
                return;
            }
            Item item;
            if (_goblin.Target.TryGetComponent(out item))
            {
                _goblin.StateMachine.MoveNext(Command.ItemFound);
                return;
            }
            Plant plant;
            if (_goblin.Target.TryGetComponent(out plant))
            {
                _goblin.StateMachine.MoveNext(Command.FoodFound);
                return;
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