using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Gazeus.DesafioMatch3.Core;
using Gazeus.DesafioMatch3.Models;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BoardIteratorTests
{
    int _defaultBoardWidth;
    int _defaultBoardHeight;
    BoardIterator _boardIterator;

    [SetUp]
    public void SetUp(){
        _defaultBoardWidth = 4;
        _defaultBoardHeight = 4;

        _boardIterator = new BoardIterator();
    }

    [Test]
    public void IsValidMovementTest()
    {
        int fromX = 1;
        int fromY = 1;
        int toX =  1;
        int toY = 0;
        Board onlyOneSolutionBoard = MockOnlyOneSolutionBoard();
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
        int toX =  1;
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
        int toX =  1;
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
        int toX =  1;
        int toY = 0;
        Board onlyOneSolutionBoard = MockOnlyOneSolutionBoard();
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

    // Mock an impossible board with all pieces with different types
    // ex 
    // 1   2  3  4
    // 5   6  7  8
    // 9  10 11 12 
    // 13 14 15 16 
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
        List<int> tileTypes = new List<int> {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16};
        Board board = new Board(_defaultBoardWidth, _defaultBoardHeight, tileTypes);
        board.UpdateBoardTiles(boardTiles);
        return board;
    }

    // Mock a board that there is one match avaiable
    // ex 
    //  1  2  1  3
    //  4  1  5  6
    //  7  8  9 10 
    // 11 12 13 14 
    private Board MockOnlyOneSolutionBoard(){
        List<List<int>> typeDistribution = new List<List<int>>
        {
            new List<int>{1,2,1,3},
            new List<int>{4,1,5,6},
            new List<int>{7,8,9,10},
            new List<int>{11,12,13,14},
        };
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
        List<int> tileTypes = new List<int> {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16};;
        Board board = new Board(_defaultBoardWidth, _defaultBoardHeight, tileTypes);;
        board.UpdateBoardTiles(boardTiles);
        return board;
    }
}
