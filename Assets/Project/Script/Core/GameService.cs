using System.Collections.Generic;
using Gazeus.DesafioMatch3.Models;
using UnityEngine;

namespace Gazeus.DesafioMatch3.Core
{
    public interface IGameService {
        Board StartGame(int boardWidth, int boardHeight);
        List<BoardSequence> SwapTile(int fromX, int fromY, int toX, int toY);
        bool IsValidMovement(int fromX, int fromY, int toX, int toY);
    }
    public class GameService: IGameService
    {
        private IBoardIterator _boardIterator;

        public GameService(IBoardIterator boardIterator){
            _boardIterator = boardIterator;
        }

        public Board StartGame(int boardWidth, int boardHeight)
        {
            List<int> tilesTypes = new List<int> { 0, 1, 2, 3 };
            Board gameBoard = new Board(boardWidth, boardHeight, tilesTypes);
            gameBoard.PopulateRandom();
            _boardIterator.AddBoard(gameBoard);
            return gameBoard;
        }

         public List<BoardSequence> SwapTile(int fromX, int fromY, int toX, int toY)
         {
            return _boardIterator.SwapTile(fromX, fromY, toX, toY);
         }

        public bool IsValidMovement(int fromX, int fromY, int toX, int toY)
        {
            return _boardIterator.IsValidMovement(fromX, fromY, toX, toY);
        }
    }
}
