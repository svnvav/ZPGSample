using System;
using UnityEngine;
using UnityEngine.AI;

namespace Svnvav.Samples
{
    public class Goblin : Creature
    {
        [SerializeField] private NavTileAgent navTileAgent;
        public NavTileAgent NavTileAgent => navTileAgent;

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