using UnityEngine;
using UnityEngine.UI;

public class GameOverUIManager : MonoBehaviour
{
    [SerializeField] Button RetryButton;


    void Awake()
    {
        RetryButton.onClick.AddListener(OnRetryClick);
    }

    void OnRetryClick()
    {
        GameManager.Instance.PlayAgain();
    }
}
