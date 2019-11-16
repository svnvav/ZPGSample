using UnityEngine;

namespace Svnvav.Samples
{
    public class GoToFood : StateComponent
    {
        [SerializeField]
        private Plant _plant;
        
        public override void Enter(AnimalWithFsm animal)
        {
            _plant = animal.Target.GetComponent<Plant>();
        }
        
        public override void GameUpdate(AnimalWithFsm animal)
        {
            if (_plant == null || !_plant.IsAlive)
            {
                animal.StateMachine.MoveNext(Command.Lost);
            }

            var positionDif = transform.position - animal.Target.position;
            
            if (positionDif.x * positionDif.x + positionDif.z * positionDif.z < 0.5f)
            {
                animal.StateMachine.MoveNext(Command.ComeToFood);
            }
            
            animal.NavMeshAgent.SetDestination(animal.Target.position);
        }

        public override void Exit(AnimalWithFsm animal)
        {
        }
    }
}