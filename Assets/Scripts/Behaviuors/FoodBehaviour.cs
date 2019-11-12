using UnityEngine;

namespace Svnvav.Samples
{
    public class FoodBehaviour : CreatureBehaviour
    {
        public override BehaviourType BehaviorType => BehaviourType.Health;

        [SerializeField]
        private float _value;

        public override bool GameUpdate(Creature creature)
        {
            return true;
        }

        public override void Recycle()
        {
            CreatureBehaviourPool<FoodBehaviour>.Reclaim(this);
        }
    }
}