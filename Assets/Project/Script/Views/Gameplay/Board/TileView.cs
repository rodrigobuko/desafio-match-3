using System;
using DG.Tweening;
using Gazeus.DesafioMatch3.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Gazeus.DesafioMatch3.Views
{
    public class TileView : MonoBehaviour
    {
        [SerializeField] private TilePrefabRepository _tilePrefabRepository;
        [SerializeField] private Image _tileImage;
        [SerializeField] private AnimationTileParameters _animationTileParameters;

        [Header("Select Tile Animation")]
        [SerializeField] private float scaleFactor = 0.8f;
        [SerializeField] private float scaleTime = 0.5f;
        [SerializeField] private float selectTileDelayTime = 0.5f;

        [Header("MatchTileAnimation")]
        [SerializeField] private float punchRotationPower = 1;

        Sequence _selectTileSequence;

        public void SetColorByType(int type)
        {
            _tileImage.color = _tilePrefabRepository.TileTypeColorList[type];
        }

        public void SelectTileAnimation()
        {

            Vector3 targetScale = Vector3.one * scaleFactor;

            _selectTileSequence = DOTween.Sequence();
            _selectTileSequence.Append(transform.DOScale(targetScale, scaleTime));
            _selectTileSequence.Append(transform.DOScale(1.0f, scaleTime))
            .AppendInterval(selectTileDelayTime)
            .SetLoops(-1, LoopType.Restart);
            _selectTileSequence.Play();
        }

        public void StopSelectTileAnimation()
        {
            _selectTileSequence.Kill();
            transform.localScale = Vector3.one;
        }

        public Tween MatchTileShakeAnimation()
        {
            return transform.DOPunchRotation(new Vector3(0,0,punchRotationPower), _animationTileParameters.DefaultTileAnimationDuration);
        }

        public Tween MatchTileDeleteAnimation(Action afterTileDeleted)
        {
            return transform.DOScale(0, _animationTileParameters.DefaultTileAnimationDuration).OnComplete(() => {
                transform.localScale = Vector3.one;
                afterTileDeleted.Invoke();
            });
        }

        public Tween CreateTileAnimation()
        {
            transform.localScale = Vector3.zero;
            return transform.DOScale(1.0f, _animationTileParameters.DefaultTileAnimationDuration);
        }
    }
}
