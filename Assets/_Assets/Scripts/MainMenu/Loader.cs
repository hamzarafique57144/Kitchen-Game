using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader 
{
    public enum Scene
    {
        MainMenuScene,
        GameScene,
        LoadingScene,
    }
    private static Scene targetScene; 
    public static int targetSceneIndex;
   public static void Load(Scene targerScene)
    {
        Loader.targetScene = targerScene;
        SceneManager.LoadScene(Loader.Scene.LoadingScene.ToString());
       
    }

    public static void LoaderCallBack()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
