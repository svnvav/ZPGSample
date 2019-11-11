using System;
using System.Collections.Generic;
using UnityEngine;

namespace Svnvav.Samples
{
    public abstract class Creature : MonoBehaviour
    {
        private int _shapeId = int.MinValue;
        
        public int ShapeId {
            get => _shapeId;
            set {
                if (_shapeId == int.MinValue) {
                    _shapeId = value;
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
        
        private List<CreatureBehaviour> _behaviours;

        private void Awake()
        {
            _behaviours = new List<CreatureBehaviour>();
        }

        public abstract bool GameUpdate();
        
        public abstract void Recycle();
    }
}