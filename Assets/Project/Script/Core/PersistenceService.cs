using System.Collections;
using System.Collections.Generic;
using Gazeus.DesafioMatch3.Models;
using Gazeus.DesafioMatch3.ScriptableObjects;
using UnityEngine;

namespace Gazeus.DesafioMatch3.Core
{
    public class PersistenceService
    {
        public Player LoadPlayerForGames(List<Game> games)
        {
            Dictionary<string, int> playerHighScores = new Dictionary<string, int>();

            foreach(Game game in games)
            {
                playerHighScores.Add(game.Id, PlayerPrefs.GetInt(game.Id.ToString()));
            }

            return new Player(playerHighScores);
        }

        public int LoadPlayerHighScoreForGame(Game game)
        {
            Dictionary<GameModes, int> playerHighScores = new Dictionary<GameModes, int>();

            return PlayerPrefs.GetInt(game.Id);
        }

        public void SavePlayerHighScoreForGame(int highScore, Game game)
        {
            PlayerPrefs.SetInt(game.Id, highScore);
        }
    }
}
