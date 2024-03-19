using System;
using System.Collections;
using System.Collections.Generic;
using Gazeus.DesafioMatch3.Core;
using Gazeus.DesafioMatch3.Models;
using Gazeus.DesafioMatch3.ScriptableObjects;
using Gazeus.DesafioMatch3.Views;
using UnityEngine;

namespace Gazeus.DesafioMatch3.Controllers
{
    public class MainMenuController : MonoBehaviour
    {
        [Header("Views")]
        [SerializeField] private MenuView _menuView;

        [Header("Game")]
        [SerializeField] private SceneRepository _sceneRepository;
        [SerializeField] private GameRepository _gameRepository;
        [SerializeField] private CurrentGameRules _currentGameRules;

        private SceneService _sceneEngine;
        private PersistenceService _persistenceEngine;
        private List<GameRules> _gameModes;
        private int _modesIndex;
        private Player _currentPlayer;

        private void Awake()
        {
            _sceneEngine = new SceneService(_sceneRepository.GetGameScenes());
            _persistenceEngine = new PersistenceService();
            _gameModes = _gameRepository.GamesRules;
            _currentPlayer = _persistenceEngine.LoadPlayerForGames(_gameRepository.GetGames());

            InitializeViews();
        }

        private void InitializeViews()
        {
            _modesIndex = 0;
            _menuView.SetUpMenu(PlayGame, MoveModesLeft, MoveModesRight);
            GameRules currentGameMode = _gameModes[_modesIndex];
            int gameModeHighScore = _currentPlayer.GetPlayerHighScoreForGameMode(currentGameMode.GameId);
            _menuView.ChangeModeWithoutAnimation(currentGameMode.GameModeName, currentGameMode.GameDescription, gameModeHighScore);
            
            _currentGameRules.ChangeCurrentRule(currentGameMode);
        }

        private void PlayGame()
        {
            _sceneEngine.LoadGameScene();
        }

        private void MoveModesRight()
        {
            _modesIndex++;
            _modesIndex = _modesIndex >= _gameModes.Count ? 0 : _modesIndex;
            UpdateMode(true);
        }

        private void MoveModesLeft()
        {
            _modesIndex--;
            _modesIndex = _modesIndex < 0 ? _gameModes.Count - 1 : _modesIndex;
            UpdateMode(false);
        }

        private void UpdateMode(bool toRight)
        {
            GameRules currentMode = _gameModes[_modesIndex];
            int gameModeHighScore = _currentPlayer.GetPlayerHighScoreForGameMode(currentMode.GameId);
            _menuView.ChangeModeAnimation(currentMode.GameModeName, currentMode.GameDescription, toRight, gameModeHighScore);
            _currentGameRules.ChangeCurrentRule(currentMode);
        }
    }
}
