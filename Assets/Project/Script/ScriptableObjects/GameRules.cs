using System.Collections;
using System.Collections.Generic;
using Gazeus.DesafioMatch3.Models;
using UnityEngine;

namespace Gazeus.DesafioMatch3.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameRules", menuName = "Gameplay/GameRules")]
    public class GameRules : ScriptableObject
    {
        [SerializeField] private string _gameModeName;
        public string GameModeName => _gameModeName;
        [SerializeField] private GameModes _gameMode;
        public GameModes GameMode => _gameMode;
        [SerializeField] private int _boardHeight;
        public int BoardHeight => _boardHeight;

        [SerializeField] private int _boardWidth;
        public int BoardWidth => _boardWidth;

        [SerializeField] private List<int> _tileTypes;
        public List<int> TileTypes => _tileTypes;

        [SerializeField] private string _gameDescription;
        public string GameDescription => _gameDescription;

        [Header("Game Mode Particularities")]
        [SerializeField] private int _limitOfRounds;
        public int LimitOfRounds => _limitOfRounds;

        public Game GetGameFromRules()
        {
            return new Game
            {
                Name = _gameModeName,
                GameMode = _gameMode,
                BoardWidth = _boardWidth,
                BoardHeight = _boardHeight,
                TileTypes = _tileTypes,
                LimitOfRounds = _limitOfRounds
            };
        }
    }
}
