using System.Collections.Generic;
using Gazeus.DesafioMatch3.Models;
using Gazeus.DesafioMatch3.Extensions;
using NUnit.Framework;
using UnityEngine;

public class BoardTests 
{
    int _defaultBoardWidth;
    int _defaultBoardHeight;
    List<int> _defaultTilesTypes;
    Board _board;

    [SetUp]
    public void SetUp(){
        _defaultBoardWidth = 5;
        _defaultBoardHeight = 5;
        _defaultTilesTypes = new List<int>{0,1,2,3,4,5};
        _board = new Board(_defaultBoardWidth, _defaultBoardHeight, _defaultTilesTypes);
    }

    [Test]
    public void PopulateRandomTest()
    {
        _board.PopulateRandom();
        List<List<Tile>> boardTiles = _board.GetBoardTiles();
        
        Assert.AreEqual(_defaultBoardHeight, boardTiles.Count);
        Assert.AreEqual(_defaultBoardWidth, boardTiles[0].Count);
    }

    [Test]
    public void PopulateRandomShouldHaveNoMatchesTest()
    {
        bool expected = false;
        _board.PopulateRandom();
        List<List<Tile>> boardTiles = _board.GetBoardTiles();
        List<List<bool>> possibleMatches = boardTiles.FindMatches();
        
        Assert.AreEqual(expected, possibleMatches.ContainsAnyMatch());
    }

    [Test]
    public void PopulatePremadeTest()
    {
        List<List<int>> premadeTypes = new List<List<int>>
        {
            new List<int>{1,2,1,1,15},
            new List<int>{4,1,5,6,16},
            new List<int>{8,1,9,10,17},
            new List<int>{23,12,13,14,18},
            new List<int>{19,20,21,22,23},
        };
        List<List<Tile>> tilesPremade = MockBoardTiles(premadeTypes);
       
        _board.PopulateWithPremadeBoard(tilesPremade, _defaultTilesTypes);

        List<int> boardTileTypes = _board.GetTileTypes();
        List<List<Tile>> boardTiles = _board.GetBoardTiles();
        int boardTileCount = _board.TileCount;
     
        Assert.AreEqual(_defaultTilesTypes, boardTileTypes);
        Assert.AreEqual(tilesPremade, boardTiles);
        Assert.AreEqual(boardTileCount, tilesPremade[0].Count*tilesPremade.Count);
    }

    [Test]
    public void CopyBoardTilesTest()
    {
        List<List<Tile>> boardTiles = _board.GetBoardTiles();
        List<List<Tile>> copiedBoardTiles = _board.CopyBoardTiles();

        int fromX = _defaultBoardWidth - 2;
        int fromY = _defaultBoardHeight - 2;
        int toX =  _defaultBoardWidth - 1;
        int toY =  _defaultBoardHeight - 2;
        (copiedBoardTiles[toY][toX], copiedBoardTiles[fromY][fromX]) = (copiedBoardTiles[fromY][fromX], copiedBoardTiles[toY][toX]);

        Assert.AreNotEqual(copiedBoardTiles, boardTiles);
    }

    [Test]
    public void UpdateBoardTilesTest()
    {
        List<List<Tile>> copiedBoardTiles = _board.CopyBoardTiles();
        int fromX = _defaultBoardWidth - 2;
        int fromY = _defaultBoardHeight - 2;
        int toX =  _defaultBoardWidth - 1;
        int toY =  _defaultBoardHeight - 2;
        (copiedBoardTiles[toY][toX], copiedBoardTiles[fromY][fromX]) = (copiedBoardTiles[fromY][fromX], copiedBoardTiles[toY][toX]);

        _board.UpdateBoardTiles(copiedBoardTiles);
        List<List<Tile>> boardTiles = _board.GetBoardTiles();

        Assert.AreEqual(copiedBoardTiles, boardTiles);
    }

    [Test]
    public void HasColorPatternTest()
    {
        List<List<int>> premadeTypes = new List<List<int>>
        {
            new List<int>{1,1,1,1,1},
            new List<int>{4,3,5,6,16},
            new List<int>{8,17,9,10,17},
            new List<int>{23,12,13,14,18},
            new List<int>{19,20,21,22,23},
        };
        bool expected = true;
        List<List<Tile>> tilesPremade = MockBoardTiles(premadeTypes);
        Vector2Int positionToCheck = new Vector2Int(0, 0);

        bool actual = tilesPremade.HasColorPattern(positionToCheck);

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void HasLPatternTest()
    {
        List<List<int>> premadeTypes = new List<List<int>>
        {
            new List<int>{5,1,1,1,7},
            new List<int>{4,1,5,6,16},
            new List<int>{8,1,9,10,17},
            new List<int>{23,12,13,14,18},
            new List<int>{19,20,21,22,23},
        };
        bool expected = true;
        List<List<Tile>> tilesPremade = MockBoardTiles(premadeTypes);
        Vector2Int positionToCheck = new Vector2Int(1, 0);

        bool actual = tilesPremade.HasLPattern(positionToCheck);

        Assert.AreEqual(expected, actual);
    }

        [Test]
    public void HasRowPatternTest()
    {
        List<List<int>> premadeTypes = new List<List<int>>
        {
            new List<int>{1,1,1,1,7},
            new List<int>{4,7,5,6,16},
            new List<int>{8,5,9,10,17},
            new List<int>{23,1,13,14,18},
            new List<int>{19,20,21,22,23},
        };
        bool expected = true;
        List<List<Tile>> tilesPremade = MockBoardTiles(premadeTypes);
         Vector2Int positionToCheck = new Vector2Int(1, 0);

        bool actual = tilesPremade.HasRowPattern(positionToCheck);

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void HasColumnPatternTest()
    {
        List<List<int>> premadeTypes = new List<List<int>>
        {
            new List<int>{5,1,4,9,7},
            new List<int>{4,1,5,6,16},
            new List<int>{8,1,9,10,17},
            new List<int>{23,1,13,14,18},
            new List<int>{19,20,21,22,23},
        };
        bool expected = true;
        List<List<Tile>> tilesPremade = MockBoardTiles(premadeTypes);
         Vector2Int positionToCheck = new Vector2Int(1, 1);

        bool actual = tilesPremade.HasColumnPattern(positionToCheck);

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void HasCrossPatternTest()
    {
        List<List<int>> premadeTypes = new List<List<int>>
        {
            new List<int>{5,1,8,4,7},
            new List<int>{1,1,1,6,16},
            new List<int>{8,1,9,10,17},
            new List<int>{23,12,13,14,18},
            new List<int>{19,20,21,22,23},
        };
        bool expected = true;
        List<List<Tile>> tilesPremade = MockBoardTiles(premadeTypes);
        Vector2Int positionToCheck = new Vector2Int(1, 1);

        bool actual = tilesPremade.HasCrossPattern(positionToCheck);

        Assert.AreEqual(expected, actual);
    }

    private List<List<Tile>> MockBoardTiles(List<List<int>> premadeTypes) {
        List<List<Tile>> boardTiles = new(_defaultBoardHeight);
        int tilesCount = 0;
        for (int y = 0; y < _defaultBoardHeight; y++)
        {
            boardTiles.Add(new List<Tile>(_defaultBoardWidth));
            for (int x = 0; x < _defaultBoardHeight; x++)
            {
                boardTiles[y].Add(new Tile { Id = tilesCount++, Type = premadeTypes[y][x]});
                tilesCount++;
            }
        }
        return boardTiles;
    }
}
