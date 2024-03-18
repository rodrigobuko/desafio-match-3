using System;
using System.Collections;
using System.Collections.Generic;
using Gazeus.DesafioMatch3.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gazeus.DesafioMatch3.Views
{
    public class GameModeHudView : MonoBehaviour
    {
        [Header("Classic Game Properties")]
        [SerializeField] GameObject _classicGameHudHolder;
        [SerializeField] TextMeshProUGUI _classicText;

        private Game _game;

        public void SetUpGameHud(Game game){
            _game = game;
            SetUpForGameMode();
        }

        public void UpdateGamePlays(int roundsPlayed){
            _classicText.text = $"{_game.LimitOfRounds - roundsPlayed}";
        }

        private void SetUpForGameMode(){
            switch (_game.GameMode)
            {
                case GameModes.ClassicGame:
                    _classicGameHudHolder.SetActive(true);
                    _classicText.text = $"{_game.LimitOfRounds}";
                    break;
                default:
                    return;
            }
        }
    }
}
