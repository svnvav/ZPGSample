using UnityEngine;
using UnityEngine.Tilemaps;

namespace Svnvav.Samples
{
    public class NavTileAgent : MonoBehaviour
    {
        private Tilemap _tilemap;
        
        private Vector3[] _path;
        private int _pathStep;
        private float _pathPointsTransition = 0f;
        private bool _moving;

        private void OnEnable()
        {
            _tilemap = Game.Instance.Board.Tilemap;
        }
        
        
    }
}