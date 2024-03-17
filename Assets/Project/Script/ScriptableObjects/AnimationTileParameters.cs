
using UnityEngine;

namespace Gazeus.DesafioMatch3.ScriptableObjects
{
    [CreateAssetMenu(fileName = "AnimationTileParameters", menuName = "Gameplay/AnimationTileParameters")]
    public class AnimationTileParameters : ScriptableObject
    {
        [SerializeField] private float _defaultTileAnimationDuration;

        public float DefaultTileAnimationDuration => _defaultTileAnimationDuration;
    }
}
