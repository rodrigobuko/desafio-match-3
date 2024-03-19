using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gazeus.DesafioMatch3.Core
{
    public class SceneService 
    {
        GameScenes _gameScenes;

        public SceneService(GameScenes gameScenes){
            _gameScenes = gameScenes;
        }

        public void LoadMainMenuScene()
        {
            SceneManager.LoadScene(_gameScenes.MainMenuScene);
        }

        public void LoadGameScene()
        {
            SceneManager.LoadScene(_gameScenes.GameScene);
        }
    }
}
