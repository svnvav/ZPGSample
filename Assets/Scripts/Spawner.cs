using UnityEngine;

namespace Svnvav.Samples
{
    public abstract class Spawner : MonoBehaviour
    {
        [SerializeField, Range(0f, 50f)] private float _spawnSpeed;

        private float _spawnProgress;
        
        public void GameUpdate()
        {
            _spawnProgress += Time.deltaTime * _spawnSpeed;
            while (_spawnProgress >= 1f)
            {
                _spawnProgress -= 1f;
                Spawn();
            }
        }

        public abstract void Spawn();
    }
}