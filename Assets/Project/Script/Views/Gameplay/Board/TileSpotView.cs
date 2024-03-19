using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Gazeus.DesafioMatch3.Views
{
    public class TileSpotView : MonoBehaviour
    {
        public event Action<int, int> Clicked;

        [SerializeField] private Button _button;

        private int _x;
        private int _y;

        private void Awake()
        {
            _button.onClick.AddListener(OnTileClick);
        }

        public Tween AnimatedSetTile(TileView tile, float duration)
        {
            tile.transform.SetParent(transform);
            tile.transform.DOKill();

            return tile.transform.DOMove(transform.position, duration);
        }

        public void SetPosition(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public void SetTile(TileView tile)
        {
            tile.transform.SetParent(transform, false);
            tile.transform.position = transform.position;
        }

        private void OnTileClick()
        {
            Clicked?.Invoke(_x, _y);
        }
    }
}
