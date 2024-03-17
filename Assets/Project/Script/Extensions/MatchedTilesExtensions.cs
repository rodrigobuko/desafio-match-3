using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gazeus.DesafioMatch3.Extensions
{
    public static class MatchedTilesExtensions 
    {
        // Extension to check if there is any true value
        public static bool ContainsAnyMatch(this List<List<bool>> matchedTiles)
        {
            foreach (var matchedTilesList in matchedTiles)
            {
                foreach (var matchedTile in matchedTilesList)
                {
                    if (matchedTile)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static List<Vector2Int> GetMatchedPositions(this List<List<bool>> matchedTiles)
        {
            List<Vector2Int> matchedPositions = new List<Vector2Int>();
            for (int y = 0; y < matchedTiles.Count; y++)
            {
                for (int x = 0; x < matchedTiles[y].Count; x++)
                {
                    if (matchedTiles[y][x])
                    {
                       matchedPositions.Add(new Vector2Int(x,y));
                    }
                }
            }
            return matchedPositions;
        }

        public static List<List<bool>> GetCopy(this List<List<bool>> matchedTiles)
        {
            List<List<bool>> copy = new List<List<bool>>();
            for (int y = 0; y < matchedTiles.Count; y++)
            {
                copy.Add(new List<bool>());
                for (int x = 0; x < matchedTiles[y].Count; x++)
                {
                    copy[y].Add(matchedTiles[y][x]);
                }
            }
            return copy;
        }
    }
}
