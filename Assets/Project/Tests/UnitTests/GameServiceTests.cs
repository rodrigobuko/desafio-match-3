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

    [SetUp]
    public void SetUp(){
        _defaultBoardWidth = 10;
        _defaultBoardHeight = 10;
        _boardIterator = Substitute.For<IBoardIterator>();
    }

    // A Test to check if the board created has no match
    [Test]
    public void StartGameTest()
    {
        GameService gameService = new GameService(_boardIterator); 
        Board board = gameService.StartGame(_defaultBoardWidth, _defaultBoardHeight);

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

        GameService gameService = new GameService(_boardIterator);
        gameService.StartGame(_defaultBoardWidth, _defaultBoardHeight);
        bool actual = gameService.IsValidMovement(fromX, fromY, toX, toY);

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

        GameService gameService = new GameService(_boardIterator);   
        gameService.StartGame(_defaultBoardWidth, _defaultBoardHeight);
        bool actual = gameService.IsValidMovement(fromX, fromY, toX, toY);

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void SwapTileTest()
    {
        // TODO: this will be further implemented 
    }
}
