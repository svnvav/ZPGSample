using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.PlayerLoop;

namespace Svnvav.Samples
{
    public class AnimalWithFsm : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        public NavMeshAgent NavMeshAgent => _navMeshAgent;

        [SerializeField] private AnimalStateMachine _stateMachine;

        public AnimalStateMachine StateMachine => _stateMachine;

        [SerializeField] private StateComponent _lifeBehaviour;

        private List<Effect> _effects;
        
        public Transform Target { get; set; }

        private void Awake()
        {
            StateMachine.Initialize(this);
        }

        private void Update()
        {
            _stateMachine.GameUpdate(this);
            _lifeBehaviour.GameUpdate(this);
        }
    }
}