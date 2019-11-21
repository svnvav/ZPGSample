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
        [SerializeField] private Transform _model;
            
        private Vector3 _prevPosition;

        public override bool IsAlive => _lifeBehaviour.Health > 0f;

        private void Start()
        {
            Initialize();
            _prevPosition = transform.position;
        }

        public override void Initialize()
        {
            NavTileAgent.Initialize();
            NavTileAgent.OnNextTileStep += DetermineModelForwardDirection;
            base.Initialize();
        }

        protected override void GameUpdate()
        {
            _lifeBehaviour.GameUpdate(this);
            _prevPosition = transform.position;
        }

        private void DetermineModelForwardDirection()
        {
            if(NavTileAgent.MoveDirection.x > 0.1f)
                _model.localScale = new Vector3(1,1,1);
            else if(NavTileAgent.MoveDirection.x < -0.1f)
                _model.localScale = new Vector3(-1,1,1);
        }

        private void OnDestroy()
        {
            NavTileAgent.OnNextTileStep -= DetermineModelForwardDirection;
        }
    }
}