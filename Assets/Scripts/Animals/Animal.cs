using System.Collections.Generic;
using UnityEngine;

namespace Svnvav.Samples
{
    public class Animal : Creature
    {
        private List<Effect> _effects;
        protected List<AnimalBehaviour> _behaviours;

        

        public bool Dead;
        
        private void Awake()
        {
            _behaviours = new List<AnimalBehaviour>();
        }

        public T AddBehaviour<T>() where T : AnimalBehaviour, new()
        {
            var component = AnimalBehaviourPool<T>.Get();
            _behaviours.Add(component);
            return component;
        }

        public override void GameUpdate()
        {
            ProcessBehaviours();
            if (Dead)
            {
                Recycle();
            }
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
        
        public void Recycle()
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