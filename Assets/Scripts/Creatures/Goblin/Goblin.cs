using UnityEngine;
using UnityEngine.AI;

namespace Svnvav.Samples
{
    public class Goblin : Creature
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        public NavMeshAgent NavMeshAgent => _navMeshAgent;

        [SerializeField] private LifeBehaviour _lifeBehaviour;

        public override bool IsAlive => _lifeBehaviour.Health > 0f;

        protected override void GameUpdate()
        {
            _lifeBehaviour.GameUpdate(this);
        }
    }
}