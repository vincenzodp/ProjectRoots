using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


[Singleton]
[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public float TreeEnergy { get; private set; }




    private DefenseManager _defenseManager;

    private void Awake()
    {
        _defenseManager = FindObjectOfType<DefenseManager>();
        if (_defenseManager == null)
            Debug.LogError("Defense Manager not found!");

        if (Instance == null)
        {
            Instance = this;
        }

        SubscribeToInstantiatedObjsEvents();
    }


    private void SubscribeToInstantiatedObjsEvents()
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
                break;

            case PowerUpsType.UPGRADE_DEFENSES_ATTACK:
                _defenseManager.IncreaseDefensesDamageBy(powerUpData.incrementValue);
                break;

            case PowerUpsType.UPGRADE_DEFENSE1:
                _defenseManager.EnableBaseDefenseUpgrade();
                break;

            case PowerUpsType.UPGRADE_DEFENSE2:
                _defenseManager.EnableHeavyDefenseUpgrade();
                break;

            case PowerUpsType.UPGRADE_DEFENSE3:
                _defenseManager.EnableLognshotDefenseUpgrade();
                break;
        }
    }
}
