using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Gazeus.DesafioMatch3
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] GameObject _gameOverContainer;
        [SerializeField] TextMeshProUGUI _scoreText;
        [SerializeField] TextMeshProUGUI _highScoreText;
        void Start()
        {
            _gameOverContainer.SetActive(false);
        }

        public void ShowGameOver(int score, int highSocre)
        {
            _gameOverContainer.SetActive(true);
            _scoreText.text = $"{score}";
            _highScoreText.text = $"{highSocre}";
        }
    }
}
