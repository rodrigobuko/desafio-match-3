using Gazeus.DesafioMatch3.Models;
using TMPro;
using UnityEngine;

namespace Gazeus.DesafioMatch3.Views
{
    public class GameModeHudView : MonoBehaviour
    {
        [Header("Classic Game Properties")]
        [SerializeField] GameObject _classicGameHudHolder;
        [SerializeField] TextMeshProUGUI _classicText;

        [Header("Time Game Properties")]
        [SerializeField] GameObject _timeGameHudHolder;
        [SerializeField] TextMeshProUGUI _timeText;
        [SerializeField] float _timeTextEndingThreshold = 10f;

        private Game _game;
        private Color _initialTimeTextColor;

        public void SetUpGameHud(Game game)
        {
            _game = game;
            SetUpForGameMode();
        }

        private void SetUpForGameMode()
        {
            _classicGameHudHolder.SetActive(false);
            _timeGameHudHolder.SetActive(false);

            switch (_game.GameMode)
            {
                case GameModes.ClassicGame:
                    _classicGameHudHolder.SetActive(true);
                    _classicText.text = _game.LimitOfRounds.ToString();
                    break;
                case GameModes.TimeGame:
                    _timeGameHudHolder.SetActive(true);
                    _timeText.text = _game.LimitOfTimeInSeconds.ToString("F0");
                    _initialTimeTextColor = _timeText.color;
                    break;
                default:
                    return;
            }
        }

        public void UpdateGamePlays(int roundsPlayed)
        {
            _classicText.text = $"{_game.LimitOfRounds - roundsPlayed}";
        }

        public void UpdateGameTime(float timePlayed)
        {
            float timeElapsed = _game.LimitOfTimeInSeconds - timePlayed;
            timeElapsed = Mathf.Max(0, timeElapsed);
            if(timeElapsed > _timeTextEndingThreshold)
            {
                _timeText.text = timeElapsed.ToString("F0");
                _timeText.color = _initialTimeTextColor;
            }else 
            {
                _timeText.text = timeElapsed.ToString("F2");
                _timeText.color = Color.red;
            }
            
        }
    }
}
