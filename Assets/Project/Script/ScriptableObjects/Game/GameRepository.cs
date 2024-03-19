using System.Collections;
using System.Collections.Generic;
using Gazeus.DesafioMatch3.Models;
using UnityEngine;

namespace Gazeus.DesafioMatch3.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameRepository", menuName = "Game/GameRepository")]
    public class GameRepository : ScriptableObject
    {
        [SerializeField] private List<GameRules> _gamesRules;
        public List<GameRules> GamesRules => _gamesRules;

        public List<Game> GetGames()
        {
            List<Game> games = new List<Game>();
            foreach(GameRules gameRule in _gamesRules)
            {
                games.Add(gameRule.GetGameFromRules());
            }
            return games;
        }
    }
}
