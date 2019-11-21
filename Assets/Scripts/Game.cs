
using System;
using UnityEngine;

namespace Svnvav.Samples
{
    public class Game : MonoBehaviour
    {
        public static Game Instance { get; private set; }
        
        [SerializeField] private GameBoard _board;
        public GameBoard Board => _board;

        private void Awake()
        {
            Instance = this;
        }
    }
}