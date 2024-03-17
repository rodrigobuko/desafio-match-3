using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Gazeus.DesafioMatch3.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
            _scoreText.transform.DOShakePosition(0.4f, strength: new Vector3(shakePositions.x, shakePositions.y, 0), vibrato: intensity, randomness: 90, snapping: false, fadeOut: true)
            .OnComplete(() =>
            {
                _scoreText.text = newScore.ToString();
            });
            return DOVirtual.DelayedCall(_animationTileParameters.DefaultTileAnimationDuration, () => { });
        }
    }
}
