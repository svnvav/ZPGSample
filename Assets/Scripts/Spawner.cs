using UnityEngine;

namespace Svnvav.Samples
{
    public class Spawner : MonoBehaviour
    {
        [System.Serializable]
        public struct SpawnConfiguration
        {
            public CreatureFactory factory;
        }
        
        [SerializeField, Range(0f, 50f)] private float _spawnSpeed;
        
        [SerializeField] private SpawnConfiguration _config;
        
        private float _spawnProgress;
        
        public void GameUpdate()
        {
            _spawnProgress += Time.deltaTime * _spawnSpeed;
            while (_spawnProgress >= 1f)
            {
                _spawnProgress -= 1f;
                SpawnCreature();
            }
        }
        
        public void SpawnCreature()
        {
            Creature instance = _config.factory.Get();
            instance.gameObject.layer = gameObject.layer;
            instance.transform.position = transform.position;
            instance.AddBehaviour<SearchFoodBehaviour>();
        }
    }
}