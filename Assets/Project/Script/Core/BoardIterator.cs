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
    public class BoardIterator : IBoardIterator
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

            return matchedTiles.ContainsAnyTrue();
        }

        public List<BoardSequence> SwapTile(int fromX, int fromY, int toX, int toY)
        {
            List<List<Tile>> newBoardTiles = _board.GetBoardTiles();

            (newBoardTiles[toY][toX], newBoardTiles[fromY][fromX]) = (newBoardTiles[fromY][fromX], newBoardTiles[toY][toX]);

            List<BoardSequence> boardSequences = new();
            List<List<bool>> matchedTiles = newBoardTiles.FindMatches();

            while (matchedTiles.ContainsAnyTrue())
            {
                //Cleaning the matched tiles
                List<Vector2Int> matchedPosition = ClearBoard(newBoardTiles, matchedTiles);

                // Dropping the tiles
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
                            Tile movedTile = newBoardTiles[j - 1][x];
                            newBoardTiles[j][x] = movedTile;
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

                        newBoardTiles[0][x] = new Tile
                        {
                            Id = -1,
                            Type = -1
                        };
                    }
                }

                // Filling the board
                List<AddedTileInfo> addedTiles = new();
                for (int y = newBoardTiles.Count - 1; y > -1; y--)
                {
                    for (int x = newBoardTiles[y].Count - 1; x > -1; x--)
                    {
                        if (newBoardTiles[y][x].Type == -1)
                        {
                            int tileType = Random.Range(0, _board.GetTileTypes().Count);
                            Tile tile = newBoardTiles[y][x];
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

                BoardSequence sequence = new()
                {
                    MatchedPosition = matchedPosition,
                    MovedTiles = movedTilesList,
                    AddedTiles = addedTiles
                };
                boardSequences.Add(sequence);
                matchedTiles = newBoardTiles.FindMatches();
            }

            _board.UpdateBoardTiles(newBoardTiles);

            return boardSequences;
        }

        private List<Vector2Int> ClearBoard(List<List<Tile>> boardTiles, List<List<bool>> matchedTiles){
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
    }
}
