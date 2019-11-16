using UnityEngine;

namespace Svnvav.Samples
{
    public class AnimalSpawner : Spawner
    {
        [SerializeField] private AnimalSpawnConfiguration _config;
        
        public override void Spawn()
        {
            Animal animal = _config.factory.Get<Animal>();
            animal.gameObject.layer = gameObject.layer;
            animal.transform.position = transform.position;
            animal.Initialize(_config);
            animal.AddBehaviour<SearchFoodBehaviour>()
                .Initialize(animal);
            animal.AddBehaviour<BasicLifeBehaviour>()
                .Initialize(animal, _config.maxAge, _config.maxHealth, _config.maxHunger);
        }
    }
}