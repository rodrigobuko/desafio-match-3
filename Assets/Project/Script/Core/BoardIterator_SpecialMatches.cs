using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gazeus.DesafioMatch3.Extensions;
using Gazeus.DesafioMatch3.Models;
using log4net;
using UnityEngine;

namespace Gazeus.DesafioMatch3.Core
{
    public partial class BoardIterator
    {
        private List<List<bool>> ApplySpecialMatches(List<List<bool>> matchedTiles, List<List<Tile>> newBoardTiles)
        {
            List<Vector2Int> matchedPositions = matchedTiles.GetMatchedPositions();
            List<List<bool>> newMatchedTiles = matchedTiles.GetCopy();
            foreach(Vector2Int matchedPosition  in matchedPositions){

                // Explosion with L PATTERN
                if(newBoardTiles.HasLPattern(matchedPosition))
                {
                    ApplyClearLPatternMatch(newMatchedTiles, matchedPosition.y, matchedPosition.x);
                }

                // Clear all colors of that match 5 
                if(newBoardTiles.HasColorPattern(matchedPosition))
                {
                    ApplyClearColorMatch(newMatchedTiles, _board[matchedPosition.y][matchedPosition.x].Type);
                    continue;
                }

                // Clear column that match 4 in the same column
                if(newBoardTiles.HasColumnPattern(matchedPosition))
                {
                    ApplyClearColumnMatch(newMatchedTiles, matchedPosition.x);
                }

                // Clear row that match 4 in the same row
                if(newBoardTiles.HasRowPattern(matchedPosition))
                {
                    ApplyClearRowMatch(newMatchedTiles, matchedPosition.y);
                }
            }

            return newMatchedTiles;
        }

        private void ApplyClearRowMatch(List<List<bool>> matchedTiles, int row)
        {
            for (int x = 0; x < matchedTiles[row].Count; x++)
            {
                matchedTiles[row][x] = true;
            }
        }

        private void ApplyClearColumnMatch(List<List<bool>> matchedTiles, int column)
        {
            for (int y = 0; y < matchedTiles.Count; y++)
            {
                matchedTiles[y][column] = true;
            }
        }

        private void ApplyClearColorMatch(List<List<bool>> matchedTiles, int type)
        {
            for (int y = 0; y < matchedTiles.Count; y++){
                for (int x = 0; x < matchedTiles[y].Count; x++)
                {
                    if(_board[x][y].Type == type){
                        matchedTiles[x][y] = true;
                    }
                }
            }
        }

        private void ApplyClearLPatternMatch(List<List<bool>> matchedTiles, int row, int column)
        {
            for (int y = Math.Max(0, row - 2); y < matchedTiles.Count; y++){
                for (int x = Math.Max(0, column - 2); x < matchedTiles[y].Count; x++)
                {
                    int distance = Math.Abs(y - row) + Math.Abs(x - column);
                    if (distance <= 2)
                    {
                        matchedTiles[y][x] = true;
                    }
                }
            }
        }
    }
}
