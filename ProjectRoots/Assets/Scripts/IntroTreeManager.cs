using UnityEngine;

public class IntroTreeManager : MonoBehaviour
{
    [SerializeField] IntroUIManager UIManager;
    [SerializeField] ParticleSystem[] Fireworks;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var firework in Fireworks)
        {
            firework.gameObject.SetActive(true);
            firework.Play();
        }
    }

    public void OnAnimationEnd()
    {
        UIManager.LoadMainScene();
    }
}
