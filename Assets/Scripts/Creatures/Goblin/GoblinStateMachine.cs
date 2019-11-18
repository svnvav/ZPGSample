using System;
using System.Collections.Generic;
using UnityEngine;

namespace Svnvav.Samples
{
    public class GoblinStateMachine : CreatureStateMachine
    {
        [SerializeField] private StateComponent Dead;
        [SerializeField] private StateComponent Dying;
        [SerializeField] private StateComponent SearchFood;
        [SerializeField] private StateComponent GoToFood;
        [SerializeField] private StateComponent Eating;
        [SerializeField] private StateComponent Attack;

        protected override StateComponent InitState => SearchFood;

        protected override Dictionary<StateTransition, StateComponent> Transitions => new Dictionary<StateTransition, StateComponent>()
            {
                {new StateTransition(Dead, Command.Spawn), SearchFood},
                
                {new StateTransition(SearchFood, Command.Die), Dying},
                {new StateTransition(SearchFood, Command.FoundFood), GoToFood},
                {new StateTransition(SearchFood, Command.EnemyFound), Attack},
                
                {new StateTransition(GoToFood, Command.Die), Dying},
                {new StateTransition(GoToFood, Command.FoodReached), Eating},
                {new StateTransition(GoToFood, Command.Lost), SearchFood},
                {new StateTransition(GoToFood, Command.EnemyFound), Attack},
                
                {new StateTransition(Attack, Command.Die), Dead},
                {new StateTransition(Attack, Command.EnemyDead), SearchFood},
                
                {new StateTransition(Eating, Command.Die), Dying},
                {new StateTransition(Eating, Command.Ate), SearchFood},
                
                {new StateTransition(Dying, Command.Die), Dead},
            };
    }
}