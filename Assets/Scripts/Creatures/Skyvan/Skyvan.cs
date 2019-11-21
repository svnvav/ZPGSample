
using UnityEngine;
using UnityEngine.AI;

namespace Svnvav.Samples
{
    public class Skyvan : Creature
    {
        [SerializeField] private NavTileAgent _navTileAgent;
        public NavTileAgent NavTileAgent => _navTileAgent;

        [SerializeField] private LifeBehaviour _lifeBehaviour;

        public override bool IsAlive => _lifeBehaviour.Health > 0f;

        private void Start()
        {
            Initialize();
        }

        public override void Initialize()
        {
            NavTileAgent.Initialize();
            base.Initialize();
        }
        
        protected override void GameUpdate()
        {
            _lifeBehaviour.GameUpdate(this);
        }
    }
}