using UnityEngine;
using UnityEngine.Tilemaps;

namespace Svnvav.Samples
{
    public class GameBoard : MonoBehaviour
    {
        [SerializeField] private Tilemap _tilemap;

        public Tilemap Tilemap => _tilemap;
    }
}