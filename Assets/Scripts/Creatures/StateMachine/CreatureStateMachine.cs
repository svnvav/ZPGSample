using System;
using System.Collections.Generic;
using UnityEngine;

namespace Svnvav.Samples
{
    public abstract class CreatureStateMachine : MonoBehaviour
    {
        protected StateComponent CurrentState { get; set; }

        protected Creature _owner;
        
        protected abstract Dictionary<StateTransition, StateComponent> Transitions { get; }
        protected abstract StateComponent InitState { get; }

        public void Initialize(Creature owner)
        {
            _owner = owner;
            
            foreach (var stateComponent in Transitions.Values)
            {
                stateComponent.enabled = false;
            }

            CurrentState = InitState;
            CurrentState.enabled = true;
            CurrentState.Enter(_owner);
        }

        public void GameUpdate(Creature creature)
        {
            CurrentState.GameUpdate(creature);
        }
        
        private StateComponent GetNext(Command command)
        {
            StateTransition transition = new StateTransition(CurrentState, command);
            StateComponent nextState;
            if (!Transitions.TryGetValue(transition, out nextState))
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