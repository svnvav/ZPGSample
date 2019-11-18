using System.Collections.Generic;
using UnityEngine;

namespace Svnvav.Samples
{
    public abstract class Creature : MonoBehaviour
    {
        private List<Effect> _effects;
        
        [SerializeField] private CreatureStateMachine _stateMachine;
        public CreatureStateMachine StateMachine => _stateMachine;
        
        [SerializeField] private Inventory _inventory;
        public Inventory Inventory => _inventory;
        
        public Transform Target { get; set; }

        public abstract bool IsAlive { get; }

        private void Awake()
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