using UnityEngine;

namespace Svnvav.Samples
{
    public class Escape : StateComponent
    {
        [SerializeField] private Goblin _enemy;
        [SerializeField] private SearchKeyPoint _escapePoint;
        [SerializeField] private float _escapeDistance;
        [SerializeField] private float _speedMultiplier;
        
        private Skyvan _skyvan;
        public override void Enter(Creature creature)
        {
            _skyvan = (Skyvan) creature;
            _enemy = _skyvan.Target.GetComponent<Goblin>();
            _skyvan.NavTileAgent.Speed *= _speedMultiplier;
            _skyvan.NavTileAgent.SetDestination(_escapePoint.Position);
        }

        public override void GameUpdate(Creature creature)
        {
            if (_enemy == null)
            {
                creature.StateMachine.MoveNext(Command.TargetLost);
            }

            var positionDif = transform.position - _enemy.transform.position;
            
            if (positionDif.x * positionDif.x + positionDif.y * positionDif.y > _escapeDistance * _escapeDistance)
            {
                creature.StateMachine.MoveNext(Command.Escaped);
            }
        }

        public override void Exit(Creature creature)
        {
            _skyvan.NavTileAgent.Speed /= _speedMultiplier;
        }
    }
}