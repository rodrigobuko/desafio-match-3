using System;
using System.Collections.Generic;
using DG.Tweening;
using Gazeus.DesafioMatch3.Core;
using Gazeus.DesafioMatch3.Models;
using Gazeus.DesafioMatch3.ScriptableObjects;
using Gazeus.DesafioMatch3.Views;
using UnityEngine;

namespace Gazeus.DesafioMatch3.Controllers
{
    public class GameController : MonoBehaviour
    {
        [Header("Views")]
        [SerializeField] private BoardView _boardView;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private ComboView _comboView;
        [SerializeField] private GameModeHudView _gameModeHudView;
        [SerializeField] private GameOverView _gameOverView;

        [Header("Game")]
        [SerializeField] private CurrentGameRules _playerCurrentRules;
        [SerializeField] private SceneRepository _sceneRepository;

        private GameRules _gameRules;
        private GameService _gameEngine;
        private SceneService _sceneEngine;
        private Game _currentGame;
        private bool _isAnimating;
        private int _selectedX = -1;
        private int _selectedY = -1;
        private int _roundsPlayed = 0;

        private void Awake()
        {
            InitializeGame();
            InitializeViews();
        }

        private void Start()
        {
            Board board = _gameEngine.StartGame();
            _boardView.CreateBoard(board.GetBoardTiles());
        }

        private void InitializeGame()
        {
            _gameRules = _playerCurrentRules.Rules;
            _currentGame = _gameRules.GetGameFromRules();
            _gameEngine = new GameService(new BoardIterator(), _currentGame);
            _sceneEngine = new SceneService(_sceneRepository.GetGameScenes());
        }

        private void InitializeViews()
        {
            _gameModeHudView.SetUpGameHud(_currentGame);
            _gameOverView.SetUpGameOver(RestartGame, () => _sceneEngine.LoadMainMenuScene());
            _boardView.TileClicked += OnTileClick;
        }

        private void AnimateBoard(GameSequence gameSequence, int index, Action onComplete)
        {
            List<BoardSequence> boardSequences = gameSequence.BoardSequence;
            BoardSequence boardSequence = boardSequences[index];
            int currentScore = gameSequence.ScoreSequence[index];

            Sequence sequence = DOTween.Sequence();
            sequence.Append(_boardView.DestroyTiles(boardSequence.MatchedPosition));
            sequence.Join(_scoreView.UpdateScore(currentScore));
            sequence.Join(_comboView.UpdateCombo(index + 1));
            sequence.Append(_boardView.MoveTiles(boardSequence.MovedTiles));
            sequence.Append(_boardView.CreateTile(boardSequence.AddedTiles));

            index += 1;
            if (index < boardSequences.Count)
            {
                sequence.onComplete += () => AnimateBoard(gameSequence, index, onComplete);
            }
            else
            {
                sequence.onComplete += () => onComplete();
            }
        }

        private void OnTileClick(int x, int y)
        {
            if (_isAnimating) return;

            if (_selectedX > -1 && _selectedY > -1)
            {
                if (Mathf.Abs(_selectedX - x) + Mathf.Abs(_selectedY - y) > 1)
                {
                    _boardView.StopAnimateTile(_selectedX, _selectedY);
                    _selectedX = x;
                    _selectedY = y;
                    _boardView.AnimateTile(x,y);
                }
                else
                {
                    _isAnimating = true;
                    _boardView.SwapTiles(_selectedX, _selectedY, x, y).onComplete += () =>
                    {
                        bool isValid = _gameEngine.IsValidMovement(_selectedX, _selectedY, x, y);
                        if (isValid)
                        {
                            GameSequence swapResult = _gameEngine.SwapTile(_selectedX, _selectedY, x, y);
                            AnimateBoard(swapResult, 0, () => {
                                EndRound();
                            });
                        }
                        else
                        {
                            _boardView.SwapTiles(x, y, _selectedX, _selectedY).onComplete += () => _isAnimating = false;
                        }
                        _selectedX = -1;
                        _selectedY = -1;
                    };
                }
            }
            else
            {
                _selectedX = x;
                _selectedY = y;
                _boardView.AnimateTile(x,y);
            }
        }

        private void EndRound(){
            _comboView.EndCombo();
            _roundsPlayed++;
            _gameModeHudView.UpdateGamePlays(_roundsPlayed);
            if(CheckEndGameCondition()){
                GameOver();
            }
            _isAnimating = false;
        }

        private bool CheckEndGameCondition()
        {
            switch (_currentGame.GameMode)
            {
                case GameModes.ClassicGame:
                    return _roundsPlayed >= _currentGame.LimitOfRounds;
                default:
                    return false;
            }
        }

        private void GameOver()
        {
            _gameOverView.ShowGameOver(_gameEngine.GetScore(), _gameEngine.GetScore());
        }

        private void RestartGame()
        {
            Board board = _gameEngine.StartGame();
            _boardView.ClearBoard();
            _boardView.PopulateBoard(board.GetBoardTiles());

            _roundsPlayed = 0;
            _gameModeHudView.UpdateGamePlays(_roundsPlayed);

            _scoreView.UpdateScoreWithoutAnimation(_gameEngine.GetScore());

            _gameOverView.DisableGameOver();
        }

        private void OnDestroy()
        {
            _boardView.TileClicked -= OnTileClick;
        }
    }
}
