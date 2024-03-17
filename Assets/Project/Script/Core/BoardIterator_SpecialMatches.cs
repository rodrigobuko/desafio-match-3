using System;
using System.Collections;
using System.Collections.Generic;
using Gazeus.DesafioMatch3.Extensions;
using Gazeus.DesafioMatch3.Models;
using UnityEngine;

namespace Gazeus.DesafioMatch3.Core
{
    public partial class BoardIterator
    {
        private void ApplySpecialMatches(List<List<bool>> matchedTiles)
        {
            List<Vector2Int> matchedPositions = matchedTiles.GetMatchedPositions();
            List<MatchInfo> matchesInfos = GetMatchesInfo(matchedPositions);
            foreach(MatchInfo matchInfo in matchesInfos){

                // Explosion with L PATTERN
                if(matchInfo.CheckLPattern())
                {
                    ApplyClearLPatternMatch(matchedTiles, matchInfo.MatchRow, matchInfo.MatchColumn);
                    continue;
                }

                // Clear all colors of that match 
                if(matchInfo.CheckColorPattern())
                {
                    ApplyClearColorMatch(matchedTiles, matchInfo.TilesMatchedType);
                    continue;
                }
                
                // Clear entire row
                if(matchInfo.CheckRowPattern())
                {
                    ApplyClearRowMatch(matchedTiles, matchInfo.MatchRow);
                    continue;
                }
                
                // Clear entire column
                if(matchInfo.CheckColumnPattern()){
                    ApplyClearColumnMatch(matchedTiles, matchInfo.MatchColumn);
                    continue;
                }
            }
        }
        private List<MatchInfo> GetMatchesInfo(List<Vector2Int> matchedPositions)
        {
            List<MatchInfo> matchesInfo = new List<MatchInfo>();
            Dictionary<int, int> rowCounts = new Dictionary<int, int>();
            Dictionary<int, int> columnCounts = new Dictionary<int, int>();

            //Populate rowCounts and columnCounts
            foreach (Vector2Int pos in matchedPositions)
            {
                if (!rowCounts.ContainsKey(pos.y))
                {
                    rowCounts[pos.y] = 1;
                }
                else
                {
                    rowCounts[pos.y]++;
                }

                if (!columnCounts.ContainsKey(pos.x))
                {
                    columnCounts[pos.x] = 1;
                }
                else
                {
                    columnCounts[pos.x]++;
                }
            }

            // FIND L PATTERN 
            foreach (Vector2Int pos in matchedPositions){
                if(columnCounts[pos.x] == 3 && rowCounts[pos.y] == 3){
                    matchesInfo.Add(new MatchInfo
                    {
                        MatchRow = pos.y,
                        MatchColumn = pos.x,
                        TotalTilesMatched = columnCounts[pos.x] + rowCounts[pos.y],
                        TilesMatchedType = _board[pos.y][pos.x].Type
                    });
                    columnCounts[pos.x] = 0;
                    rowCounts[pos.y] = 0;
                }
            }

            // Find other Matches
            foreach (Vector2Int pos in matchedPositions) 
            {
                if(columnCounts[pos.x] >= 3)
                {
                    matchesInfo.Add(new MatchInfo
                    {
                        MatchRow = -1,
                        MatchColumn = pos.x,
                        TotalTilesMatched = columnCounts[pos.x],
                        TilesMatchedType = _board[pos.y][pos.x].Type
                    });
                    columnCounts[pos.x] = 0;
                }
                else if(rowCounts[pos.y] >= 3){
                    matchesInfo.Add(new MatchInfo
                    {
                        MatchRow = pos.y,
                        MatchColumn =  -1,
                        TotalTilesMatched = rowCounts[pos.y],
                        TilesMatchedType = _board[pos.y][pos.x].Type
                    });
                    rowCounts[pos.y] = 0;
                }
            }

            return matchesInfo;
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
            for (int y = Math.Max(0, row - 2); y < Math.Min(row + 2, matchedTiles.Count); y++){
                for (int x = Math.Max(0, column - 2); x < Math.Min(column + 2, matchedTiles[y].Count); x++)
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
