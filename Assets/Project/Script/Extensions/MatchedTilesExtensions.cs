using System.Collections.Generic;
using UnityEngine;

namespace Gazeus.DesafioMatch3.Extensions
{
    public static class MatchedTilesExtensions 
    {
        /// <summary>
        /// Check if there is any match (true value) in this matchedTiles
        /// /// </summary>
        /// <returns>true if there are any match (true value)</returns>
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

        /// <summary>
        /// Get all matched positions.
        /// /// </summary>
        /// <returns>List of positions where is a match </returns>
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

        /// <summary>
        /// Get a exact copy of this matchedTiles
        /// /// </summary>
        /// <returns>The matchedTiles copy </returns>
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
