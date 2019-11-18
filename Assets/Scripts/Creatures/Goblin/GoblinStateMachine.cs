using System.Collections.Generic;
using UnityEngine;

namespace Svnvav.Samples
{
    public class GoblinStateMachine : CreatureStateMachine
    {
        [SerializeField] private StateComponent Dead;
        [SerializeField] private StateComponent Dying;
        [SerializeField] private StateComponent Search;
        [SerializeField] private StateComponent GoToTarget;
        [SerializeField] private StateComponent Eating;
        [SerializeField] private StateComponent Grab;
        [SerializeField] private StateComponent Attack;

        protected override StateComponent InitState => Search;

        protected override Dictionary<StateTransition, StateComponent> Transitions => new Dictionary<StateTransition, StateComponent>()
            {
                {new StateTransition(Dead, Command.Spawn), Search},
                
                {new StateTransition(Search, Command.Die), Dying},
                {new StateTransition(Search, Command.TargetFound), GoToTarget},

                {new StateTransition(GoToTarget, Command.Die), Dying},
                {new StateTransition(GoToTarget, Command.FoodFound), Eating},
                {new StateTransition(GoToTarget, Command.TargetLost), Search},
                {new StateTransition(GoToTarget, Command.EnemyFound), Attack},
                {new StateTransition(GoToTarget, Command.ItemFound), Grab},
                
                {new StateTransition(Attack, Command.Die), Dead},
                {new StateTransition(Attack, Command.TargetLost), Search},
                
                {new StateTransition(Eating, Command.Die), Dead},
                {new StateTransition(Eating, Command.TargetLost), Search},
                
                {new StateTransition(Grab, Command.Die), Dead},
                {new StateTransition(Grab, Command.TargetLost), Search},
                
                {new StateTransition(Dying, Command.Die), Dead},
            };
    }
}