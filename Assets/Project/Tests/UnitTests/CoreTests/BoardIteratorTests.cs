using System.Collections.Generic;
using Gazeus.DesafioMatch3.Core;
using Gazeus.DesafioMatch3.Models;
using NUnit.Framework;
using UnityEngine;

public class BoardIteratorTests
{
    int _defaultBoardWidth;
    int _defaultBoardHeight;
    BoardIterator _boardIterator;

    [SetUp]
    public void SetUp(){
        _defaultBoardWidth = 5;
        _defaultBoardHeight = 5;

        _boardIterator = new BoardIterator();
    }

    [Test]
    public void IsValidMovementTest()
    {
        int fromX = 1;
        int fromY = 1;
        int toX = 1;
        int toY = 0;
        List<List<int>> typeDistribution = new List<List<int>>
        {
            new List<int>{1,2,1,3,15},
            new List<int>{4,1,5,6,16},
            new List<int>{7,8,9,10,17},
            new List<int>{11,12,13,14,18},
            new List<int>{19,20,21,22,23},
        };
        Board onlyOneSolutionBoard = MockGenericBoard(typeDistribution);
        _boardIterator.AddBoard(onlyOneSolutionBoard);
        bool expected = true;

        bool actual = _boardIterator.IsValidMovement(fromX, fromY, toX, toY);

        Assert.AreEqual(expected, actual);
    }

   [Test]
    public void IsNotValidMovementTest()
    {
        int fromX = 0;
        int fromY = 1;
        int toX = 1;
        int toY = 1;
        Board impossibleBoard = MockImpossibleBoard();
        _boardIterator.AddBoard(impossibleBoard);
        bool expected = false;

        bool actual = _boardIterator.IsValidMovement(fromX, fromY, toX, toY);

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void InvalidSwapTileTest()
    {
        int fromX = 1;
        int fromY = 1;
        int toX = 1;
        int toY = 0;
        int expected = 0;
        
        Board impossibleBoard = MockImpossibleBoard();
        _boardIterator.AddBoard(impossibleBoard);
        List<BoardSequence> actual = _boardIterator.SwapTile(fromX, fromY, toX, toY);
        Assert.AreEqual(expected, actual.Count);
    }

    [Test]
    public void ValidSwapTileTest()
    {
        int fromX = 1;
        int fromY = 1;
        int toX = 1;
        int toY = 0;
        List<List<int>> typeDistribution = new List<List<int>>
        {
            new List<int>{1,2,1,3,15},
            new List<int>{4,1,5,6,16},
            new List<int>{7,8,9,10,17},
            new List<int>{11,12,13,14,18},
            new List<int>{19,20,21,22,23},
        };
        Board onlyOneSolutionBoard = MockGenericBoard(typeDistribution);
        _boardIterator.AddBoard(onlyOneSolutionBoard);
        List<Vector2Int> expectedFirstMatchedTilesPositions = new List<Vector2Int>{new Vector2Int(0,0), new Vector2Int(1,0), new Vector2Int(2,0)};
        int expectedFirstMoveTilesCount = 0;
        int expectedFirstAddedTilesCount = 3;

        List<BoardSequence> actual = _boardIterator.SwapTile(fromX, fromY, toX, toY);

        Assert.True(actual.Count > 0);
        Assert.AreEqual(expectedFirstMatchedTilesPositions, actual[0].MatchedPosition);
        Assert.AreEqual(expectedFirstMoveTilesCount, actual[0].MovedTiles.Count);
        Assert.AreEqual(expectedFirstAddedTilesCount, actual[0].AddedTiles.Count);
    }

    [Test]
    public void ClearRowSwapTileTest()
    {
        int fromX = 1;
        int fromY = 1;
        int toX = 1;
        int toY = 0;
        List<List<int>> clearRowBoardTypeDistribution = new List<List<int>>
        {
            new List<int>{1,2,1,1,15},
            new List<int>{4,1,5,6,16},
            new List<int>{7,8,9,10,17},
            new List<int>{11,12,13,14,18},
            new List<int>{19,20,21,22,23},
        };
        Board clearRowBoard = MockGenericBoard(clearRowBoardTypeDistribution);
        _boardIterator.AddBoard(clearRowBoard);
        List<Vector2Int> expectedFirstMatchedTilesPositions = new List<Vector2Int>{new Vector2Int(0,0), new Vector2Int(1,0), new Vector2Int(2,0), new Vector2Int(3,0), new Vector2Int(4,0)};
        int expectedFirstMoveTilesCount = 0;
        int expectedFirstAddedTilesCount = 5;

        List<BoardSequence> actual = _boardIterator.SwapTile(fromX, fromY, toX, toY);

        Assert.True(actual.Count > 0);
        Assert.AreEqual(expectedFirstMatchedTilesPositions, actual[0].MatchedPosition);
        Assert.AreEqual(expectedFirstMoveTilesCount, actual[0].MovedTiles.Count);
        Assert.AreEqual(expectedFirstAddedTilesCount, actual[0].AddedTiles.Count);
    }

    [Test]
    public void ClearColumnSwapTileTest()
    {
        int fromX = 1;
        int fromY = 1;
        int toX = 0;
        int toY = 1;
        List<List<int>> clearColumnBoardTypeDistribution = new List<List<int>>
        {
            new List<int>{1,2,3,22,15},
            new List<int>{4,1,5,6,16},
            new List<int>{1,8,9,10,17},
            new List<int>{1,12,13,14,18},
            new List<int>{19,20,21,22,23},
        };
        Board clearColumnBoard = MockGenericBoard(clearColumnBoardTypeDistribution);
        _boardIterator.AddBoard(clearColumnBoard);
        List<Vector2Int> expectedFirstMatchedTilesPositions = new List<Vector2Int>{new Vector2Int(0,0), new Vector2Int(0,1), new Vector2Int(0,2), new Vector2Int(0,3), new Vector2Int(0,4)};
        int expectedFirstMoveTilesCount = 0;
        int expectedFirstAddedTilesCount = 5;

        List<BoardSequence> actual = _boardIterator.SwapTile(fromX, fromY, toX, toY);

        Assert.True(actual.Count > 0);
        Assert.AreEqual(expectedFirstMatchedTilesPositions, actual[0].MatchedPosition);
        Assert.AreEqual(expectedFirstMoveTilesCount, actual[0].MovedTiles.Count);
        Assert.AreEqual(expectedFirstAddedTilesCount, actual[0].AddedTiles.Count);
    }

    [Test]
    public void ClearLPatternSwapTileTest()
    {
        int fromX = 0;
        int fromY = 0;
        int toX = 1;
        int toY = 0;
        List<List<int>> clearLPatternBoardTypeDistribution = new List<List<int>>
        {
            new List<int>{1,2,1,1,15},
            new List<int>{4,1,5,6,16},
            new List<int>{8,1,9,10,17},
            new List<int>{23,12,13,14,18},
            new List<int>{19,20,21,22,23},
        };
        Board clearLPatternBoard = MockGenericBoard(clearLPatternBoardTypeDistribution);
        _boardIterator.AddBoard(clearLPatternBoard);
        List<Vector2Int> expectedFirstMatchedTilesPositions = new List<Vector2Int>{new Vector2Int(0,0), new Vector2Int(1,0), new Vector2Int(2,0), new Vector2Int(3,0), new Vector2Int(0,1), new Vector2Int(1,1), new Vector2Int(2,1), new Vector2Int(1,2)};
        int expectedFirstMoveTilesCount = 0;
        int expectedFirstAddedTilesCount = 8;

        List<BoardSequence> actual = _boardIterator.SwapTile(fromX, fromY, toX, toY);

        Assert.True(actual.Count > 0);
        Assert.AreEqual(expectedFirstMatchedTilesPositions, actual[0].MatchedPosition);
        Assert.AreEqual(expectedFirstMoveTilesCount, actual[0].MovedTiles.Count);
        Assert.AreEqual(expectedFirstAddedTilesCount, actual[0].AddedTiles.Count);
    }

    [Test]
    public void ClearColorSwapTileTest()
    {
        int fromX = 1;
        int fromY = 2;
        int toX = 0;
        int toY = 2;
        List<List<int>> clearColorBoardTypeDistribution = new List<List<int>>
        {
            new List<int>{1,2,3,1,1},
            new List<int>{1,4,5,6,16},
            new List<int>{8,1,9,10,17},
            new List<int>{1,12,13,14,18},
            new List<int>{1,20,21,22,23},
        };
        Board clearRowBoard = MockGenericBoard(clearColorBoardTypeDistribution);
        _boardIterator.AddBoard(clearRowBoard);
        List<Vector2Int> expectedFirstMatchedTilesPositions = new List<Vector2Int>{new Vector2Int(0,0), new Vector2Int(3,0), new Vector2Int(4,0), new Vector2Int(0,1), new Vector2Int(0,2), new Vector2Int(0,3), new Vector2Int(0,4)};
        int expectedFirstMoveTilesCount = 0;
        int expectedFirstAddedTilesCount = 7;

        List<BoardSequence> actual = _boardIterator.SwapTile(fromX, fromY, toX, toY);

        Assert.True(actual.Count > 0);
        Assert.AreEqual(expectedFirstMatchedTilesPositions, actual[0].MatchedPosition);
        Assert.AreEqual(expectedFirstMoveTilesCount, actual[0].MovedTiles.Count);
        Assert.AreEqual(expectedFirstAddedTilesCount, actual[0].AddedTiles.Count);
    }

    // Mock an impossible board with all pieces with different types
    // ex 
    //  1  2  3  4  5
    //  6  7  8  9 10
    // 11 12 13 14 15
    // 16 17 18 19 20
    // 21 22 23 24 25
    private Board MockImpossibleBoard(){
        List<List<Tile>> boardTiles = new(_defaultBoardHeight);
        int tilesCount = 0;
        for (int y = 0; y < _defaultBoardHeight; y++)
        {
            boardTiles.Add(new List<Tile>(_defaultBoardWidth));
            for (int x = 0; x < _defaultBoardHeight; x++)
            {
                boardTiles[y].Add(new Tile { Id = tilesCount++, Type = tilesCount });
                tilesCount++;
            }
        }
        List<int> tileTypes = new List<int> {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25};
        Board board = new Board(_defaultBoardWidth, _defaultBoardHeight, tileTypes);
        board.UpdateBoardTiles(boardTiles);
        return board;
    }

    // Mock a genenric board by a predefined List<List<int>>
    private Board MockGenericBoard(List<List<int>> typeDistribution){
        List<List<Tile>> boardTiles = new(_defaultBoardHeight);
        int tilesCount = 0;
        for (int y = 0; y < _defaultBoardHeight; y++)
        {
            boardTiles.Add(new List<Tile>(_defaultBoardWidth));
            for (int x = 0; x < _defaultBoardWidth; x++)
            {
                boardTiles[y].Add(new Tile { Id = tilesCount, Type = typeDistribution[y][x]});
                tilesCount++;
            }
        }
        List<int> tileTypes = new List<int> {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25};
        Board board = new Board(_defaultBoardWidth, _defaultBoardHeight, tileTypes);;
        board.UpdateBoardTiles(boardTiles);
        return board;
    }
}
