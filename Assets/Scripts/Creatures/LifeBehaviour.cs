using UnityEngine;

namespace Svnvav.Samples
{
    public class LifeBehaviour : StateComponent
    {
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

        public float MaxAge => _maxAge;

        public float MaxHealth => _maxHealth;

        public float MaxHunger => _maxHunger;

        public float Age => _age;

        public float Health => _health;

        public float Hunger => _hunger;
        
        public override void Enter(Creature goblin)
        {
        }

        public override void GameUpdate(Creature creature)
        {
            if (_health < 0f)
            {
                creature.StateMachine.MoveNext(Command.Die);
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
        }
        
        public override void Exit(Creature goblin)
        {
        }
    }
}