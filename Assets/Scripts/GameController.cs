
using System;
using UnityEngine;

namespace Svnvav.Samples
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }
        
        [SerializeField] private GameBoard _board;
        public GameBoard Board => _board;

        private void Awake()
        {
            Instance = this;
        }
    }
}