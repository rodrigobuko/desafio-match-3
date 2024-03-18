using System;
using System.Collections.Generic;
using Codice.Client.BaseCommands.Update.Transformers;
using Codice.CM.SEIDInfo;
using Gazeus.DesafioMatch3.Models;
using Gazeus.DesafioMatch3.ScriptableObjects;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace Gazeus.DesafioMatch3.Core
{
    public interface IGameService {
        Board StartGame();
        GameSequence SwapTile(int fromX, int fromY, int toX, int toY);
        bool IsValidMovement(int fromX, int fromY, int toX, int toY);
        int GetScore();
    }
    public class GameService: IGameService
    {
        private int _gameScore;
        private IBoardIterator _boardIterator;
        private Game _game;

        public GameService(IBoardIterator boardIterator, Game game){
            _boardIterator = boardIterator;
            _game = game;
        }

        public Board StartGame()
        {
            List<int> tilesTypes = _game.TileTypes;
            Board gameBoard = new Board(_game.BoardWidth, _game.BoardHeight, tilesTypes);
            gameBoard.PopulateRandom();
            _boardIterator.AddBoard(gameBoard);
            return gameBoard;
        }

         public GameSequence SwapTile(int fromX, int fromY, int toX, int toY)
         {
            List<BoardSequence> boardSequences =  _boardIterator.SwapTile(fromX, fromY, toX, toY);
            List<int> scoreSequences = new List<int>();
            int comboMultiplier = 1;
            foreach(BoardSequence boardSequence in boardSequences){
                _gameScore += boardSequence.MatchedPosition.Count * comboMultiplier;
                scoreSequences.Add(_gameScore);
                comboMultiplier++;
            }

            GameSequence gameSequence = new(){
                BoardSequence = boardSequences,
                ScoreSequence = scoreSequences
            };

            return gameSequence;
         }

        public bool IsValidMovement(int fromX, int fromY, int toX, int toY)
        {
            return _boardIterator.IsValidMovement(fromX, fromY, toX, toY);
        }

        public int GetScore()
        {
            return _gameScore;
        }
    }
}
