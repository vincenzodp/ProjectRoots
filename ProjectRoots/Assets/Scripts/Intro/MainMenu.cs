using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    MeteorBehavior Meteor;

    public void StartButtonPressed()
    {
        SceneLoader.LoadMainSceneSuspended();
        Meteor.StartCrashingSequence(() =>
        {
            SceneLoader.ResumeSuspendedMainScene();
        });
    }

    public void CreditsButtonPressed()
    {
        SceneLoader.LoadCreditsScene();
    }

    public void QuitCreditsButtonPressed()
    {
        SceneLoader.LoadIntroScene();
    }

    public void QuitButtonPressed()
    {
        Application.Quit();
    }
}
