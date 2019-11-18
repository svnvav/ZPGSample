using UnityEngine;
using UnityEngine.AI;

namespace Svnvav.Samples
{
    public class Goblin : Creature
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        public NavMeshAgent NavMeshAgent => _navMeshAgent;

        [SerializeField] private StateComponent _lifeBehaviour;

        public Transform Target { get; set; }

        protected override void GameUpdate()
        {
            _lifeBehaviour.GameUpdate(this);
        }
    }
}