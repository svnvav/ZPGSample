using UnityEngine;

namespace Svnvav.Samples
{
    public class HealthBehaviour : CreatureBehaviour
    {
        public override BehaviourType BehaviorType => BehaviourType.Health;

        [SerializeField]
        private float _health;

        public override bool GameUpdate(Creature creature)
        {
            return true;
        }

        public override void Recycle()
        {
            CreatureBehaviourPool<HealthBehaviour>.Reclaim(this);
        }
    }
}