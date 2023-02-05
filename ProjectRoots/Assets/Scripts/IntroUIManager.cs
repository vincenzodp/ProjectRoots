using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroUIManager : MonoBehaviour
{
    [SerializeField] Button PlayButton;
    [SerializeField] Transform UIContainer;
    [SerializeField] Transform Meteor;
    [SerializeField] Transform Tree;

    AsyncOperation loadingScene;

    void Start()
    {
        Tree.gameObject.SetActive(false);
        Meteor.gameObject.SetActive(false);
        PlayButton.onClick.AddListener(OnPlayClick);

        // start loading the main scene
        loadingScene = SceneManager.LoadSceneAsync(1);
        loadingScene.allowSceneActivation = false;
    }

    void OnPlayClick()
    {
        // 1 Start Animation
        UIContainer.gameObject.SetActive(false);

        // 2 Start Animation
        Meteor.gameObject.SetActive(true);
    }

    public void GrowTree()
    {
        Tree.gameObject.SetActive(true);
    }

    public void LoadMainScene()
    {
        loadingScene.allowSceneActivation = true;
    }
}
