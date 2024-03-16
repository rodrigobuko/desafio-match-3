using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Gazeus.DesafioMatch3.Models;
using UnityEngine;

namespace Gazeus.DesafioMatch3.Extensions
{
    public static class BoardExtensions 
    {
        public static List<List<bool>> FindMatches( this List<List<Tile>> boardTiles)
        {
            List<List<bool>> matchedTiles = new();
            for (int y = 0; y < boardTiles.Count; y++)
            {
                matchedTiles.Add(new List<bool>(boardTiles[y].Count));
                for (int x = 0; x < boardTiles[y].Count; x++)
                {
                    matchedTiles[y].Add(false);
                }
            }

            for (int y = 0; y < boardTiles.Count; y++)
            {
                for (int x = 0; x < boardTiles[y].Count; x++)
                {
                    if (x > 1 &&
                        boardTiles[y][x].Type == boardTiles[y][x - 1].Type &&
                        boardTiles[y][x - 1].Type == boardTiles[y][x - 2].Type)
                    {
                        matchedTiles[y][x] = true;
                        matchedTiles[y][x - 1] = true;
                        matchedTiles[y][x - 2] = true;
                    }

                    if (y > 1 &&
                        boardTiles[y][x].Type == boardTiles[y - 1][x].Type &&
                        boardTiles[y - 1][x].Type == boardTiles[y - 2][x].Type)
                    {
                        matchedTiles[y][x] = true;
                        matchedTiles[y - 1][x] = true;
                        matchedTiles[y - 2][x] = true;
                    }
                }
            }
            
            return matchedTiles;
        }
    }
}
