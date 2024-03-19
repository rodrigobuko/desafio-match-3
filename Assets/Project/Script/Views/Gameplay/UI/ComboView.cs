using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Gazeus.DesafioMatch3.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gazeus.DesafioMatch3.Views
{
    public class ComboView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _comboText;
        [SerializeField] private float _scaleEffectFactor;
        [SerializeField] private float _maxScale;
        [SerializeField] private int mediumCombo;
        [SerializeField] private Color mediumComboColor;
        [SerializeField] private int highCombo;
        [SerializeField] private Color highComboColor;
        [SerializeField] private AnimationTileParameters _animationTileParameters;

        Vector3 _initialScale;
        Color _initialColor;

        void Awake()
        {
            _initialScale = _comboText.transform.localScale;
            _initialColor = _comboText.color;
        }

        public Tween UpdateCombo(int comboMultiplier)
        {
            Sequence sequence = DOTween.Sequence();
            float updateScale = _scaleEffectFactor * comboMultiplier;
            float xScale = Math.Min(_initialScale.x + updateScale, _maxScale);
            float yScale = Math.Min(_initialScale.y + updateScale, _maxScale);
            Vector3 currentScale = new Vector3(xScale, yScale, 0);

            _comboText.text = $"{comboMultiplier}x";
            sequence.Append(_comboText.transform.DOScale(currentScale, _animationTileParameters.DefaultTileAnimationDuration));
            
            ApplyColorToCombo(comboMultiplier);

            return sequence;
        }

        public Tween EndCombo()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_comboText.transform.DOScale(_initialScale, _animationTileParameters.DefaultTileAnimationDuration)
            .OnComplete(() =>
            {
                _comboText.text = $"{1}x";
                _comboText.color = _initialColor;
            }));
            return sequence;
        }

        private void ApplyColorToCombo(int comboMultiplier){
            if(comboMultiplier > mediumCombo && comboMultiplier < highCombo){
                _comboText.color = mediumComboColor;
            }
            if(comboMultiplier >= highCombo){
                _comboText.color = highComboColor;
            }
        }
    }
}
