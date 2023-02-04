using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


[Singleton]
[DisallowMultipleComponent]
[RequireComponent(typeof(EnergyRefiller))]
public class GameManager : MonoBehaviour
{
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
        Debug.Log("MORTE!!!");
        GameOver();

    }

    private void GameOver()
    {
        onGameOver?.Invoke();
    }

    private void OnDestroy()
    {
        EnergyRefiller.Instance.OnValueBelowZeroOnce -= EnergyBelowZeroEmitted;
    }
}
