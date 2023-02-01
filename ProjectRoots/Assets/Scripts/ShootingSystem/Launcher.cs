using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField] protected GameObject _projectilePrefab;

    [SerializeField] protected Transform _shootingTarget;

    [SerializeField] protected float _intervalBetweenShots = 2f;

    [SerializeField] protected float _shootingRange;

    [SerializeField] protected float _damageIncrement;

    private float _elapsedTime;

    private bool _canShoot = false;

    public void StartShooting()
    {
        _canShoot = true;
    }

    private void Shoot()
    {
        Instantiate(_projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>().Initialize(transform, _shootingTarget, _damageIncrement);
    }

    private void Update()
    {
        if (!_canShoot) return;

        if(_intervalBetweenShots <= _elapsedTime)
        {
            _elapsedTime = 0;
            Shoot();
        }
        else
        {
            _elapsedTime += Time.deltaTime;
        }
    }

}
