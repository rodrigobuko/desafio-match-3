using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gazeus.DesafioMatch3.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SceneRepository", menuName = "Game/SceneRepository")]
    public class SceneRepository : ScriptableObject
    {
        [SerializeField] private string _gameSceneName;
        [SerializeField] private string _mainMenuSceneName;

        public  GameScenes GetGameScenes()
        {
            return new GameScenes
            {
                MainMenuScene = _mainMenuSceneName,
                GameScene = _gameSceneName,
            };
        }
    }
}
