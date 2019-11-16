using System;
using System.Collections.Generic;
using UnityEngine;

namespace Svnvav.Samples
{
    public class AnimalStateMachine : MonoBehaviour
    {
        [SerializeField] private StateComponent Dead;
        [SerializeField] private StateComponent Dying;
        [SerializeField] private StateComponent SearchFood;
        [SerializeField] private StateComponent GoToFood;
        [SerializeField] private StateComponent Eating;
        
        private Dictionary<StateTransition, StateComponent> _transitions;
        private StateComponent CurrentState { get; set; }

        private AnimalWithFsm _owner;
        
        public void Initialize(AnimalWithFsm owner)
        {
            _owner = owner;
            
            _transitions = new Dictionary<StateTransition, StateComponent>()
            {
                {new StateTransition(Dead, Command.Spawn), SearchFood},
                {new StateTransition(SearchFood, Command.Die), Dying},
                {new StateTransition(SearchFood, Command.FoundFood), GoToFood},
                {new StateTransition(GoToFood, Command.Die), Dying},
                {new StateTransition(GoToFood, Command.ComeToFood), Eating},
                {new StateTransition(GoToFood, Command.Lost), SearchFood},
                {new StateTransition(Eating, Command.Die), Dying},
                {new StateTransition(Eating, Command.Ate), SearchFood},
                {new StateTransition(Dying, Command.Die), Dead},
            };

            foreach (var stateComponent in _transitions.Values)
            {
                stateComponent.enabled = false;
            }
            
            CurrentState = Dead;
            MoveNext(Command.Spawn);
        }

        public void GameUpdate(AnimalWithFsm animalWithFsm)
        {
            CurrentState.GameUpdate(animalWithFsm);
        }
        
        private StateComponent GetNext(Command command)
        {
            StateTransition transition = new StateTransition(CurrentState, command);
            StateComponent nextState;
            if (!_transitions.TryGetValue(transition, out nextState))
                throw new Exception("Invalid transition: " + CurrentState + " -> " + command);
            return nextState;
        }

        public StateComponent MoveNext(Command command)
        {
            CurrentState.Exit(_owner);
            CurrentState.enabled = false;
            CurrentState = GetNext(command);
            CurrentState.enabled = true;
            CurrentState.Enter(_owner);
            return CurrentState;
        }
    }
}