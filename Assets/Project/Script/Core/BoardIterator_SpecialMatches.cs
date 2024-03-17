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
        private void ApplySpecialMatches(List<List<bool>> matchedTiles, List<List<Tile>> newBoardTiles)
        {
            // copy matchesTiles 
            List<Vector2Int> matchedPositions = matchedTiles.GetMatchedPositions();
            foreach(Vector2Int matchedPosition  in matchedPositions){

                // Explosion with L PATTERN
                if(newBoardTiles.HasLPattern(matchedPosition))
                {
                    ApplyClearLPatternMatch(matchedTiles, matchedPosition.y, matchedPosition.x);
                }

                // Clear all colors of that match 5 
                if(newBoardTiles.HasColorPattern(matchedPosition))
                {
                    ApplyClearColorMatch(matchedTiles, _board[matchedPosition.y][matchedPosition.x].Type);
                    continue;
                }

                // Clear column that match 4 in the same column
                if(newBoardTiles.HasColumnPattern(matchedPosition.x))
                {
                    ApplyClearColumnMatch(matchedTiles, matchedPosition.x);
                }

                // Clear row that match 4 in the same row
                if(newBoardTiles.HasRowPattern(matchedPosition.y))
                {
                    ApplyClearRowMatch(matchedTiles, matchedPosition.y);
                }
            }
        }
        private List<MatchInfo> GetMatchesInfo(List<List<Vector2Int>> allMatches)
        {
            // OUTRA TENTATIVA 
            List<MatchInfo> matchesInfo = new List<MatchInfo>();
            foreach(List<Vector2Int> matchPositions in allMatches){
                Vector2Int reference = matchPositions[0];
                int matchesInColumn = matchPositions.Where(pos => pos.x == reference.x).ToList().Count;
                int matchesInRow = matchPositions.Where(pos => pos.y == reference.y).ToList().Count;
                Debug.Log("ALL MATCHES");
                foreach(Vector2Int vec in matchPositions){
                     Debug.Log(vec);
                }

                Debug.Log("MATCHES INFOS");
                Debug.Log($"matchesRows: {matchesInRow} and matches in column {matchesInColumn}");
                int numberOfMatchesInRow = matchesInRow >= 3 ? matchesInRow : 0;
                int numberOfMatchesInColumn = matchesInColumn >= 3 ? matchesInColumn : 0;
                Debug.Log($"row : {numberOfMatchesInRow}, col : {numberOfMatchesInColumn}, total: {matchesInColumn + matchesInRow}, type : {_board[reference.y][reference.x].Type}");
                matchesInfo.Add(new MatchInfo
                {
                    MatchRow = numberOfMatchesInRow > 0 ? reference.y : - 1,
                    MatchColumn = numberOfMatchesInColumn > 0 ? reference.x : - 1,
                    TotalTilesMatched = numberOfMatchesInColumn + numberOfMatchesInRow,
                    TilesMatchedType = _board[reference.y][reference.x].Type
                });

            }
            //List<MatchInfo> matchesInfo = new List<MatchInfo>();
            // Dictionary<int, int> rowCounts = new Dictionary<int, int>();
            // Dictionary<int, int> columnCounts = new Dictionary<int, int>();

            // //Populate rowCounts and columnCounts
            // foreach (Vector2Int pos in matchedPositions)
            // {
            //     if (!rowCounts.ContainsKey(pos.y))
            //     {
            //         rowCounts[pos.y] = 1;
            //     }
            //     else
            //     {
            //         rowCounts[pos.y]++;
            //     }

            //     if (!columnCounts.ContainsKey(pos.x))
            //     {
            //         columnCounts[pos.x] = 1;
            //     }
            //     else
            //     {
            //         columnCounts[pos.x]++;
            //     }
            // }


            // List<MatchInfo> matchesInfo = new List<MatchInfo>();
            // Dictionary<int, bool> rowIsMatched= new Dictionary<int, bool>();
            // Dictionary<int, bool> columnCounts = new Dictionary<int, bool>();
            // // LOGICA NOVAAAAAA
            // foreach (Vector2Int position in matchedPositions)
            // {
            //     int column = position.x;
            //     int row = position.y;
            //     int type = _board[row][column].Type;
            //     List<Vector2Int> mathchesInColum = matchedPositions
            //     .Where(pos => (pos.x == column) && _board[pos.y][pos.x].Type == type).ToList();
            //     // Debug.Log($"Matches in column for: {position}");
            //     // foreach(var a in mathchesInColum){
            //     //     Debug.Log(a);
            //     // }
            //     List<Vector2Int> mathchesInRow = matchedPositions
            //     .Where(pos => (pos.y == row) && _board[pos.y][pos.x].Type == type).ToList();
            //     // Debug.Log($"Matches in row for: {position}");
            //     // foreach(var a in mathchesInRow){
            //     //     Debug.Log(a);
            //     // }
            //     int numberOfMatchesInRow = mathchesInRow.Count >= 3 ? mathchesInRow.Count : 0;
            //     int numberOfMatchesInColumn = mathchesInColum.Count >= 3 ? mathchesInColum.Count : 0;

            //     if(numberOfMatchesInRow > 0 || numberOfMatchesInColumn > 0)
            //     {
            //         matchesInfo.Add(new MatchInfo
            //         {
            //             MatchRow = numberOfMatchesInRow > 0 ? row : -1,
            //             MatchColumn = numberOfMatchesInColumn > 0 ? column : -1,
            //             TotalTilesMatched = numberOfMatchesInColumn + numberOfMatchesInRow,
            //             TilesMatchedType = type
            //         });
            //     }
            // }
           
            // // FIND L PATTERN 
            // foreach (Vector2Int pos in matchedPositions){
            //     if(columnCounts[pos.x] == 3 && rowCounts[pos.y] == 3){
            //         matchesInfo.Add(new MatchInfo
            //         {
            //             MatchRow = pos.y,
            //             MatchColumn = pos.x,
            //             TotalTilesMatched = columnCounts[pos.x] + rowCounts[pos.y],
            //             TilesMatchedType = _board[pos.y][pos.x].Type
            //         });
            //         columnCounts[pos.x] = 0;
            //         rowCounts[pos.y] = 0;
            //     }
            // }

            // // Find other Matches
            // foreach (Vector2Int pos in matchedPositions) 
            // {
            //     if(columnCounts[pos.x] >= 3)
            //     {
            //         matchesInfo.Add(new MatchInfo
            //         {
            //             MatchRow = -1,
            //             MatchColumn = pos.x,
            //             TotalTilesMatched = columnCounts[pos.x],
            //             TilesMatchedType = _board[pos.y][pos.x].Type
            //         });
            //         columnCounts[pos.x] = 0;
            //     }
            //     else if(rowCounts[pos.y] >= 3){
            //         matchesInfo.Add(new MatchInfo
            //         {
            //             MatchRow = pos.y,
            //             MatchColumn =  -1,
            //             TotalTilesMatched = rowCounts[pos.y],
            //             TilesMatchedType = _board[pos.y][pos.x].Type
            //         });
            //         rowCounts[pos.y] = 0;
            //     }
            // }

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
