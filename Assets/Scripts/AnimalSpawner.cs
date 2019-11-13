using UnityEngine;

namespace Svnvav.Samples
{
    public class AnimalSpawner : Spawner
    {
        [System.Serializable]
        public struct SpawnConfiguration
        {
            public CreatureFactory factory;
            public float searchFoodRadius;
        }
        
        [SerializeField] private SpawnConfiguration _config;
        
        public override void Spawn()
        {
            Animal animal = _config.factory.Get<Animal>();
            animal.gameObject.layer = gameObject.layer;
            animal.transform.position = transform.position;
            animal.AddBehaviour<SearchFoodBehaviour>()
                .Initialize(animal, _config.searchFoodRadius);
        }
    }
}