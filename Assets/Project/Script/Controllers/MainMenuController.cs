using System;
using System.Collections;
using System.Collections.Generic;
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
        private List<GameRules> _gameModes;
        private int _modesIndex;

        private void Awake()
        {
            _sceneEngine = new SceneService(_sceneRepository.GetGameScenes());
            _menuView.SetUpMenu(PlayGame, MoveModesLeft, MoveModesRight);
            _gameModes = _gameRepository.Games;
            _modesIndex = 0;
        }

        private void PlayGame()
        {
            _sceneEngine.LoadGameScene();
        }

        private void MoveModesRight()
        {
            _modesIndex++;
            _modesIndex = Math.Min(_modesIndex, _gameModes.Count - 1);
            UpdateMode();
        }

        private void MoveModesLeft()
        {
            _modesIndex--;
            _modesIndex = Math.Max(0, _modesIndex);
            UpdateMode();
        }

        private void UpdateMode()
        {
            GameRules currentRule = _gameModes[_modesIndex];
            _menuView.ChangeMode(currentRule.GameModeName, currentRule.GameDescription);
            _currentGameRules.ChangeCurrentRule(currentRule);
        }
    }
}
