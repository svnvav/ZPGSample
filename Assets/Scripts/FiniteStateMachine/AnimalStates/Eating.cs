using UnityEngine;

namespace Svnvav.Samples
{
    public class Eating : StateComponent
    {
        [SerializeField]
        private Plant _plant;
        
        public override void Enter(AnimalWithFsm animal)
        {
            _plant = animal.Target.GetComponent<Plant>();
            if (_plant == null || !_plant.IsAlive)
            {
                animal.StateMachine.MoveNext(Command.Ate);
            }
        }
        
        public override void GameUpdate(AnimalWithFsm animal)
        {
            _plant.Die();
            if (_plant == null || !_plant.IsAlive)
            {
                animal.StateMachine.MoveNext(Command.Ate);
            }
            
        }

        public override void Exit(AnimalWithFsm animal)
        {
        }
    }
}