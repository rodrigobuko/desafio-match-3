using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Gazeus.DesafioMatch3.Models;
using UnityEngine;

namespace Gazeus.DesafioMatch3.Extensions
{
    public static class BoardTilesExtensions 
    {
        public static List<List<bool>> FindMatches( this List<List<Tile>> boardTiles)
        {
            List<List<bool>> matchedTiles = new();
            for (int y = 0; y < boardTiles.Count; y++)
            {
                matchedTiles.Add(new List<bool>(boardTiles[y].Count));
                for (int x = 0; x < boardTiles[y].Count; x++)
                {
                    matchedTiles[y].Add(false);
                }
            }

            for (int y = 0; y < boardTiles.Count; y++)
            {
                for (int x = 0; x < boardTiles[y].Count; x++)
                {
                    if (x > 1 &&
                        boardTiles[y][x].Type == boardTiles[y][x - 1].Type &&
                        boardTiles[y][x - 1].Type == boardTiles[y][x - 2].Type)
                    {
                        matchedTiles[y][x] = true;
                        matchedTiles[y][x - 1] = true;
                        matchedTiles[y][x - 2] = true;
                    }

                    if (y > 1 &&
                        boardTiles[y][x].Type == boardTiles[y - 1][x].Type &&
                        boardTiles[y - 1][x].Type == boardTiles[y - 2][x].Type)
                    {
                        matchedTiles[y][x] = true;
                        matchedTiles[y - 1][x] = true;
                        matchedTiles[y - 2][x] = true;
                    }
                }
            }
            
            return matchedTiles;
        }

        public static bool HasLPattern( this List<List<Tile>> boardTiles, Vector2Int position){
            int row = position.y;
            int column = position.x;
            int currentType = boardTiles[row][column].Type;

            for(int y = 0; y < boardTiles.Count; y++)
            {
                for(int x = 0; x < boardTiles[row].Count; x++){
                    bool hasRowMatch = false;
                    bool hasColumnMatch = false;

                    if (x > 1)
                    {
                        if (
                            boardTiles[row][x - 2].Type == currentType &&
                            boardTiles[row][x - 1].Type == currentType &&
                            boardTiles[row][x].Type == currentType)
                        {
                            hasRowMatch = true;
                        }
                    }

                    if (x < boardTiles[row].Count - 2)
                    {
                        if (
                            boardTiles[row][x].Type == currentType &&
                            boardTiles[row][x + 1].Type == currentType &&
                            boardTiles[row][x + 2].Type == currentType)
                        {
                            hasRowMatch = true;
                        }
                    }

                    if (y > 1)
                    {
                        if (boardTiles[y - 2][column].Type == currentType &&
                            boardTiles[y - 1][column].Type == currentType &&
                            boardTiles[y][column].Type == currentType)
                        {
                            hasColumnMatch = true;
                        }
                    }

                    if (y < boardTiles.Count - 2)
                    {
                        if (boardTiles[y][column].Type == currentType &&
                            boardTiles[y + 1][column].Type == currentType &&
                            boardTiles[y + 2][column].Type == currentType)
                        {
                            hasColumnMatch = true;
                        }
                    }

                    if(hasColumnMatch && hasRowMatch && (x == column && y == row)){
                        return true;
                    }
                }
            }
                        
            return false;
        }

        public static bool HasRowPattern( this List<List<Tile>> boardTiles, int row){
            for(int x = 0; x < boardTiles[row].Count; x++){
                int currentType = boardTiles[row][x].Type;
                if (x > 2)
                {
                    if (boardTiles[row][x - 3].Type == currentType &&
                        boardTiles[row][x - 2].Type == currentType &&
                        boardTiles[row][x - 1].Type == currentType &&
                        boardTiles[row][x].Type == currentType)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool HasColumnPattern( this List<List<Tile>> boardTiles, int column){
            for(int y = 0; y < boardTiles.Count; y++){
                int currentType = boardTiles[y][column].Type;
                if (y > 2)
                {
                    if (boardTiles[y - 3][column].Type == currentType &&
                        boardTiles[y - 2][column].Type == currentType &&
                        boardTiles[y - 1][column].Type == currentType &&
                        boardTiles[y][column].Type == currentType)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool HasColorPattern( this List<List<Tile>> boardTiles, Vector2Int position){
            int row = position.y;
            int column = position.x;
            for(int y = 0; y < boardTiles.Count; y++){
                int currentType = boardTiles[y][column].Type;
                if (y > 3)
                {
                    if (boardTiles[y - 4][column].Type == currentType &&
                        boardTiles[y - 3][column].Type == currentType &&
                        boardTiles[y - 2][column].Type == currentType &&
                        boardTiles[y - 1][column].Type == currentType &&
                        boardTiles[y][column].Type == currentType)
                    {
                        return true;
                    }
                }
            }

            for(int x = 0; x < boardTiles[row].Count; x++){
                int currentType = boardTiles[row][x].Type;
                if (x > 3)
                {
                    if (boardTiles[row][x - 4].Type == currentType &&
                        boardTiles[row][x - 3].Type == currentType &&
                        boardTiles[row][x - 2].Type == currentType &&
                        boardTiles[row][x - 1].Type == currentType &&
                        boardTiles[row][x].Type == currentType)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool HasCrossPattern( this List<List<Tile>> boardTiles, Vector2Int position){
            int row = position.y;
            int column = position.x;
            int currentType = boardTiles[row][column].Type;

            for(int y = 0; y < boardTiles.Count; y++)
            {
                for(int x = 0; x < boardTiles[row].Count; x++){
                    bool hasRowMatch = false;
                    bool hasColumnMatch = false;

                    if (x > 0 && x < boardTiles[row].Count - 1)
                    {
                        if (
                            boardTiles[row][x - 1].Type == currentType &&
                            boardTiles[row][x].Type == currentType &&
                            boardTiles[row][x + 1].Type == currentType)
                        {
                            hasRowMatch = true;
                        }
                    }

                    if (y > 0 && y < boardTiles.Count - 1)
                    {
                        if (boardTiles[y - 1][column].Type == currentType &&
                            boardTiles[y][column].Type == currentType &&
                            boardTiles[y + 1][column].Type == currentType)
                        {
                            hasColumnMatch = true;
                        }
                    }

                    if(hasColumnMatch && hasRowMatch && (x == column && y == row)){
                        return true;
                    }
                }
            }
                        
            return false;
        }
    }
}
