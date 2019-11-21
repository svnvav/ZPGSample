using UnityEngine;

namespace Svnvav.Samples
{
    public class SkyvanSearchTarget : StateComponent
    {
        [SerializeField] private float _searchRadius;
        [SerializeField] private SearchKeyPoint[] _keyPoints;

        private int _currentKeyPointId;
        private Vector3 _destination => _keyPoints[_currentKeyPointId].Position;
        
        private Skyvan _skyvan;
        public override void Enter(Creature creature)
        {
            creature.Target = null;
            _skyvan = (Skyvan) creature;
            _currentKeyPointId = Random.Range(0, _keyPoints.Length);
            _skyvan.NavTileAgent.SetDestination(_destination);
        }

        public override void GameUpdate(Creature creature)
        {
            var positionDif = creature.transform.position - _destination;
            
            if (positionDif.x * positionDif.x + positionDif.y * positionDif.y < 0.01f)
            {
                _currentKeyPointId = Random.Range(0, _keyPoints.Length);//TODO: what if the same point after random
                _skyvan.NavTileAgent.SetDestination(_destination);
            }
            CheckSearchArea(_skyvan);
        }

        public override void Exit(Creature creature)
        {
            
        }
        
        private void CheckSearchArea(Skyvan skyvan)
        {
            var position = skyvan.transform.position;
            var colliders = Physics2D.OverlapCircleAll(
                position, _searchRadius
            );

            foreach (var collider in colliders)
            {
                var target = collider.GetComponent<Goblin>();
                if (target != null && target.IsAlive)
                {
                    skyvan.Target = target.transform;
                    skyvan.StateMachine.MoveNext(Command.TargetFound);
                    break;
                }
                var item = collider.GetComponent<Item>();
                if (item != null)
                {
                    skyvan.Target = item.transform;
                    skyvan.StateMachine.MoveNext(Command.TargetFound);
                    break;
                }
            }
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, _searchRadius);
        }
    }
}