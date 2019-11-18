using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Svnvav.Samples
{
    public class SkyvanStateMachine : CreatureStateMachine
    {
        [SerializeField] private StateComponent Dead;
        [SerializeField] private StateComponent Dying;
        [SerializeField] private StateComponent Search;
        [SerializeField] private StateComponent GoToTarget;
        [SerializeField] private StateComponent Steal;
        [SerializeField] private StateComponent Grab;
        [SerializeField] private StateComponent Escape;
        
        protected override StateComponent InitState => Search;
        protected override Dictionary<StateTransition, StateComponent> Transitions => new Dictionary<StateTransition, StateComponent>()
        {
            {new StateTransition(Dead, Command.Spawn), Search},
            {new StateTransition(Search, Command.Die), Dying},
            {new StateTransition(Search, Command.TargetFound), GoToTarget},
            {new StateTransition(Search, Command.SeenByEnemy), Escape},
            
            {new StateTransition(GoToTarget, Command.Die), Dying},
            {new StateTransition(GoToTarget, Command.InventoryFound), Steal},
            {new StateTransition(GoToTarget, Command.ItemFound), Grab},
            {new StateTransition(GoToTarget, Command.SeenByEnemy), Escape},
            
            {new StateTransition(Steal, Command.Die), Dying},
            {new StateTransition(Steal, Command.TargetLost), Escape},
            
            {new StateTransition(Grab, Command.Die), Dying},
            {new StateTransition(Grab, Command.TargetLost), Escape},
            
            {new StateTransition(Escape, Command.Die), Dying},
            {new StateTransition(Escape, Command.Escaped), Search},
            
            {new StateTransition(Dying, Command.Die), Dead}
        };
    }
}