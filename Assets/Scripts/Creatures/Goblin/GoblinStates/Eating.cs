using UnityEngine;

namespace Svnvav.Samples
{
    public class Eating : StateComponent, Eater
    {
        [SerializeField]
        private Plant _plant;

        private Goblin _goblin;
        
        public override void Enter(Creature goblin)
        {
            _goblin = (Goblin) goblin;
            _plant = _goblin.Target.GetComponent<Plant>();
            if (_plant == null || !_plant.IsAlive)
            {
                _goblin.StateMachine.MoveNext(Command.Ate);
            }
        }
        
        public override void GameUpdate(Creature goblin)
        {
            var effects = Eat(_plant);
            goblin.TakeEffects(effects);
            if (_plant == null || !_plant.IsAlive)
            {
                goblin.StateMachine.MoveNext(Command.Ate);
            }
            
        }

        public override void Exit(Creature goblin)
        {
        }

        public Effect[] Eat(Eatable eatable)
        {
            return eatable.ToBeEaten(this);
        }
    }
}