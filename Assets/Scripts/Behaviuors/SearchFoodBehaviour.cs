using UnityEngine;

namespace Svnvav.Samples
{
    public class SearchFoodBehaviour : CreatureBehaviour
    {
        public override BehaviourType BehaviorType => BehaviourType.Health;

        [SerializeField]
        private float _searchRadius;

        public override bool GameUpdate(Creature creature)
        {
            return true;
        }

        public override void Recycle()
        {
            CreatureBehaviourPool<SearchFoodBehaviour>.Reclaim(this);
        }
    }
}