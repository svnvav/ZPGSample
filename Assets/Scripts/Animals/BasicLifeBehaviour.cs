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
        
        [SerializeField]
        private float _age;
        [SerializeField]
        private float _health;
        [SerializeField]
        private float _hunger;
        

        public float Age => _age;

        public float Health => _health;
        

        public void Initialize(Animal animal, float maxAge, float maxHealth, float maxHunger)
        {
            _maxAge = maxAge;
            _age = maxAge;

            _maxHealth = maxHealth;
            _health = maxHealth;

            _maxHunger = maxHunger;
            _hunger = 0;
        }

        public override bool GameUpdate(Animal animal)
        {
            if (_health < 0f)
            {
                return false;
            }

            if (_health > _maxHealth)
            {
                _health -= Time.deltaTime;
            }
            if (_age > _maxAge)
            {
                _health -= Time.deltaTime;
            }
            
            if (_hunger > _maxHunger)
            {
                _health -= Time.deltaTime;
            }
            else
            {
                _hunger += Time.deltaTime;
            }

            _age += Time.deltaTime;

            return true;
        }

        public override void Recycle()
        {
            AnimalBehaviourPool<BasicLifeBehaviour>.Reclaim(this);
        }
        
    }
}