using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gazeus.DesafioMatch3.Models
{
    public class MatchInfo
    {
        const int kEmptyRowColumnValue = -1;
        const int kTotalTilesMatchedForColorPattern = 5;
        const int kTotalTilesMatchedForRowColumnPattern = 4;
        public int MatchRow { get; set; }
        public int MatchColumn { get; set; }
        public int TotalTilesMatched { get; set; }
        public int TilesMatchedType { get; set; }

        public bool CheckLPattern(){
            return MatchRow != kEmptyRowColumnValue && MatchColumn != kEmptyRowColumnValue;
        }

        public bool CheckColorPattern(){
            return (MatchRow != kEmptyRowColumnValue || MatchColumn != kEmptyRowColumnValue) && 
                   TotalTilesMatched == kTotalTilesMatchedForColorPattern;
        }

        public bool CheckRowPattern(){
            return MatchRow != kEmptyRowColumnValue && 
                   MatchColumn == kEmptyRowColumnValue && 
                   TotalTilesMatched == kTotalTilesMatchedForRowColumnPattern;
        }

        public bool CheckColumnPattern(){
            return MatchColumn != kEmptyRowColumnValue && 
                   MatchRow == kEmptyRowColumnValue && 
                   TotalTilesMatched == kTotalTilesMatchedForRowColumnPattern;
        }
    }
}
