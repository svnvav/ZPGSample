using System;
using System.Collections.Generic;
using UnityEngine;

namespace Svnvav.Samples
{
    public class Game : MonoBehaviour
    {
        public static Game Instance;

        [SerializeField] private Spawner[] _spawners;
        
        private List<Creature> _creatures, _killList;
        
        private bool _inGameUpdateLoop;

        private void Awake()
        {
            _creatures = new List<Creature>();
            _killList = new List<Creature>();
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
            foreach (var spawner in _spawners)
            {
                spawner.GameUpdate();
            }
            _inGameUpdateLoop = false;
            
            if (_killList.Count > 0) {
                for (int i = 0; i < _killList.Count; i++) {
                    KillImmediately(_killList[i]);
                }
                _killList.Clear();
            }
        }

        public void AddCreature(Creature instance)
        {
            _creatures.Add(instance);
        }
        
        public void Kill(Creature creature)
        {
            if (_inGameUpdateLoop) {
                _killList.Add(creature);
            }
            else {
                KillImmediately(creature);
            }
        }
        
        private void KillImmediately (Creature creature)
        {
            creature.Recycle();
            _creatures.Remove(creature);
        }
    }
}