using UnityEngine;
using UnityEngine.AI;

namespace Svnvav.Samples
{
    public class BasicLifeBehaviour : AnimalBehaviour
    {
        public override AnimalBehaviourType BehaviorType => AnimalBehaviourType.BasicLife;
        
        [SerializeField]
        private float _maxAge;
        [SerializeField]
        private float _maxHealth;
        [SerializeField]
        private float _maxHunger;
        


        public void Initialize(Animal animal, float maxAge, float maxHealth, float maxHunger)
        {
            _maxAge = maxAge;
            _maxHealth = maxHealth;
            _maxHunger = maxHunger;

            animal.Age = maxAge;
            animal.Health = maxHealth;
            animal.Hunger = maxHunger;
        }

        public override bool GameUpdate(Animal animal)
        {
            if (animal.Health < 0f)
            {
                animal.Dead = true;
                return false;
            }

            if (animal.Health > _maxHealth)
            {
                animal.Health -= Time.deltaTime;
            }
            if (animal.Age > _maxAge)
            {
                animal.Health -= Time.deltaTime;
            }
            
            if (animal.Hunger > _maxHunger)
            {
                animal.Health -= Time.deltaTime;
            }
            else
            {
                animal.Hunger += Time.deltaTime;
            }

            animal.Age += Time.deltaTime;

            return true;
        }

        public override void Recycle()
        {
            AnimalBehaviourPool<BasicLifeBehaviour>.Reclaim(this);
        }
        
    }
}