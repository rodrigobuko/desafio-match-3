using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gazeus.DesafioMatch3.Views
{
    public class MenuView : MonoBehaviour
    {
        [Header("Mode")]
        [SerializeField] GameObject _gameModeContainer;
        [SerializeField] TextMeshProUGUI _modeDescription;
        [SerializeField] TextMeshProUGUI _modeName;

        [Header("Change Mode Anination Parameters")]
        [SerializeField] float _changeModePostionOffeset;
        [SerializeField] float _animationChangeModeTransitionTime;

        [Header("HighScore")]
        [SerializeField] TextMeshProUGUI _highScoreText;

        [Header("Buttons")]
        [SerializeField] Button _playButton;
        [SerializeField] Button _leftChangeModeButton;
        [SerializeField] Button _rightChangeModeButton;

        public void SetUpMenu(Action playAction, Action leftChangeModeAction, Action rightChangeModeAction)
        {
            _playButton.onClick.AddListener(() => playAction.Invoke());
            _leftChangeModeButton.onClick.AddListener(() => leftChangeModeAction.Invoke());
            _rightChangeModeButton.onClick.AddListener(() => rightChangeModeAction.Invoke());
        }

        public void ChangeModeAnimation(string modeName, string modeDescription, bool toRight, int modeHighcore = 0)
        {
            float offset = toRight ? -_changeModePostionOffeset : _changeModePostionOffeset;
            Vector3 initialPosition = _gameModeContainer.transform.position;
            Vector3 firstPosition = initialPosition + new Vector3(offset, 0, 0);
            Vector3 secondPosition = initialPosition - new Vector3(offset, 0, 0);
            Vector3 endPostion = initialPosition;

            Sequence changeModeSequence = DOTween.Sequence();
            changeModeSequence.Append(_gameModeContainer.transform.DOMove(firstPosition, _animationChangeModeTransitionTime).OnComplete(() => {
                _gameModeContainer.SetActive(false);
            }).SetEase(Ease.InOutSine));
            changeModeSequence.Append(_gameModeContainer.transform.DOMove(secondPosition, _animationChangeModeTransitionTime).OnComplete(() => {
                _gameModeContainer.SetActive(true);
                ChangeModeWithoutAnimation(modeName, modeDescription, modeHighcore);
            }).SetEase(Ease.InOutSine));
            changeModeSequence.Append(_gameModeContainer.transform.DOMove(endPostion, _animationChangeModeTransitionTime).SetEase(Ease.InOutSine));

            changeModeSequence.OnComplete(() => {
                ToggleChangeModeButtons(true);
            });
                          
            ToggleChangeModeButtons(false);
            changeModeSequence.Play();
        }

        private void ToggleChangeModeButtons(bool toggleValue)
        {
            _leftChangeModeButton.gameObject.SetActive(toggleValue);
            _rightChangeModeButton.gameObject.SetActive(toggleValue);
        }

        public void ChangeModeWithoutAnimation(string modeName, string modeDescription, int modeHighcore = 0)
        {
            _modeName.text = modeName;
            _modeDescription.text = modeDescription;
            _highScoreText.text = modeHighcore.ToString();
        }
    }
}
