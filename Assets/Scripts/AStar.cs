
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Svnvav.Samples
{
    public static class AStar
    {
        private static Vector2Int[] neighboursPattern = new []
        {
            new Vector2Int(0, -1), 
            new Vector2Int(-1, 0), 
            new Vector2Int(1, 0), 
            new Vector2Int(0, 1), 
        };
        
        private struct Node
        {
            public int F => g + h;
            public int g;
            public int h;
            public Vector3Int pos;
            public Vector3Int step;
        }
        
        public static Vector2Int[] FindPath(Vector3Int start, Vector3Int finish, Tilemap tilemap)
        {
            var closed = new HashSet<Node>();
            var open = new List<Node>()
            {
                new Node()
                {
                    g = 0,
                    h = CalculateH(start, finish),
                    pos = finish,
                    step = Vector3Int.zero
                }
            };

            while (open.Count > 0)
            {
                open.Sort((x1, x2) => x1.F > x2.F ? -1 : 1);
                var current = open[open.Count - 1];
                open.RemoveAt(open.Count - 1);
                closed.Add(current);
                if (current.pos == start)
                {
                    return GetPath(finish, start, closed);
                }
                ExamineNeighbours(current, start, tilemap, closed, open, neighboursPattern);
            }

            return null;
        }
        
        private static void ExamineNeighbours(
            Node current,
            Vector3Int goal,
            Tilemap tilemap,
            HashSet<Node> closed,
            List<Node> open,
            Vector2Int[] pattern)
        {
            foreach (var shift in pattern)
            {
                var pos = new Vector3Int(current.pos.x + shift.x, current.pos.y + shift.y, 0);
                var tile = tilemap.GetTile<CustomTile>(pos);
                if(closed.Any(n => n.pos == pos) || tile == null || tile.type != GameTileType.Empty)
                    continue;

                var neighbour = new Node
                {
                    g = current.g + CalculateG(pos, current.pos),
                    h = CalculateH(pos, goal),
                    pos = pos,
                    step = new Vector3Int(-shift.x, -shift.y,0)
                };
                open.Add(neighbour);
            }
        }

        private static Vector2Int[] GetPath(Vector3Int start, Vector3Int finish, HashSet<Node> closed)
        {
            var path = new List<Vector3Int>();

            var current = closed.First(n => n.pos == finish);
            path.Add(finish);
            var loopBound = closed.Count;
            var loopCheck = 0;
            while (current.pos != start)
            {
                loopCheck++;
                if(loopCheck > loopBound)
                    throw new Exception("infinite loop");
                
                current = closed.First(n => n.pos == current.pos + current.step);
                path.Add(current.pos);
            }
            
            return path.Select(p => new Vector2Int(p.x, p.y)).ToArray();
        }
        
        private static int CalculateH(Vector3Int point, Vector3Int goal)
        {
            return 10 * (Math.Abs(point.x - goal.x) + Math.Abs(point.y - goal.y));
        }
        
        private static int CalculateG(Vector3Int point, Vector3Int prev)
        {
            var diff = Mathf.Abs(point.x - prev.x) + Mathf.Abs(point.y - prev.y);
            return diff % 2 == 0 ? 14 : 10;
        }
    }
}