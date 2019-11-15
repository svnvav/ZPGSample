using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Svnvav.Samples
{
    public class Animal : Creature
    {
        private List<Effect> _effects;
        protected List<AnimalBehaviour> _behaviours;
        
        private NavMeshAgent _navMeshAgent;

        public NavMeshAgent NavMeshAgent => _navMeshAgent;

        public bool Dead { get; set; }

        #region Props
        [SerializeField]
        private float _age;
        [SerializeField]
        private float _health;
        [SerializeField]
        private float _hunger;
        [SerializeField]
        private float _moveSpeed;

        public float Age
        {
            get => _age;
            set => _age = value;
        }

        public float Health
        {
            get => _health;
            set => _health = value;
        }

        public float Hunger
        {
            get => _hunger;
            set => _hunger = value;
        }

        public float MoveSpeed
        {
            get => _moveSpeed;
            set
            {
                _moveSpeed = value;
                _navMeshAgent.speed = value;
            }
        }

        #endregion

        private void Awake()
        {
            _effects = new List<Effect>();
            _behaviours = new List<AnimalBehaviour>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void Initialize(float moveSpeed)
        {
            Dead = false;
            _hunger = 0;
            _health = 0;
            _age = 0;
            _moveSpeed = moveSpeed;
        }

        public override void GameUpdate()
        {
            ProcessBehaviours();
            ProcessEffects();
            if (Dead)
            {
                Debug.Log(_age);
                Die();
            }
        }
        
        public T AddBehaviour<T>() where T : AnimalBehaviour, new()
        {
            var component = AnimalBehaviourPool<T>.Get();
            _behaviours.Add(component);
            return component;
        }

        public void TakeEffect(Effect effect)
        {
            _effects.Add(effect);
        }
        
        private void ProcessBehaviours()
        {
            for (var i = 0; i < _behaviours.Count; i++)
            {
                var behaviour = _behaviours[i];
                if (!behaviour.GameUpdate(this))
                {
                    behaviour.OnDisable(this);
                    behaviour.Recycle();
                    _behaviours.RemoveAt(i--);
                }
            }
        }

        private void ProcessEffects()
        {
            for (var i = 0; i < _effects.Count; i++)
            {
                var effect = _effects[i];
                if (!effect.Apply(this))
                {
                    _effects.RemoveAt(i--);
                }
            }
        }
        
        public override void Recycle()
        {
            for (int i = 0; i < _behaviours.Count; i++) {
                _behaviours[i].Recycle();
            }
            _behaviours.Clear();
            
            base.Recycle();
            Dead = false;
        }
    }
}