using UnityEngine;

namespace Svnvav.Samples
{
    public class Steal : StateComponent
    {
        [SerializeField] private Goblin _stealTarget;
        [SerializeField] private float _duration;

        private float _progress;
        
        private Skyvan _skyvan;
        
        public override void Enter(Creature creature)
        {
            _skyvan = (Skyvan) creature;
            _stealTarget = _skyvan.Target.GetComponent<Goblin>();
            _progress = 0f;
        }
        
        public override void GameUpdate(Creature creature)
        {
            if (_progress < _duration)
            {
                _progress += Time.deltaTime;
                return;
            }
            
            if (_stealTarget.Inventory.ItemsCount > 0)
            {
                var toSteal = _stealTarget.Inventory.Grab(0);
                
                _skyvan.Inventory.Put(toSteal);
            }

            creature.StateMachine.MoveNext(Command.TargetLost);
        }

        public override void Exit(Creature creature)
        {
        }
    }
}