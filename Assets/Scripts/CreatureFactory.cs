using System;
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

        private void CreatePools()
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
                    _pools[shape.CreatureId].Add(shape);
                }
                return;
            }
#endif

            _poolScene = SceneManager.CreateScene(name);
        }

        public T Get<T>(int creatureId = 0) where T : Creature
        {
            T instance;

            if (_recycle)
            {
                if (_pools == null)
                {
                    CreatePools();
                }

                List<Creature> pool = _pools[creatureId];
                int lastIndex = pool.Count - 1;
                
                
                if (lastIndex >= 0)
                {
                    instance = pool[lastIndex] as T;
                    pool.RemoveAt(lastIndex);
                }
                else
                {
                    instance = Instantiate(_creatures[creatureId]) as T;
                    instance.OriginFactory = this;
                    instance.CreatureId = creatureId;
                    SceneManager.MoveGameObjectToScene(instance.gameObject, _poolScene);
                }

                instance.gameObject.SetActive(true);
            }
            else
            {
                instance = Instantiate(_creatures[creatureId]) as T;
                instance.CreatureId = creatureId;
            }
            

            Game.Instance.AddCreature(instance);
            return instance;
        }
        
        public void Reclaim<T>(T instanceToRecycle) where T : Creature
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

                _pools[instanceToRecycle.CreatureId].Add(instanceToRecycle);
                instanceToRecycle.gameObject.SetActive(false);
            }
            else
            {
                Destroy(instanceToRecycle.gameObject);
            }
        }
    }
}