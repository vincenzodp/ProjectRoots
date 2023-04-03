using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    enum SceneBuildIndexes
    {
        IntroScene = 0,
        MainScene = 1,
        CreditsScene = 2,
    }

    static AsyncOperation loadingScene;

    public static void LoadMainSceneSuspended()
    {
        loadingScene = SceneManager.LoadSceneAsync((int)SceneBuildIndexes.MainScene);
        loadingScene.allowSceneActivation = false;
    }
    public static void ResumeSuspendedMainScene()
    {
        loadingScene.allowSceneActivation = true;
    }
    public static void ReloadMainScene()
    {
        SceneManager.LoadScene((int)SceneBuildIndexes.MainScene);
    }

    public static void LoadIntroScene()
    {
        SceneManager.LoadScene((int)SceneBuildIndexes.IntroScene);
    }

    public static void LoadCreditsScene()
    {
        SceneManager.LoadScene((int)SceneBuildIndexes.CreditsScene);
    }
}

