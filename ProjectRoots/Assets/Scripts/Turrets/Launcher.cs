using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField] protected GameObject _projectilePrefab;


    [Header("Defense stats")]

    [SerializeField] protected float _fireRate = 2f;

    [SerializeField] protected float _damage;

    [SerializeField] protected float _shootRange;

    [Header("Projectile Spawn")]
    [SerializeField] private Transform _projectileSpawner;

    [HideInInspector] public bool Disabled { get { return _disabled; } set { _disabled = value; } }

    private float _elapsedTime;

    private bool _disabled = true;

    private bool _isWaitingForTarget = true;

    private ITarget _targetToShootWhenInRange = null;

    private DefenseManager _defenseManager;
    private void Start()
    {
        _defenseManager = FindObjectOfType<DefenseManager>();
    }


    private void AskToShoot()
    {
        if(_defenseManager.TryGetTarget(this, out ITarget target))
        {
            if(TargetIsInRange(target))
            {
                // Applies the damage to the target before the projectile hits it
                target.ApplyDamage(_damage);

                // Instantiates and starts the projectile
                Projectile projectile = Instantiate(_projectilePrefab, _projectileSpawner.localPosition, Quaternion.identity, transform).GetComponent<Projectile>();
                projectile.StartMovement(target);

                    //.GetComponent<Projectile>().Initialize(_projectileSpawner, target.transform, _damage);


                // If a target is going to be shot, the launher will no longer wait for a target (until a new one can't be found) 
                _isWaitingForTarget = false;
                
                return;
            }
        }

        _isWaitingForTarget = true;
    }

    public void TryToShootImmediately(ITarget target)
    {
        if (target.Targetable())
        {
            // Applies the damage to the target before the projectile hits it
            target.ApplyDamage(_damage);

            // Instantiates and starts the projectile
            Projectile projectile = Instantiate(_projectilePrefab, _projectileSpawner.localPosition, Quaternion.identity, transform).GetComponent<Projectile>();
            projectile.StartMovement(target);

            // If a target is going to be shot, the launher will no longer wait for a target (until a new one can't be found) 
            _isWaitingForTarget = false;

        }
    }

    private bool TargetIsInRange(ITarget target)
    {
        return Vector3.Distance(target.GetHitPosition(), transform.position) <= _shootRange;
    }

    private void Update()
    {
        if (_disabled) return;

        if (_isWaitingForTarget)
        {
            if(_targetToShootWhenInRange != null)
            {
                if (TargetIsInRange(_targetToShootWhenInRange))
                {
                    TryToShootImmediately(_targetToShootWhenInRange);
                    _targetToShootWhenInRange = null;
                    
                }
            }
        }
        else
        {
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

    internal void NewTargetFound(ITarget targetToShoot)
    {
        if (_disabled || !_isWaitingForTarget) return;

        if(TargetIsInRange(targetToShoot))
        {
            TryToShootImmediately(targetToShoot);
        }
        else
        {
            _targetToShootWhenInRange = targetToShoot;
        }

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
