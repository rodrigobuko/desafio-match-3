using System;
using System.Collections.Generic;
using DG.Tweening;
using Gazeus.DesafioMatch3.Models;
using Gazeus.DesafioMatch3.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Gazeus.DesafioMatch3.Views
{
    public class BoardView : MonoBehaviour
    {
        public event Action<int, int> TileClicked;

        [SerializeField] private GridLayoutGroup _boardContainer;
        [SerializeField] private RectTransform _boardContainerRect;
        [SerializeField] private TilePrefabRepository _tilePrefabRepository;
        [SerializeField] private TileSpotView _tileSpotPrefab;
        [SerializeField] private AnimationTileParameters _animationTileParameters;
        [SerializeField] private int _poolTileSize = 100;

        private TileView[][] _tiles;
        private TileSpotView[][] _tileSpots;
        private List<TileView> _tilePool;
        
        private void Start()
        {
            InitializeTilePool();
        }

        private void InitializeTilePool()
        {
            _tilePool = new List<TileView>();
            for (int i = 0; i < _poolTileSize; i++)
            {
                TileView tile = InstantiateTile();
                tile.gameObject.SetActive(false);
                _tilePool.Add(tile);
            }
        }

        private TileView InstantiateTile()
        {
            TileView tile = Instantiate(_tilePrefabRepository.TilePrefab);
            return tile;
        }

        private TileView GetTileFromPool()
        {
            foreach(TileView tile in _tilePool)
            {
                if (!tile.gameObject.activeInHierarchy)
                {
                    tile.gameObject.SetActive(true);
                    return tile;
                }
            }
            // If no inactive tile found, instantiate a new one
            TileView newTile = InstantiateTile();
            _tilePool.Add(newTile);
            return newTile;
        }

        private void ReturnTileToPool(TileView tile)
        {
            tile.gameObject.SetActive(false);
        }

        public void CreateBoard(List<List<Tile>> board)
        {
            _boardContainer.constraintCount = board[0].Count;
            _tiles = new TileView[board.Count][];
            _tileSpots = new TileSpotView[board.Count][];

            for (int y = 0; y < board.Count; y++)
            {
                _tiles[y] = new TileView[board[0].Count];
                _tileSpots[y] = new TileSpotView[board[0].Count];

                for (int x = 0; x < board[0].Count; x++)
                {
                    TileSpotView tileSpot = Instantiate(_tileSpotPrefab);
                    tileSpot.transform.SetParent(_boardContainer.transform, false);
                    tileSpot.SetPosition(x, y);
                    tileSpot.Clicked += TileSpot_Clicked;

                    _tileSpots[y][x] = tileSpot;

                    int tileTypeIndex = board[y][x].Type;
                    if (tileTypeIndex > -1)
                    {
                        TileView tile = GetTileFromPool();
                        tile.SetColorByType(tileTypeIndex);
                        tileSpot.SetTile(tile);

                        _tiles[y][x] = tile;
                    }
                }
            }
            
            _boardContainer.cellSize = new Vector2(_boardContainerRect.rect.width / board[0].Count, _boardContainerRect.rect.height / board.Count);
        }

        public Tween CreateTile(List<AddedTileInfo> addedTiles)
        {
            Sequence sequence = DOTween.Sequence();
            for (int i = 0; i < addedTiles.Count; i++)
            {
                AddedTileInfo addedTileInfo = addedTiles[i];
                Vector2Int position = addedTileInfo.Position;

                TileSpotView tileSpot = _tileSpots[position.y][position.x];

                TileView tile = GetTileFromPool();
                tile.SetColorByType(addedTileInfo.Type);
                tileSpot.SetTile(tile);

                _tiles[position.y][position.x] = tile;

                tile.transform.localScale = Vector2.zero;
                sequence.Join(tile.transform.DOScale(1.0f, _animationTileParameters.DefaultTileAnimationDuration));
            }

            return sequence;
        }

        public Tween DestroyTiles(List<Vector2Int> matchedPosition)
        {
            for (int i = 0; i < matchedPosition.Count; i++)
            {
                Vector2Int position = matchedPosition[i];
                ReturnTileToPool(_tiles[position.y][position.x]);
                _tiles[position.y][position.x] = null;
            }

            return DOVirtual.DelayedCall(_animationTileParameters.DefaultTileAnimationDuration, () => { });
        }

        public Tween MoveTiles(List<MovedTileInfo> movedTiles)
        {
            TileView[][] tiles = new TileView[_tiles.Length][];
            for (int y = 0; y < _tiles.Length; y++)
            {
                tiles[y] = new TileView[_tiles[y].Length];
                for (int x = 0; x < _tiles[y].Length; x++)
                {
                    tiles[y][x] = _tiles[y][x];
                }
            }

            Sequence sequence = DOTween.Sequence();
            for (int i = 0; i < movedTiles.Count; i++)
            {
                MovedTileInfo movedTileInfo = movedTiles[i];

                Vector2Int from = movedTileInfo.From;
                Vector2Int to = movedTileInfo.To;

                sequence.Join(_tileSpots[to.y][to.x].AnimatedSetTile(_tiles[from.y][from.x]));

                tiles[to.y][to.x] = _tiles[from.y][from.x];
            }

            _tiles = tiles;

            return sequence;
        }

        public Tween SwapTiles(int fromX, int fromY, int toX, int toY)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_tileSpots[fromY][fromX].AnimatedSetTile(_tiles[toY][toX]));
            sequence.Join(_tileSpots[toY][toX].AnimatedSetTile(_tiles[fromY][fromX]));

            (_tiles[toY][toX], _tiles[fromY][fromX]) = (_tiles[fromY][fromX], _tiles[toY][toX]);

            return sequence;
        }

        #region Events
        private void TileSpot_Clicked(int x, int y)
        {
            TileClicked(x, y);
        }
        #endregion
    }
}
