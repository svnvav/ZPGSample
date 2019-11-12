using System;
using System.Collections.Generic;
using UnityEngine;

namespace Svnvav.Samples
{
    public class Creature : MonoBehaviour
    {
        private int _creatureId = int.MinValue;
        
        public int CreatureId {
            get => _creatureId;
            set {
                if (_creatureId == int.MinValue) {
                    _creatureId = value;
                }
                else {
                    Debug.LogError("Not allowed to change shapeId.");
                }
            }
        }
        
        private CreatureFactory _originFactory;
        
        public CreatureFactory OriginFactory
        {
            get => _originFactory;
            set
            {
                if (_originFactory == null) {
                    _originFactory = value;
                }
                else {
                    Debug.LogError("Not allowed to change origin factory.");
                }
            }
        }
        
        protected List<CreatureBehaviour> _behaviours;

        private void Awake()
        {
            _behaviours = new List<CreatureBehaviour>();
        }

        public T AddBehaviour<T>() where T : CreatureBehaviour, new()
        {
            var component = CreatureBehaviourPool<T>.Get();
            _behaviours.Add(component);
            return component;
        }
        
        public void GameUpdate()
        {
            for (var i = 0; i < _behaviours.Count; i++)
            {
                var behaviour = _behaviours[i];
                if (!behaviour.GameUpdate(this))
                {
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
            OriginFactory.Reclaim(this);
        }
    }
}