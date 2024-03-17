using System.Collections;
using System.Collections.Generic;
using Gazeus.DesafioMatch3.Extensions;
using Gazeus.DesafioMatch3.Models;
using UnityEngine;

namespace Gazeus.DesafioMatch3.Core
{
    public interface IBoardIterator {
        void AddBoard(Board board);
        bool IsValidMovement(int fromX, int fromY, int toX, int toY);
        List<BoardSequence> SwapTile(int fromX, int fromY, int toX, int toY);
    }
    public partial class BoardIterator : IBoardIterator
    {
        private Board _board;

        public void AddBoard(Board board)
        {
            _board = board;;
        }

        public bool IsValidMovement(int fromX, int fromY, int toX, int toY)
        {
            List<List<Tile>> newBoardTiles = _board.CopyBoardTiles();

            (newBoardTiles[toY][toX], newBoardTiles[fromY][fromX]) = (newBoardTiles[fromY][fromX], newBoardTiles[toY][toX]);
            List<List<bool>> matchedTiles = newBoardTiles.FindMatches();

            return matchedTiles.ContainsAnyMatch();
        }

        public List<BoardSequence> SwapTile(int fromX, int fromY, int toX, int toY)
        {
            List<List<Tile>> newBoardTiles = _board.GetBoardTiles();

            (newBoardTiles[toY][toX], newBoardTiles[fromY][fromX]) = (newBoardTiles[fromY][fromX], newBoardTiles[toY][toX]);

            List<BoardSequence> boardSequences = new();
            List<List<bool>> matchedTiles = newBoardTiles.FindMatches();
            ApplySpecialMatches(matchedTiles, newBoardTiles);
            

            while (matchedTiles.ContainsAnyMatch())
            {
                //Cleaning the matched tiles
                List<Vector2Int> matchedPosition = ClearBoardTiles(newBoardTiles, matchedTiles);

                // Dropping the tiles
                List<MovedTileInfo> movedTilesList = DropBoardTiles(newBoardTiles, matchedPosition);

                // Filling the board
                List<AddedTileInfo> addedTiles = FillBoardTiles(newBoardTiles);

                BoardSequence sequence = new()
                {
                    MatchedPosition = matchedPosition,
                    MovedTiles = movedTilesList,
                    AddedTiles = addedTiles
                };
                boardSequences.Add(sequence);
                matchedTiles = newBoardTiles.FindMatches();
                ApplySpecialMatches(matchedTiles, newBoardTiles);
            }

            _board.UpdateBoardTiles(newBoardTiles);

            return boardSequences;
        }

        private List<Vector2Int> ClearBoardTiles(List<List<Tile>> boardTiles, List<List<bool>> matchedTiles)
        {
            List<Vector2Int> matchedPosition = new();
            for (int y = 0; y < boardTiles.Count; y++)
            {
                for (int x = 0; x < boardTiles[y].Count; x++)
                {
                    if (matchedTiles[y][x])
                    {
                        matchedPosition.Add(new Vector2Int(x, y));
                        boardTiles[y][x] = new Tile { Id = -1, Type = -1 };
                    }
                }
            }
            return matchedPosition;
        }

        private List<MovedTileInfo> DropBoardTiles(List<List<Tile>> boardTiles, List<Vector2Int> matchedPosition)
        {
            Dictionary<int, MovedTileInfo> movedTiles = new();
            List<MovedTileInfo> movedTilesList = new();
            for (int i = 0; i < matchedPosition.Count; i++)
            {
                int x = matchedPosition[i].x;
                int y = matchedPosition[i].y;
                if (y > 0)
                {
                    for (int j = y; j > 0; j--)
                    {
                        Tile movedTile = boardTiles[j - 1][x];
                        boardTiles[j][x] = movedTile;
                        if (movedTile.Type > -1)
                        {
                            if (movedTiles.ContainsKey(movedTile.Id))
                            {
                                movedTiles[movedTile.Id].To = new Vector2Int(x, j);
                            }
                            else
                            {
                                MovedTileInfo movedTileInfo = new()
                                {
                                    From = new Vector2Int(x, j - 1),
                                    To = new Vector2Int(x, j)
                                };
                                movedTiles.Add(movedTile.Id, movedTileInfo);
                                movedTilesList.Add(movedTileInfo);
                            }
                        }
                    }

                    boardTiles[0][x] = new Tile
                    {
                        Id = -1,
                        Type = -1
                    };
                }
                
            }
            return movedTilesList;
        }

        private List<AddedTileInfo> FillBoardTiles(List<List<Tile>> boardTiles){
            List<AddedTileInfo> addedTiles = new();
            for (int y = boardTiles.Count - 1; y > -1; y--)
            {
                for (int x = boardTiles[y].Count - 1; x > -1; x--)
                {
                    if (boardTiles[y][x].Type == -1)
                    {
                        int tileType = Random.Range(0, _board.GetTileTypes().Count);
                        Tile tile = boardTiles[y][x];
                        tile.Id = _board.TileCount++;
                        tile.Type = _board.GetTileTypes()[tileType];
                        addedTiles.Add(new AddedTileInfo
                        {
                            Position = new Vector2Int(x, y),
                            Type = tile.Type
                        });
                    }
                }
            }
            return addedTiles;
        }
    }
}
