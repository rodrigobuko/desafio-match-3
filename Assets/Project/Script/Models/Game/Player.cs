using System.Collections.Generic;

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
