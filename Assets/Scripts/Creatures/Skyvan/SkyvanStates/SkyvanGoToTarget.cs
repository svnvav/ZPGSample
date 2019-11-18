using UnityEngine;

namespace Svnvav.Samples
{
    public class SkyvanGoToTarget : StateComponent
    {
        private Skyvan _skyvan;
        
        public override void Enter(Creature creature)
        {
            _skyvan = (Skyvan) creature;
        }

        public override void GameUpdate(Creature creature)
        {
            if (_skyvan.Target == null)
            {
                creature.StateMachine.MoveNext(Command.TargetLost);
            }

            var positionDif = transform.position - _skyvan.Target.position;
            
            if (positionDif.x * positionDif.x + positionDif.z * positionDif.z < 1.5f)
            {
                OnTargetReach();
            }
            else
            {
                _skyvan.NavMeshAgent.SetDestination(_skyvan.Target.position);
            }
        }

        private void OnTargetReach()
        {
            Item item;
            if (_skyvan.Target.TryGetComponent(out item))
            {
                _skyvan.StateMachine.MoveNext(Command.ItemFound);
            }
            Inventory inventory;
            if (_skyvan.Target.TryGetComponent(out inventory))
            {
                _skyvan.StateMachine.MoveNext(Command.InventoryFound);
            }
        }
        
        public override void Exit(Creature creature)
        {
            
        }
    }
}