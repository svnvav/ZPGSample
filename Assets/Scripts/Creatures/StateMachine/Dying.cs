using UnityEngine;

namespace Svnvav.Samples
{
    public class Dying : StateComponent
    {
        [SerializeField] private float _duration;

        private float _remain;
        
        public override void Enter(Creature creature)
        {
            _remain = _duration;
            if (creature.Animator != null)
            {
                creature.Animator.Play("Dying");
            }
        }
        
        public override void GameUpdate(Creature creature)
        {
            _remain -= Time.deltaTime;
            if (_remain < 0f && creature.IsAlive)
            {
                creature.StateMachine.MoveNext(Command.Die);
            }
        }

        public override void Exit(Creature creature)
        {
        }
    }
}