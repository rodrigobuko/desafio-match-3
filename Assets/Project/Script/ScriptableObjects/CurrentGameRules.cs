using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gazeus.DesafioMatch3.ScriptableObjects
{
    [CreateAssetMenu(fileName = "CurrentGameRules", menuName = "Game/CurrentGameRules")]
    public class CurrentGameRules : ScriptableObject
    {
        [SerializeField] private GameRules _currentRules;
        public GameRules Rules => _currentRules;

        public void ChangeCurrentRule(GameRules gameRules){
            _currentRules = gameRules;
        }
    }
}
