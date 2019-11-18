using UnityEngine;

namespace Svnvav.Samples
{
    public class GoToStealTarget : StateComponent
    {
        [SerializeField] private Goblin _stealTarget;
        
        private Skyvan _skyvan;
        
        public override void Enter(Creature creature)
        {
            _skyvan = (Skyvan) creature;
            _stealTarget = _skyvan.Target.GetComponent<Goblin>();
        }

        public override void GameUpdate(Creature creature)
        {
            if (_stealTarget == null)
            {
                creature.StateMachine.MoveNext(Command.Lost);
            }

            var positionDif = transform.position - _stealTarget.transform.position;
            
            if (positionDif.x * positionDif.x + positionDif.z * positionDif.z < 0.5f)
            {
                creature.StateMachine.MoveNext(Command.ReachedStealTarget);
            }
            else
            {
                _skyvan.NavMeshAgent.SetDestination(_stealTarget.Target.position);
            }
        }

        public override void Exit(Creature creature)
        {
            
        }
    }
}