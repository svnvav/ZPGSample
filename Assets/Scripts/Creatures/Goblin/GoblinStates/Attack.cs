using UnityEngine;

namespace Svnvav.Samples
{
    public class Attack : StateComponent
    {
        [SerializeField] private float _damagePerSecond = 100f;
        [SerializeField] private float _attackRange = 2f;
        [SerializeField] private Skyvan _enemy;
        
        private Goblin _goblin;
        
        public override void Enter(Creature goblin)
        {
            _goblin = (Goblin) goblin;
            _enemy = _goblin.Target.GetComponent<Skyvan>();
        }
        
        public override void GameUpdate(Creature goblin)
        {
            if (_enemy == null || !_enemy.IsAlive)
            {
                goblin.StateMachine.MoveNext(Command.TargetLost);
            }

            var positionDif = transform.position - _enemy.transform.position;
            
            if (positionDif.x * positionDif.x + positionDif.z * positionDif.z < _attackRange * _attackRange)
            {
                _enemy.GetComponent<LifeBehaviour>().TakeDamage(_damagePerSecond * Time.deltaTime);
            }
            else
            {
                _goblin.NavMeshAgent.SetDestination(_enemy.transform.position);
            }
        }

        public override void Exit(Creature goblin)
        {
        }
    }
}