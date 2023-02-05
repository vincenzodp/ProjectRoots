using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[Singleton]
[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{
    [SerializeField] Transform damageSpawnPoint;
    [SerializeField] GameObject damageFeedbackText;

    [SerializeField] GameObject gameOverUI;

    public delegate void OnRootunlocked(TreeRootNode treeRootNodeUnlocked);
    public event OnRootunlocked onRootUnlocked;

    public delegate void OnGameOver();
    public event OnGameOver onGameOver;

    public static GameManager Instance { get; private set; }

    //Handful sub-systems references
    private DefenseManager _defenseManager;
    private TreeController _treeController;

    private void Awake()
    {
        _defenseManager = FindObjectOfType<DefenseManager>();
        if (_defenseManager == null)
            Debug.LogError("Defense Manager not found!");

        _treeController = FindObjectOfType<TreeController>();
        if (_treeController == null)
            Debug.LogError("Tree Controller not found!");

        if (Instance == null)
        {
            Instance = this;
        }

        SubscribeToInstantiatedObjsEvents();
        EnergyRefiller.Instance.OnValueBelowZeroOnce += EnergyBelowZeroEmitted;
    }

    void SubscribeToInstantiatedObjsEvents()
    {
        FindObjectsOfType<PowerUpController>().ToList().ForEach(puc => { puc.onUnlocked += OnPowerUpUnlocked; });
    }

    /// <summary>
    /// Unlocked Power Up handler
    /// </summary>
    /// <param name="powerUpData"></param>
    private void OnPowerUpUnlocked(PowerUpData powerUpData)
    {
        switch (powerUpData.type)
        {
            case PowerUpsType.UPGRADE_TREE:
                _treeController.Grow();
                _defenseManager.NextDefensesBloom();
                break;

            case PowerUpsType.UPGRADE_DEFENSES_ATTACK:
                _defenseManager.IncreaseDefensesDamageBy(powerUpData.incrementValue);
                break;

            case PowerUpsType.UPGRADE_DEFENSES_RATE_OF_FIRE:
                _defenseManager.IncreaseDefensesRateOfFireBy(powerUpData.incrementValue);
                break;
        }
    }

    public void NewRootNodeBought(TreeRootNode unlockedRootNode)
    {
        onRootUnlocked?.Invoke(unlockedRootNode);

    }

    void EnergyBelowZeroEmitted()
    {
        GameOver();
    }

    private void GameOver()
    {
        Instantiate(gameOverUI, Vector3.zero, gameOverUI.transform.rotation);
        onGameOver?.Invoke();
    }

    private void OnDestroy()
    {
        EnergyRefiller.Instance.OnValueBelowZeroOnce -= EnergyBelowZeroEmitted;
    }

    public void DisplayDamage(float damage)
    {
        var newPosition = damageSpawnPoint.transform.position;
        var feedback = Instantiate(damageFeedbackText, newPosition, damageFeedbackText.transform.rotation);
        feedback.GetComponent<FloatingDamageFeedbackManager>().DisplayDamage(damage);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }
}
