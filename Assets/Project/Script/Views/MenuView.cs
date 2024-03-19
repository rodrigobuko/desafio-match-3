using System;
using System.Collections;
using System.Collections.Generic;
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

        public void ChangeMode(string modeName, string modeDescription)
        {
            _modeName.text = modeName;
            _modeDescription.text = modeDescription;
        }
    }
}
