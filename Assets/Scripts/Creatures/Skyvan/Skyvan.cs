
using UnityEngine;
using UnityEngine.AI;

namespace Svnvav.Samples
{
    public class Skyvan : Creature
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        public NavMeshAgent NavMeshAgent => _navMeshAgent;

        [SerializeField] private LifeBehaviour _lifeBehaviour;
        
        public Transform Target { get; set; }
        
        public bool IsAlive => _lifeBehaviour.Health > 0f;

        protected override void GameUpdate()
        {
            _lifeBehaviour.GameUpdate(this);
        }

        
    }
}