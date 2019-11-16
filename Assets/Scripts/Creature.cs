using System;
using System.Collections.Generic;
using UnityEngine;

namespace Svnvav.Samples
{
    public abstract class Creature : MonoBehaviour
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

        [SerializeField]
        private bool _isAlive;

        public bool IsAlive
        {
            get => _isAlive;
            set => _isAlive = value;
        }

        public abstract void GameUpdate();

        public virtual void Recycle()
        {
            OriginFactory.Reclaim(this);
        }

        public void Die()
        {
            IsAlive = false;
            Game.Instance.Kill(this);
        }
    }
}