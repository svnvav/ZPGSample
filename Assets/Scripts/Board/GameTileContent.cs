using UnityEngine;

namespace Svnvav.Samples
{
    public class GameTileContent : MonoBehaviour
    {
        [SerializeField] private GameTileContentType _type = default;
        public GameTileContentType Type => _type;
        
        private GameTileContentFactory originFactory;
        public GameTileContentFactory OriginFactory {
            get => originFactory;
            set {
                Debug.Assert(originFactory == null, "Redefined origin factory!");
                originFactory = value;
            }
        }
        
        public bool BlocksPath =>
            Type == GameTileContentType.Wall;

        public void Recycle () {
            originFactory.Reclaim(this);
        }
    }
}