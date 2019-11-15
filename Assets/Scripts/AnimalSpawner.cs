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
            public float moveSpeed;
            public float maxAge;
            public float maxHunger;
            public float maxHealth;
        }
        
        [SerializeField] private SpawnConfiguration _config;
        
        public override void Spawn()
        {
            Animal animal = _config.factory.Get<Animal>();
            animal.gameObject.layer = gameObject.layer;
            animal.transform.position = transform.position;
            animal.Initialize(_config.moveSpeed);
            animal.AddBehaviour<SearchFoodBehaviour>()
                .Initialize(animal, _config.searchFoodRadius);
            animal.AddBehaviour<BasicLifeBehaviour>()
                .Initialize(animal, _config.maxAge, _config.maxHealth, _config.maxHunger);
        }
    }
}