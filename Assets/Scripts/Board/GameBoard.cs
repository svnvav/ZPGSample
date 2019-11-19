using UnityEngine;

namespace Svnvav.Samples
{
    public class GameBoard : MonoBehaviour
    {
        [SerializeField] private Transform ground = default;

        [SerializeField] private GameTile tilePrefab = default;
        
        [SerializeField] private Texture2D gridTexture = default;
        
        private GameTileContentFactory contentFactory;

        private Vector2Int size;

        private GameTile[,] tiles;
        
        public void Initialize(Vector2Int size)
        {
            this.size = size;
            ground.localScale = new Vector3(size.x, size.y, 1f);

            Vector2 offset = new Vector2(
                (size.x - 1) * 0.5f, (size.y - 1) * 0.5f
            );

            tiles = new GameTile[size.x, size.y];
            

            Clear();
        }
        
        public void Clear () {
            foreach (GameTile tile in tiles) {
                tile.Content = contentFactory.Get(GameTileContentType.Empty);
            }
        }
    }
}