using UnityEngine;

namespace Svnvav.Samples
{
    public class Grab : StateComponent
    {
        public override void Enter(Creature creature)
        {
            var item = creature.Target.GetComponent<Item>();
            creature.Inventory.Put(item);
            creature.StateMachine.MoveNext(Command.TargetLost);
        }
        
        public override void GameUpdate(Creature creature)
        {
        }

        public override void Exit(Creature creature)
        {
        }
    }
}