using System.Collections.Generic;
using UnityEngine;

namespace Svnvav.Samples
{
    public abstract class Creature : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        public Animator Animator => _animator;
        
        [SerializeField] private CreatureStateMachine _stateMachine;
        public CreatureStateMachine StateMachine => _stateMachine;
        
        [SerializeField] private Inventory _inventory;
        public Inventory Inventory => _inventory;
        
        private List<Effect> _effects;
        
        public Transform Target { get; set; }

        public abstract bool IsAlive { get; }

        public virtual void Initialize()
        {
            StateMachine.Initialize(this);
            _effects = new List<Effect>();
        }
        
        private void FixedUpdate()
        {
            StateMachine.GameUpdate(this);
            GameUpdate();
        }

        protected virtual void GameUpdate()
        {
            
        }
        
        public void TakeEffects(Effect[] effects)
        {
            _effects.AddRange(effects);
        }
    }
}