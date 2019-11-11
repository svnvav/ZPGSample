﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Svnvav.Samples
{
    [CreateAssetMenu]
    public class CreatureFactory : ScriptableObject
    {
        [SerializeField] private Creature[] _creatures;
        
        [SerializeField] private bool _recycle = true;
        
        [NonSerialized] private List<Creature>[] _pools;

        [NonSerialized] private Scene _poolScene;

        void CreatePools()
        {
            _pools = new List<Creature>[_creatures.Length];
            for (int i = 0; i < _pools.Length; i++)
            {
                _pools[i] = new List<Creature>();
            }

#if UNITY_EDITOR
            _poolScene = SceneManager.GetSceneByName(name);
            if (_poolScene.isLoaded)
            {
                var inactiveShapes = _poolScene
                    .GetRootGameObjects()
                    .Where(go => !go.activeSelf)
                    .Select(go => go.GetComponent<Creature>());
                foreach (var shape in inactiveShapes)
                {
                    _pools[shape.ShapeId].Add(shape);
                }
                return;
            }
#endif

            _poolScene = SceneManager.CreateScene(name);
        }

        public Creature Get(int shapeId = 0)
        {
            Creature instance;

            if (_recycle)
            {
                if (_pools == null)
                {
                    CreatePools();
                }

                List<Creature> pool = _pools[shapeId];
                int lastIndex = pool.Count - 1;
                
                
                if (lastIndex >= 0)
                {
                    instance = pool[lastIndex];
                    pool.RemoveAt(lastIndex);
                }
                else
                {
                    instance = Instantiate(_creatures[shapeId]);
                    instance.OriginFactory = this;
                    instance.ShapeId = shapeId;
                    SceneManager.MoveGameObjectToScene(instance.gameObject, _poolScene);
                }

                instance.gameObject.SetActive(true);
            }
            else
            {
                instance = Instantiate(_creatures[shapeId]);
                instance.ShapeId = shapeId;
            }
            

            Game.Instance.AddCreature(instance);
            return instance;
        }
        
        public void Reclaim(Creature instanceToRecycle)
        {
            if (instanceToRecycle.OriginFactory != this) {
                Debug.LogError("Tried to reclaim shape with wrong factory.");
                return;
            }
            
            if (_recycle)
            {
                if (_pools == null)
                {
                    CreatePools();
                }

                _pools[instanceToRecycle.ShapeId].Add(instanceToRecycle);
                instanceToRecycle.gameObject.SetActive(false);
            }
            else
            {
                Destroy(instanceToRecycle.gameObject);
            }
        }
    }
}