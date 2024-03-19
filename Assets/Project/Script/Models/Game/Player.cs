using System.Collections;
using System.Collections.Generic;
using Gazeus.DesafioMatch3.Models;
using UnityEngine;

namespace Gazeus.DesafioMatch3
{
    public class Player 
    {
        Dictionary<string, int> _playerHighcores;

        public Player(Dictionary<string, int> playerHighcores){
            _playerHighcores = playerHighcores;
        }

        public int GetPlayerHighScoreForGameMode(string gameModeId) => _playerHighcores[gameModeId];
    }
}
