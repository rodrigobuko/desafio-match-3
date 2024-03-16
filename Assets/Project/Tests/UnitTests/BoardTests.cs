using System.Collections.Generic;
using System.Drawing;
using Gazeus.DesafioMatch3.Core;
using Gazeus.DesafioMatch3.Models;
using Gazeus.DesafioMatch3.Extensions;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BoardTests 
{
    int _defaultBoardWidth;
    int _defaultBoardHeight;
    List<int> _defaultTilesTypes;
    Board _board;

    [SetUp]
    public void SetUp(){
        _defaultBoardWidth = 4;
        _defaultBoardHeight = 4;
        _defaultTilesTypes = new List<int>{1,2,3,4,5};
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
        List<List<bool>> findMatches = boardTiles.FindMatches();
        
        Assert.AreEqual(expected, findMatches.ContainsAnyTrue());
    }

    [Test]
    public void PopulatePremadeTest()
    {
        List<int> premadeTypes = new List<int>{6, 8, 9, 10, 77, 88};
        List<List<Tile>> tilesPremade = MockBoardTiles(premadeTypes);
       
        _board.PopulateWithPremadeBoard(tilesPremade, premadeTypes);

        List<int> boardTileTypes = _board.GetTileTypes();
        List<List<Tile>> boardTiles = _board.GetBoardTiles();
        int boardTileCount = _board.TileCount;
     
        Assert.AreEqual(premadeTypes, boardTileTypes);
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

    private List<List<Tile>> MockBoardTiles(List<int> premadeTypes) {
        List<List<Tile>> boardTiles = new(_defaultBoardHeight);
        int tilesCount = 0;
        for (int y = 0; y < _defaultBoardHeight; y++)
        {
            boardTiles.Add(new List<Tile>(_defaultBoardWidth));
            for (int x = 0; x < _defaultBoardHeight; x++)
            {
                int index = Random.Range(0, premadeTypes.Count);
                boardTiles[y].Add(new Tile { Id = tilesCount++, Type = premadeTypes[index]});
                tilesCount++;
            }
        }
        return boardTiles;
    }
}
