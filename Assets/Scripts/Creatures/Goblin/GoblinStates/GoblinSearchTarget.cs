
using UnityEngine;
using Random = UnityEngine.Random;

namespace Svnvav.Samples
{
    public class GoblinSearchTarget : StateComponent
    {
        [SerializeField] private float _searchRadius;
        [SerializeField] private SearchKeyPoint[] _keyPoints;

        private int _currentKeyPointId;
        private Vector3 _destination => _keyPoints[_currentKeyPointId].Position;

        private Goblin _goblin;

        public override void Enter(Creature creature)
        {
            creature.Target = null;
            _goblin = (Goblin) creature;
            _currentKeyPointId = Random.Range(0, _keyPoints.Length);
            _goblin.NavTileAgent.SetDestination(_destination);
            creature.Animator.Play("Walk");
        }
        
        public override void GameUpdate(Creature goblin)
        {
            CheckSearchArea(_goblin);
            
            var positionDif = goblin.transform.position - _destination;
            
            if (positionDif.x * positionDif.x + positionDif.y * positionDif.y < 0.01f)
            {
                _currentKeyPointId = Random.Range(0, _keyPoints.Length);//TODO: what if the same point after random
                _goblin.NavTileAgent.SetDestination(_destination);
            }
        }
        
        private void CheckSearchArea(Goblin goblin)
        {
            var position = goblin.transform.position;
            var colliders = Physics2D.OverlapCircleAll(
                position, _searchRadius
            );

            foreach (var collider in colliders)
            {
                var enemy = collider.GetComponent<Skyvan>();
                if (enemy != null && enemy.IsAlive)
                {
                    goblin.Target = enemy.transform;
                    goblin.StateMachine.MoveNext(Command.TargetFound);
                    break;
                }
                
                var item = collider.GetComponent<Item>();
                if (item != null)
                {
                    goblin.Target = item.transform;
                    goblin.StateMachine.MoveNext(Command.TargetFound);
                    break;
                }
                
                var plant = collider.GetComponent<Plant>();
                if (plant != null && plant.IsAlive)
                {
                    goblin.Target = plant.transform;
                    goblin.StateMachine.MoveNext(Command.TargetFound);
                    break;
                }
            }
        }

        public override void Exit(Creature goblin)
        {
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _searchRadius);
        }
    }
}