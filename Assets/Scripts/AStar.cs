
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Svnvav.Samples
{
    public static class AStar
    {
        private struct Node
        {
            public float f;
            public Vector2Int pos;
            public List<Vector2Int> path;
        }
        
        public static List<Vector2Int> FindPath(Vector2Int start, Vector2Int finish, Tilemap tilemap)
        {
            var closed = new HashSet<Node>();
            var open = new List<Node>()
            {
                new Node()
                {
                    f = F(start, finish),
                    pos = start,
                    path = new List<Vector2Int>()
                    {
                        start
                    }
                }
            };

            while (open.Count > 0)
            {
                var x = open[0];
                open.RemoveAt(0);
            }

            return closed.Select(node => node.pos).ToList();
        }

        private static float F(Vector2Int point, Vector2Int finish)
        {
            return Vector2.Distance(point, finish);
        }
    }
}