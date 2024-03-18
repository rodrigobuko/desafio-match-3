using System.Collections;
using System.Collections.Generic;
using Gazeus.DesafioMatch3.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Gazeus.DesafioMatch3.Views
{
    public class TileView : MonoBehaviour
    {
        [SerializeField] private TilePrefabRepository _tilePrefabRepository;
        [SerializeField] private Image _tileImage;

        public void SetColorByType(int type){
            _tileImage.color = _tilePrefabRepository.TileTypeColorList[type];
        }
    }
}
