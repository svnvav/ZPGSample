using UnityEngine;

namespace Svnvav.Samples
{
    public class Steal : StateComponent
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
            _skyvan.Inventory.Put(_stealTarget.Inventory.Items[0]);
            creature.StateMachine.MoveNext(Command.Stole);
        }

        public override void Exit(Creature creature)
        {
        }
    }
}