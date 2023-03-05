using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DefenseManager : MonoBehaviour
{

    [Header("Defenses")]
    [SerializeField] List<Launcher> _leftDefenses; // They should be placed from the lowest to the highest

    [SerializeField] List<Launcher> _rightDefenses; // They should be placed from the lowest to the highest

    [Header("Blossom stats")]
    [SerializeField] Vector3 _finalDefensesScale;
    [SerializeField] float _bloomTime;


    [Header("Target Providers")]
    //[SerializeField] private List<ITargetProvider> _leftTargetProviders;
    //[SerializeField] private List<ITargetProvider> _rightTargetProviders;

    [SerializeField]
    private ScriptableObject[] _leftTargetProviders;

    private IEnumerable<ITargetProvider> LeftTargetProviders => _leftTargetProviders.OfType<ITargetProvider>();


    [SerializeField]
    private ScriptableObject[] _rightTargetProviders;

    private IEnumerable<ITargetProvider> RightTargetProviders => _rightTargetProviders.OfType<ITargetProvider>();


    private int _defesesBloomIndex = 0;

    private TargetDetector _leftTargetDetector;
    private TargetDetector _rightTargetDetector;

    private ITarget _leftTargetToShoot = null;
    private ITarget _rightTargetToShoot = null;

    private bool _enabled = true;

    private void Awake()
    {
        // Defenses scale is set to zero
        foreach (Launcher launcher in _leftDefenses)
        {
            launcher.transform.localScale = Vector3.zero;
        }

        foreach (Launcher launcher in _rightDefenses)
        {
            launcher.transform.localScale = Vector3.zero;
        }
    }


    private void Start()
    {
        _leftTargetDetector = new TargetDetector(transform.position, LeftTargetProviders.ToList());
        _rightTargetDetector = new TargetDetector(transform.position, RightTargetProviders.ToList());
        
        _leftTargetDetector.OnNewNearestEnemyFound += targetDetector_OnNewLeftNearestEnemyFound;
        _rightTargetDetector.OnNewNearestEnemyFound += targetDetector_OnNewRightNearestEnemyFound;

        NextDefensesBloom();
        GameManager.Instance.onGameOver += onGameOver;

        _enabled = true;
    }

    private void OnDisable()
    {
        _leftTargetDetector.Disable();
        _rightTargetDetector.Disable();

        _leftTargetDetector.OnNewNearestEnemyFound -= targetDetector_OnNewLeftNearestEnemyFound;
        _rightTargetDetector.OnNewNearestEnemyFound -= targetDetector_OnNewRightNearestEnemyFound;
    }

    private void Update()
    {
        if (!_enabled) return;

        _leftTargetDetector.Update();
        _rightTargetDetector.Update();
    }

    public bool TryGetTarget(Launcher requester, out ITarget target)
    {
        if (_leftDefenses.Contains(requester))
            return TryGetLeftTarget(out target);

        return TryGetRightTarget(out target);
    }


    private bool TryGetLeftTarget(out ITarget target)
    {
        if (_leftTargetToShoot.Targetable() || _leftTargetDetector.TryGetNewTarget())
        {
            target = _leftTargetToShoot;
            return true;
        }

        target = null;
        return false;
    }

    private bool TryGetRightTarget(out ITarget target)
    {
        if (_rightTargetToShoot.Targetable() || _rightTargetDetector.TryGetNewTarget())
        {
            target = _rightTargetToShoot;
            return true;
        }

        target = null;
        return false;
    }



    private void targetDetector_OnNewRightNearestEnemyFound(ITarget obj) 
    {
        _rightTargetToShoot = obj;

        // If launchers are waiting for a target, they should shoot it immediately if they can
        foreach (Launcher launcher in _rightDefenses)
        {
            launcher.NewTargetFound(_rightTargetToShoot);
        }
    }

    private void targetDetector_OnNewLeftNearestEnemyFound(ITarget obj) 
    {
        _leftTargetToShoot = obj;
        
        // If launchers are waiting for a target, they should shoot it immediately if they can
        foreach (Launcher launcher in _leftDefenses)
        {
            launcher.NewTargetFound(_leftTargetToShoot);
        }


    } 

    private void onGameOver()
    {
        foreach (Launcher launcher in _leftDefenses)
        {
            launcher.DisableDefense();
        }

        foreach (Launcher launcher in _rightDefenses)
        {
            launcher.DisableDefense();
        }



        _enabled = false;
    }

    public void NextDefensesBloom()
    {
        StartCoroutine(DefenseBloom(_leftDefenses[_defesesBloomIndex], _bloomTime));

        StartCoroutine(DefenseBloom(_rightDefenses[_defesesBloomIndex], _bloomTime));

        _defesesBloomIndex++;
    }

    public void IncreaseDefensesDamageBy(float percentage)
    {
        foreach (Launcher launcher in _leftDefenses)
        {
            launcher.IncrementDamageBy(percentage);
        }

        foreach (Launcher launcher in _rightDefenses)
        {
            launcher.IncrementDamageBy(percentage);
        }
    }

    public void IncreaseDefensesRateOfFireBy(float percentage)
    {
        foreach (Launcher launcher in _leftDefenses)
        {
            launcher.IncrementFireRateBy(percentage);
        }

        foreach (Launcher launcher in _rightDefenses)
        {
            launcher.IncrementFireRateBy(percentage);
        }
    }




    public int GetBloomIndex()
    {
        return _defesesBloomIndex;
    }

    #region COROUTINES

    private IEnumerator DefenseBloom(Launcher defenseToGrow, float timeToEnd)
    {
        Vector3 initialScale = defenseToGrow.transform.localScale;
        Vector3 finalScale = _finalDefensesScale;

        float elapsedTime = 0;
        while (elapsedTime < timeToEnd)
        {
            Vector3 newScale = Vector3.Slerp(initialScale, finalScale, elapsedTime / timeToEnd);
            defenseToGrow.transform.localScale = newScale;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        defenseToGrow.transform.localScale = finalScale;

        defenseToGrow.EnableDefense();
    }

    #endregion
}
