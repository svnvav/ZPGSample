using UnityEngine;

namespace Svnvav.Samples
{
    public class GameTile : MonoBehaviour
    {
        private GameTileContent _content;
        
        public GameTileContent Content {
            get => _content;
            set {
                Debug.Assert(value != null, "Null assigned to content!");
                if (_content != null) {
                    _content.Recycle();
                }
                _content = value;
                _content.transform.localPosition = transform.localPosition;
            }
        }
    }
}