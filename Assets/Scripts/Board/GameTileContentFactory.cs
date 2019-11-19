using UnityEngine;
using UnityEngine.SceneManagement;

namespace Svnvav.Samples
{
    [CreateAssetMenu]
    public class GameTileContentFactory : GameObjectFactory
    {
        [SerializeField] private GameTileContent emptyPrefab = default;
        [SerializeField] private GameTileContent wallPrefab = default;

        public GameTileContent Get(GameTileContentType type)
        {
            switch (type)
            {
                case GameTileContentType.Empty: return Get(emptyPrefab);
                case GameTileContentType.Wall: return Get(wallPrefab);
            }

            Debug.Assert(false, "Unsupported non-tower type: " + type);
            return null;
        }

        private T Get<T>(T prefab) where T : GameTileContent
        {
            T instance = CreateGameObjectInstance(prefab);
            instance.OriginFactory = this;
            return instance;
        }
        
        public void Reclaim(GameTileContent content)
        {
            Debug.Assert(content.OriginFactory == this, "Wrong factory reclaimed!");
            Destroy(content.gameObject);
        }
    }
}