using System.Collections.Generic;
using Gazeus.DesafioMatch3.Models;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace Gazeus.DesafioMatch3.Core
{
    public interface IGameService {
        Board StartGame(int boardWidth, int boardHeight);
        GameSequence SwapTile(int fromX, int fromY, int toX, int toY);
        bool IsValidMovement(int fromX, int fromY, int toX, int toY);
    }
    public class GameService: IGameService
    {
        private int _gameScore;
        private IBoardIterator _boardIterator;

        public GameService(IBoardIterator boardIterator){
            _boardIterator = boardIterator;
        }

        public Board StartGame(int boardWidth, int boardHeight)
        {
            List<int> tilesTypes = new List<int> { 0, 1, 2, 3, 4, 5};
            Board gameBoard = new Board(boardWidth, boardHeight, tilesTypes);
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
    }
}
