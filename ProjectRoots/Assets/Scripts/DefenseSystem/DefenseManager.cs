using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseManager : MonoBehaviour
{

    [Header("Defenses")]
    [SerializeField] List<Launcher> _leftDefenses; // They should be placed from the lowest to the highest

    [SerializeField] List<Launcher> _rightDefenses; // They should be placed from the lowest to the highest

    [Header("Colliders")]
    [SerializeField] EnemyCollisionDetector _leftSideCollisionDetector;
    [SerializeField] EnemyCollisionDetector _rightSideCollisionDetector;

    [Header("Blossom stats")]
    [SerializeField] Vector3 _finalDefensesScale;
    [SerializeField] float _bloomTime;

    [Header("Defenses Stats")]
    [SerializeField] private float _fireRate;
    [SerializeField] private float _damage;


    private int _defesesBloomIndex = 0;

    private List<Enemy> _leftEnemiesQueue = new List<Enemy>();
    private List<Enemy> _rightEnemiesQueue = new List<Enemy>();

    private float _elapsedTime = 0;

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

        _leftSideCollisionDetector.onNewEnemyEntered += newEnemyFoundAtLeft;
        _rightSideCollisionDetector.onNewEnemyEntered += newEnemyFoundAtRight;


    }


    private void Start()
    {
        NextDefensesBloom();
        GameManager.Instance.onGameOver += onGameOver;
    }

    private void Update()
    {
        //if (_elapsedTime >= _fireRate)
        //{
        //    _elapsedTime = 0;
        //    Shoot();
        //}
        //else
        //{
        //    _elapsedTime += Time.deltaTime;
        //}
    }

    //private void Shoot()
    //{
    //    foreach (Launcher launcher in _leftDefenses)
    //    {
    //        launcher.Shoot(_damage);
    //    }

    //    foreach (Launcher launcher in _rightDefenses)
    //    {
    //        launcher.Shoot(_damage);
    //    }
    //}

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
    }

    private void newEnemyFoundAtLeft(Enemy enemy)
    {
        foreach (Launcher launcher in _leftDefenses)
        {
            launcher.EnemyDetected(enemy);
            //launcher.AssignTarget(enemy);

        }

        //_leftEnemiesQueue.Add(enemy);


    }

    private void newEnemyFoundAtRight(Enemy enemy)
    {
        foreach (Launcher launcher in _rightDefenses)
        {
            launcher.EnemyDetected(enemy);
            //launcher.AssignTarget(enemy);
        }

        //_rightEnemiesQueue.Add(enemy);
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
            //_damage += (_damage * percentage / 100);
        }

        foreach (Launcher launcher in _rightDefenses)
        {
            launcher.IncrementDamageBy(percentage);
            //_fireRate += (_fireRate * percentage / 100);

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

    private void OnDisable()
    {
        _leftSideCollisionDetector.onNewEnemyEntered -= newEnemyFoundAtLeft;
        _rightSideCollisionDetector.onNewEnemyEntered -= newEnemyFoundAtRight;
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
