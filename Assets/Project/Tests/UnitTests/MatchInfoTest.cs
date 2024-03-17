using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Gazeus.DesafioMatch3.Core;
using Gazeus.DesafioMatch3.Models;
using NUnit.Framework;
using UnityEngine.TestTools;

public class MatchInfoTest 
{
    [Test]
    [TestCase(3, 2, 6, 5, true)] // success
    [TestCase(5, -1, 4, 1, false)] // Column is not in the match 
    [TestCase(-1, 5, 4, 1, false)] // Row is not in the match 
    public void CheckLPatternTest(int matchRow, int matchColumn, int totalTilesMatched, int tilesMatchedType, bool expected){
        MatchInfo matcheInfo = new MatchInfo{
            MatchRow = matchRow,
            MatchColumn = matchColumn,
            TotalTilesMatched = totalTilesMatched,
            TilesMatchedType = tilesMatchedType
        };
        bool actual = matcheInfo.CheckLPattern();

        Assert.AreEqual(expected, actual);
    }

    [Test]
    [TestCase(-1, 2, 5, 1, true)] // success
    [TestCase(5, -1, 4, 1, false)] // totalTilesMatched < 5
    public void CheckColorPatternTest(int matchRow, int matchColumn, int totalTilesMatched, int tilesMatchedType, bool expected){
        MatchInfo matcheInfo = new MatchInfo{
            MatchRow = matchRow,
            MatchColumn = matchColumn,
            TotalTilesMatched = totalTilesMatched,
            TilesMatchedType = tilesMatchedType
        };
        bool actual = matcheInfo.CheckColorPattern();

        Assert.AreEqual(expected, actual);
    }

    [Test]
    [TestCase(-1, 2, 4, 1, true)]  // success  
    [TestCase(5, -1, 4, 1, false)] // Column is not in the match 
    [TestCase(-1, 4, 3, 1, false)] // totalTilesMatched < 4 
    public void CheckColumnPatternTest(int matchRow, int matchColumn, int totalTilesMatched, int tilesMatchedType, bool expected){
        MatchInfo matcheInfo = new MatchInfo{
            MatchRow = matchRow,
            MatchColumn = matchColumn,
            TotalTilesMatched = totalTilesMatched,
            TilesMatchedType = tilesMatchedType
        };
        bool actual = matcheInfo.CheckColumnPattern();

        Assert.AreEqual(expected, actual);
    }

    [Test]
    [TestCase(5, -1, 4, 1, true)]  // success  
    [TestCase(-1, 2, 4, 1, false)] // Row is not in the match 
    [TestCase(4, -1, 3, 1, false)] // totalTilesMatched < 4 
    public void CheckRowPatternTest(int matchRow, int matchColumn, int totalTilesMatched, int tilesMatchedType, bool expected){
        MatchInfo matcheInfo = new MatchInfo{
            MatchRow = matchRow,
            MatchColumn = matchColumn,
            TotalTilesMatched = totalTilesMatched,
            TilesMatchedType = tilesMatchedType
        };
        bool actual = matcheInfo.CheckRowPattern();

        Assert.AreEqual(expected, actual);
    }
}
