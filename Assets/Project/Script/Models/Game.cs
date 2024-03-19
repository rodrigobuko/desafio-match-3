using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gazeus.DesafioMatch3.Models
{
    public class Game
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public GameModes GameMode { get; set; }
        public int BoardHeight { get; set; }
        public int BoardWidth { get; set; }
        public List<int> TileTypes { get; set; }
        public int LimitOfRounds { get; set; }
    }
}
