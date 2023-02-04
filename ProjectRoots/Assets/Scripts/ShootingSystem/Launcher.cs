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

    [Header("Projectile Spawn")]
    [SerializeField] private Transform _projectileSpawner;

    [Header("DEBUG ONLY")]

    [SerializeField] protected Enemy _shootingTarget;

    [SerializeField] private List<Enemy> _enemiesQueue;  

    private float _elapsedTime;

    private void Shoot()
    {
        Instantiate(_projectilePrefab, _projectileSpawner.localPosition, Quaternion.identity, transform).GetComponent<Projectile>().Initialize(_projectileSpawner, _shootingTarget.transform, _damage);
        
        if((_shootingTarget.getHealth() - _damage) <= 0)
        {
            UpdateTarget();
        }
    }


    private void Update()
    {
        if (_shootingTarget == null) return;

        if(_elapsedTime >= _fireRate)
        {
            _elapsedTime = 0;
            Shoot();
        }
        else
        {
            _elapsedTime += Time.deltaTime;
        }
    }

    /// <summary>
    /// Updates the target of the defense
    /// </summary>
    private void UpdateTarget() 
    {
        //Checks if there are no enemies in queue
        if(_enemiesQueue.Count == 0)
        {
            _shootingTarget = null;
            _elapsedTime = 0;
            return;
        }

        //By default, the first enemy of the queue will be the next target
        Enemy nextTarget = _enemiesQueue.First();

        float shortestDistanceFromNextTarget = Vector3.Distance(transform.position, nextTarget.transform.position);

        // Checks if there are nearer enemies to the defense, if so, updates the next target 
        foreach(Enemy enemyInQueue in _enemiesQueue)
        {
            float temp = Vector3.Distance(transform.position, enemyInQueue.transform.position);
            if (temp < shortestDistanceFromNextTarget)
            {
                nextTarget = enemyInQueue;
                shortestDistanceFromNextTarget = temp;
            }
        }

        _shootingTarget = nextTarget;
        _enemiesQueue.Remove(nextTarget);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy potentialTarget = other.GetComponent<Enemy>();

            // if there is no target to shoot, the new entered enemy will be the target
            // else, it will be added to the queue, from where will be chosen in future
            if(_shootingTarget == null)
            {
                _shootingTarget = potentialTarget;
            }
            else
            {
                _enemiesQueue.Add(potentialTarget);
            }
        }
    }

}
