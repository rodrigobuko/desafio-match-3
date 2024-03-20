using DG.Tweening;
using Gazeus.DesafioMatch3.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace Gazeus.DesafioMatch3.Views
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private Vector2 shakePositions; 
        [SerializeField] private int intensity; 
        [SerializeField] private AnimationTileParameters _animationTileParameters;
        public Tween UpdateScore(int newScore)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_scoreText.transform.DOShakePosition(_animationTileParameters.DefaultTileAnimationDuration,
                strength: new Vector3(shakePositions.x, shakePositions.y, 0),
                vibrato: intensity,
                randomness: 90,
                snapping: false,
                fadeOut: true)
                .OnComplete(() =>
                {
                    _scoreText.text = newScore.ToString();
                }));
            return sequence;
        }

        public void UpdateScoreWithoutAnimation(int newScore)
        {
            _scoreText.text = newScore.ToString();
        }
    }
}
