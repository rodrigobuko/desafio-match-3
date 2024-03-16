using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gazeus.DesafioMatch3.Models
{
    public class Board 
    {
        private List<List<Tile>> _boardTiles;
        private int _tilesCount;
        private List<int> _tileTypes;
        private int _height;
        private int _width;
        public List<List<Tile>>  GetBoardTiles() => _boardTiles;
        public List<int> GetTileTypes() => _tileTypes;

        public Board(int width, int height, List<int> tileTypes){
            _tileTypes = tileTypes;
            _width = width;
            _height = height;
            CreateEmpty();
        }

        public int TileCount {
            get {return _tilesCount;}
            set {_tilesCount = value;}
        }

        private void CreateEmpty(){
            _boardTiles = new(_height);

            _tilesCount = 0;
            for (int y = 0; y < _height; y++)
            {
                _boardTiles.Add(new List<Tile>(_width));
                for (int x = 0; x < _width; x++)
                {
                    _boardTiles[y].Add(new Tile { Id = -1, Type = -1 });
                }
            }
        }

        public List<List<Tile>> CopyBoardTiles()
        {
            List<List<Tile>> newBoardTiles = new(_boardTiles.Count);
            for (int y = 0; y < _boardTiles.Count; y++)
            {
                newBoardTiles.Add(new List<Tile>(_boardTiles[y].Count));
                for (int x = 0; x < _boardTiles[y].Count; x++)
                {
                    Tile tile = _boardTiles[y][x];
                    newBoardTiles[y].Add(new Tile { Id = tile.Id, Type = tile.Type });
                }
            }

            return newBoardTiles;
        }

        public void PopulateRandom()
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    List<int> noMatchTypes = new(_tileTypes.Count);
                    for (int i = 0; i < _tileTypes.Count; i++)
                    {
                        noMatchTypes.Add(_tileTypes[i]);
                    }

                    if (x > 1 &&
                        _boardTiles[y][x - 1].Type == _boardTiles[y][x - 2].Type)
                    {
                        noMatchTypes.Remove(_boardTiles[y][x - 1].Type);
                    }

                    if (y > 1 &&
                        _boardTiles[y - 1][x].Type == _boardTiles[y - 2][x].Type)
                    {
                        noMatchTypes.Remove(_boardTiles[y - 1][x].Type);
                    }

                    _boardTiles[y][x].Id = _tilesCount++;
                    _boardTiles[y][x].Type = noMatchTypes[UnityEngine.Random.Range(0, noMatchTypes.Count)];
                }
            }
        }

         public void PopulateWithPremadeBoard(List<List<Tile>> tiles, List<int> tileTypes){
            _width = tiles[0].Count;
            _height = tiles.Count;
            _boardTiles = tiles;
            _tileTypes = tileTypes;
            _tilesCount = tiles.Count * tiles[0].Count;
         }

        public void UpdateBoardTiles(List<List<Tile>> tiles){
            _boardTiles = tiles;
        }
    }
}
