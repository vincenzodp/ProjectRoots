using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// A Launcher component asks for and shoots to a target
/// </summary>
public class Launcher : MonoBehaviour
{
    [SerializeField] protected GameObject _projectilePrefab;


    //[Header("Defense stats")]
    [SerializeField] LauncherData _launcherData;


    [Header("Projectile Spawn")]
    [SerializeField] private Transform _projectileSpawner;

    [HideInInspector] public bool Disabled { get { return _disabled; } set { _disabled = value; } }

    private float _elapsedTime;

    private bool _disabled = true;

    private bool _isWaitingForTarget = true;

    private DefenseManager _defenseManager;


    private float _fireRate;
    private float _damage;
    private float _shootRange;

    private void Start()
    {
        _defenseManager = FindObjectOfType<DefenseManager>();

        _damage = _launcherData.Damage;
        _fireRate = _launcherData.FireRate;
        _shootRange = _launcherData.ShootRange;

    }


    // Asks to the defense manager for a new shootable target
    private void AskToShoot()
    {
        if(_defenseManager.TryGetTarget(this, out ITarget target))
        {
            if(TargetIsInRange(target))
            {
                // Applies the damage to the target before the projectile hits it
                target.ApplyDamage(_damage);

                // Instantiates and starts the projectile
                Projectile projectile = Instantiate(_projectilePrefab, _projectileSpawner.position, Quaternion.identity, transform).GetComponent<Projectile>();
                projectile.StartMovement(target);

                    //.GetComponent<Projectile>().Initialize(_projectileSpawner, target.transform, _damage);


                // If a target is going to be shot, the launher will no longer wait for a target (until a new one can't be found) 
                _isWaitingForTarget = false;
                
                return;
            }
        }

        _isWaitingForTarget = true;
    }


    // Checks if a target is in range considering its shoot range
    private bool TargetIsInRange(ITarget target)
    {
        return Vector3.Distance(target.GetHitPosition(), transform.position) <= _shootRange;
    }

    private void Update()
    {
        if (_disabled) return;

        // If a target has not been chosen yet, it asks for it every frame
        if (_isWaitingForTarget)
        {
            AskToShoot();
        }
        else
        {
            // Otherwise, it respects its fire ratio

            if (_elapsedTime >= _fireRate)
            {
                _elapsedTime = 0;
                AskToShoot();
            }
            else
            {
                _elapsedTime += Time.deltaTime;
            }
        }


    }

    public void DisableDefense()
    {
        _disabled = true;
    }

    public void EnableDefense()
    {
        _disabled = false;

    }


    public void IncrementDamageBy(float percentage)
    {
        _damage += (_damage * percentage / 100);
    }

    public void IncrementFireRateBy(float percentage)
    {
        _fireRate += (_fireRate * percentage / 100);
    }

    public float GetDamage()
    {
        return _damage;
    }

}
