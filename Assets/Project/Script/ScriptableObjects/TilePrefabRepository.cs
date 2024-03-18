using Gazeus.DesafioMatch3.Views;
using UnityEngine;

namespace Gazeus.DesafioMatch3.ScriptableObjects
{
    [CreateAssetMenu(fileName = "TilePrefabRepository", menuName = "Gameplay/TilePrefabRepository")]
    public class TilePrefabRepository : ScriptableObject
    {
        [SerializeField] private Color[] _tileTypeColorList;
        public Color[] TileTypeColorList => _tileTypeColorList;
        [SerializeField] private TileView _tilePrefab;
        public TileView TilePrefab => _tilePrefab;
    }
}
