using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gazeus.DesafioMatch3.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameRepository", menuName = "Game/GameRepository")]
    public class GameRepository : ScriptableObject
    {
        [SerializeField] private List<GameRules> _games;
        public List<GameRules> Games => _games;
    }
}
