using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gazeus.DesafioMatch3
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] GameObject _gameOverContainer;
        [SerializeField] TextMeshProUGUI _scoreText;
        [SerializeField] TextMeshProUGUI _highScoreText;
        [SerializeField] Button _playAgainButton;
        [SerializeField] Button _backToMenuButton;
        void Start()
        {
            DisableGameOver();
        }

        public void SetUpGameOver(Action playAgainAction, Action backToMenuAction)
        {
            _playAgainButton.onClick.AddListener(() => playAgainAction.Invoke());
            _backToMenuButton.onClick.AddListener(() => backToMenuAction.Invoke());
        }

        public void ShowGameOver(int score, int highSocre)
        {
            _gameOverContainer.SetActive(true);
            _scoreText.text = score.ToString();
            _highScoreText.text = highSocre.ToString();
        }

        public void DisableGameOver()
        {
            _gameOverContainer.SetActive(false);
        }
    }
}
