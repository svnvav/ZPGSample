using System;
using System.Collections.Generic;
using UnityEngine;

namespace Svnvav.Samples
{
    public class Game : MonoBehaviour
    {
        public static Game Instance;
        
        private List<Creature> _creatures;

        private bool _inGameUpdateLoop;

        private void Awake()
        {
            _creatures = new List<Creature>();
        }

        private void OnEnable()
        {
            Instance = this;
        }

        private void Update()
        {
            _inGameUpdateLoop = true;
            foreach (var shape in _creatures)
            {
                shape.GameUpdate();
            }
            
            _inGameUpdateLoop = false;
        }

        public void AddCreature(Creature instance)
        {
            _creatures.Add(instance);
        }
    }
}