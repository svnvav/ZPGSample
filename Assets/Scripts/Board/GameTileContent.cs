using UnityEngine;

namespace Svnvav.Samples
{
    public class GameTileContent : MonoBehaviour
    {
        [SerializeField] private GameTileType _type = default;
        public GameTileType Type => _type;
        
        private GameTileContentFactory originFactory;
        public GameTileContentFactory OriginFactory {
            get => originFactory;
            set {
                Debug.Assert(originFactory == null, "Redefined origin factory!");
                originFactory = value;
            }
        }
        
        public bool BlocksPath =>
            Type == GameTileType.Wall;

        public void Recycle () {
            originFactory.Reclaim(this);
        }
    }
}