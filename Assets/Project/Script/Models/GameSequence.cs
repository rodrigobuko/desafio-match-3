using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gazeus.DesafioMatch3.Models
{
    public class GameSequence 
    {
        public List<BoardSequence> BoardSequence { get; set; }
        public List<int> ScoreSequence { get; set; }
    }
}
