using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Gazeus.DesafioMatch3.Core;
using Gazeus.DesafioMatch3.Models;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameServiceTests
{
    int _defaultBoardWidth;
    int _defaultBoardHeight;

    IBoardIterator _boardIterator;
    GameService _gameService;

    [SetUp]
    public void SetUp(){
        _defaultBoardWidth = 10;
        _defaultBoardHeight = 10;
        _boardIterator = Substitute.For<IBoardIterator>();
        _gameService = new GameService(_boardIterator);
    }

    // A Test to check if the board created has no match
    [Test]
    public void StartGameTest()
    {
        Board board = _gameService.StartGame(_defaultBoardWidth, _defaultBoardHeight);

        List<List<Tile>> boardTiles = board.GetBoardTiles();
        Assert.NotNull(boardTiles);
        Assert.AreEqual(_defaultBoardWidth, boardTiles[0].Count);
        Assert.AreEqual(_defaultBoardHeight, boardTiles.Count);
    }

    [Test]
    public void IsValidMovementTest()
    {
        int fromX = 0;
        int fromY = 0;
        int toX =  1;
        int toY = 0;
        bool expected = true;
        _boardIterator.IsValidMovement(fromX, fromY, toX, toY).Returns(expected);

        _gameService.StartGame(_defaultBoardWidth, _defaultBoardHeight);
        bool actual = _gameService.IsValidMovement(fromX, fromY, toX, toY);

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void IsNotValidMovementTest()
    {
        int fromX = 0;
        int fromY = 0;
        int toX =  1;
        int toY = 1;
        bool expected = false;
        _boardIterator.IsValidMovement(fromX, fromY, toX, toY).Returns(expected);

        _gameService.StartGame(_defaultBoardWidth, _defaultBoardHeight);
        bool actual = _gameService.IsValidMovement(fromX, fromY, toX, toY);

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void SwapTileWithoutComboTest()
    {
        int fromX = 0;
        int fromY = 0;
        int toX =  1;
        int toY = 0;
        List<BoardSequence> expectedBoardSequence = new List<BoardSequence>{
            new BoardSequence{
                MatchedPosition = new List<Vector2Int>{new Vector2Int(0,0), new Vector2Int(1,0), new Vector2Int(2,0), new Vector2Int(3,0)},
                MovedTiles = new List<MovedTileInfo>{},
                AddedTiles = new List<AddedTileInfo>{
                    new AddedTileInfo{
                        Position = new Vector2Int(0,0),
                        Type = 2,
                    },
                    new AddedTileInfo{
                        Position = new Vector2Int(1,0),
                        Type = 5,
                    },
                    new AddedTileInfo{
                        Position = new Vector2Int(2,0),
                        Type = 8,
                    },
                    new AddedTileInfo{
                        Position = new Vector2Int(3,0),
                        Type = 3,
                    },
                }
            },
        };
        _boardIterator.SwapTile(fromX, fromY, toX, toY).Returns(expectedBoardSequence);
        List<int> expectedScoreSequence = new List<int>{4};

        _gameService.StartGame(_defaultBoardWidth, _defaultBoardHeight);
        GameSequence actual = _gameService.SwapTile(fromX, fromY, toX, toY);

        Assert.AreEqual(expectedScoreSequence, actual.ScoreSequence);
        Assert.AreEqual(expectedBoardSequence, actual.BoardSequence);
    }

    [Test]
    public void SwapTileWithComboTest()
    {
        int fromX = 0;
        int fromY = 0;
        int toX =  1;
        int toY = 0;
        List<BoardSequence> expectedBoardSequence = new List<BoardSequence>{
            new BoardSequence{
                MatchedPosition = new List<Vector2Int>{new Vector2Int(0,0), new Vector2Int(1,0), new Vector2Int(2,0), new Vector2Int(3,0)},
                MovedTiles = new List<MovedTileInfo>{},
                AddedTiles = new List<AddedTileInfo>{
                    new AddedTileInfo{
                        Position = new Vector2Int(0,0),
                        Type = 2,
                    },
                    new AddedTileInfo{
                        Position = new Vector2Int(1,0),
                        Type = 5,
                    },
                    new AddedTileInfo{
                        Position = new Vector2Int(2,0),
                        Type = 8,
                    },
                    new AddedTileInfo{
                        Position = new Vector2Int(3,0),
                        Type = 3,
                    },
                }
            },
            new BoardSequence{
                MatchedPosition = new List<Vector2Int>{new Vector2Int(4,0), new Vector2Int(5,0), new Vector2Int(6,0)},
                MovedTiles = new List<MovedTileInfo>{},
                AddedTiles = new List<AddedTileInfo>{
                    new AddedTileInfo{
                        Position = new Vector2Int(4,0),
                        Type = 2,
                    },
                    new AddedTileInfo{
                        Position = new Vector2Int(5,0),
                        Type = 5,
                    },
                    new AddedTileInfo{
                        Position = new Vector2Int(6,0),
                        Type = 8,
                    },
                }
            }
        };
        _boardIterator.SwapTile(fromX, fromY, toX, toY).Returns(expectedBoardSequence);
        List<int> expectedScoreSequence = new List<int>{4 , 10};

        _gameService.StartGame(_defaultBoardWidth, _defaultBoardHeight);
        GameSequence actual = _gameService.SwapTile(fromX, fromY, toX, toY);

        Assert.AreEqual(expectedScoreSequence, actual.ScoreSequence);
        Assert.AreEqual(expectedBoardSequence, actual.BoardSequence);
    }
}
