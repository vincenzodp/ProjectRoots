using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class DefenseManager : MonoBehaviour
{

    [Header("Defenses")]
    [SerializeField] List<Launcher> _leftDefenses; // They should be placed from the lowest to the highest

    [SerializeField] List<Launcher> _rightDefenses; // They should be placed from the lowest to the highest

    [Header("Blossom stats")]
    [SerializeField] Vector3 _finalDefensesScale; // The scale that defenses will reach at blooming time
    [SerializeField] float _bloomTime; // How much time should bloom take?


    [Header("Target Providers")]

    [RequireInterface(typeof(ITargetProvider))]
    public Object leftTargetProvider;


    [RequireInterface(typeof(ITargetProvider))]
    public Object rightTargetProvider;



    private int _defesesBloomIndex = 0; // Up to 3

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
        // Creating instances of Target Detectors 
        List<ITargetProvider> leftTargetProviders = new List<ITargetProvider>();
        leftTargetProviders.Add(leftTargetProvider as ITargetProvider);

        List<ITargetProvider> rightTargetProviders = new List<ITargetProvider>();
        rightTargetProviders.Add(rightTargetProvider as ITargetProvider);


        _leftTargetDetector = new TargetDetector(transform.position, leftTargetProviders);
        _rightTargetDetector = new TargetDetector(transform.position, rightTargetProviders);
        
        // Subscribing to relevant events
        _leftTargetDetector.OnNewNearestEnemyFound += targetDetector_OnNewLeftNearestEnemyFound;
        _rightTargetDetector.OnNewNearestEnemyFound += targetDetector_OnNewRightNearestEnemyFound;

        // Let the first row of defences grow
        NextDefensesBloom();

        //Subscribes to Game Manager relevant events
        //GameManager.Instance.onGameOver += onGameOver;

        // now the manager is ready to work
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

        // If enabled, also the detectors should work
        _leftTargetDetector.Update();
        _rightTargetDetector.Update();
    }

    /// <summary>
    /// Tries to provide a target to a requester launcher
    /// </summary>
    /// <param name="requester"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public bool TryGetTarget(Launcher requester, out ITarget target)
    {
        // It needs to know the side of the requester
         
        if (_leftDefenses.Contains(requester))
            return TryGetLeftTarget(out target);

        return TryGetRightTarget(out target);
    }

    // Real left target getter method
    private bool TryGetLeftTarget(out ITarget target)
    {
        // First checks if the currently target to shoot is not null and it is tagetable
        // If not, asks the detector to find a get a new target
        if ((_leftTargetToShoot != null && _leftTargetToShoot.Targetable()) || _leftTargetDetector.TryGetNewTarget())
        {
            // if found, the function returs true and the target
            target = _leftTargetToShoot;
            return true;
        }

        target = null;
        return false;
    }

    // Real right target getter method
    private bool TryGetRightTarget(out ITarget target)
    {
        // First checks if the currently target to shoot is not null and it is tagetable
        // If not, asks the detector to find a get a new target
        if ((_rightTargetToShoot != null && _rightTargetToShoot.Targetable()) || _rightTargetDetector.TryGetNewTarget())
        {
            // if found, the function returs true and the target
            target = _rightTargetToShoot;
            return true;
        }

        target = null;
        return false;
    }


    // Everytime a new nearest enemy is found at right, it becomes the new target
    private void targetDetector_OnNewRightNearestEnemyFound(ITarget obj) 
    {
        _rightTargetToShoot = obj;

        // NOT CURRENTLY USED
        // If launchers are waiting for a target, they should shoot it immediately if they can 
        //foreach (Launcher launcher in _rightDefenses)
        //{
        //    launcher.NewTargetFound(_rightTargetToShoot);
        //}
    }

    // Everytime a new nearest enemy is found at left, it becomes the new target
    private void targetDetector_OnNewLeftNearestEnemyFound(ITarget obj) 
    {
        _leftTargetToShoot = obj;

        // NOT CURRENTLY USED
        // If launchers are waiting for a target, they should shoot it immediately if they can
        //foreach (Launcher launcher in _leftDefenses)
        //{
        //    launcher.NewTargetFound(_leftTargetToShoot);
        //}


    }

    // What should happen when game ends
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

   /// <summary>
   /// Let a new row of defences grow
   /// </summary>
    public void NextDefensesBloom()
    {
        StartCoroutine(DefenseBloom(_leftDefenses[_defesesBloomIndex], _bloomTime));

        StartCoroutine(DefenseBloom(_rightDefenses[_defesesBloomIndex], _bloomTime));

        _defesesBloomIndex++;
    }

    private void BloomTurret(int turretIndex, bool isLeft)
    {
        if (isLeft)
        {
            StartCoroutine(DefenseBloom(_leftDefenses[turretIndex], _bloomTime));
        }
        else
        {
            StartCoroutine(DefenseBloom(_rightDefenses[turretIndex], _bloomTime));
        }
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
