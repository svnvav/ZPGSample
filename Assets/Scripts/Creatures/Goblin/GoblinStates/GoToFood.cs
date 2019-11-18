using UnityEngine;

namespace Svnvav.Samples
{
    public class GoToFood : StateComponent
    {
        [SerializeField]
        private Plant _plant;
        
        private Goblin _goblin;
        
        public override void Enter(Creature goblin)
        {
            _goblin = (Goblin) goblin;
            _plant = _goblin.Target.GetComponent<Plant>();
        }
        
        public override void GameUpdate(Creature goblin)
        {
            CheckNearCreatures(_goblin);
            if (_plant == null || !_plant.IsAlive)
            {
                goblin.StateMachine.MoveNext(Command.Lost);
            }

            var positionDif = transform.position - _goblin.Target.position;
            
            if (positionDif.x * positionDif.x + positionDif.z * positionDif.z < 0.5f)
            {
                goblin.StateMachine.MoveNext(Command.FoodReached);
            }
            
            _goblin.NavMeshAgent.SetDestination(_goblin.Target.position);
        }
        
        private void CheckNearCreatures(Goblin goblin)
        {
            var position = goblin.transform.position;
            var colliders = Physics.OverlapSphere(
                position, 15
            );

            foreach (var collider in colliders)
            {
                var enemy = collider.GetComponent<Skyvan>();
                if (enemy != null && enemy.IsAlive)
                {
                    goblin.Target = enemy.transform;
                    goblin.StateMachine.MoveNext(Command.EnemyFound);
                    break;
                }
            }
        }

        public override void Exit(Creature goblin)
        {
        }
    }
}