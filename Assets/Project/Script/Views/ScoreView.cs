using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gazeus.DesafioMatch3.Views
{

    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        public Tween UpdateScore(int newScore)
        {
            _scoreText.transform.DOShakePosition(0.2f, strength: new Vector3(110f, 75f, 0), vibrato: 25, randomness: 90, snapping: false, fadeOut: true)
            .OnComplete(() =>
            {
                _scoreText.text = newScore.ToString();
            });
            return DOVirtual.DelayedCall(0.2f, () => { });
        }
    }
}
