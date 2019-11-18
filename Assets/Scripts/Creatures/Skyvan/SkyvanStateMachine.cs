using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Svnvav.Samples
{
    public class SkyvanStateMachine : CreatureStateMachine
    {
        [SerializeField] private StateComponent Dead;
        [SerializeField] private StateComponent Dying;
        [SerializeField] private StateComponent SearchStealTarget;
        [SerializeField] private StateComponent GoToStealTarget;
        [SerializeField] private StateComponent Steal;
        [SerializeField] private StateComponent Escape;
        
        protected override Dictionary<StateTransition, StateComponent> Transitions => new Dictionary<StateTransition, StateComponent>()
        {
            {new StateTransition(Dead, Command.Spawn), SearchStealTarget},
            {new StateTransition(SearchStealTarget, Command.Die), Dying},
            {new StateTransition(SearchStealTarget, Command.FoundStealTarget), GoToStealTarget},
            {new StateTransition(SearchStealTarget, Command.SeenByEnemy), Escape},
            
            {new StateTransition(GoToStealTarget, Command.Die), Dying},
            {new StateTransition(GoToStealTarget, Command.ReachedStealTarget), Steal},
            {new StateTransition(GoToStealTarget, Command.SeenByEnemy), Escape},
            
            {new StateTransition(Steal, Command.Die), Dying},
            {new StateTransition(Steal, Command.Stole), Escape},
            
            {new StateTransition(Escape, Command.Die), Dying},
            {new StateTransition(Escape, Command.Escaped), SearchStealTarget},
            
            {new StateTransition(Dying, Command.Die), Dead}
        };
        protected override StateComponent InitState => SearchStealTarget;
    }
}