using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected Transform _destinationTarget;

    [SerializeField] protected float _baseDamage;

    [SerializeField] protected float _speed;

    protected Transform _spawnerTransform;
    
    protected float _totalDamage;
    
    protected bool _canStart = false;
    
    protected float _startTime;

    public void Initialize(Transform spawnerTransform, Transform destinationTarget, float damageIncrement)
    {
        _destinationTarget = destinationTarget;

        _spawnerTransform = spawnerTransform;

        _totalDamage = _baseDamage + (_baseDamage * damageIncrement / 100);

        _canStart = true;

        _startTime = Time.time;
    
    }

    private void Update()
    {
        if (_destinationTarget == null)
        {
            return;
        }

        if (!_canStart) return;

        ApplyMovement();

        if(Vector3.Distance(transform.position, _destinationTarget.position) < .25f)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void ApplyMovement()
    {
        if(_destinationTarget == null)
        {
            return;
        }

        Vector3 center = (_spawnerTransform.position + _destinationTarget.position) * 0.5F;


        if (_spawnerTransform.position.x < 0)
        {
            center -= new Vector3(0, 9, 0);
        }
        else
        {
            center -= new Vector3(0, 12, 0);
        }


        Vector3 riseRelCenter = _spawnerTransform.position + Vector3.up * 2 - center;
        Vector3 setRelCenter = _destinationTarget.position + Vector3.up - center;

        float fracComplete = (Time.time - _startTime) * _speed /*/ 150f*/;

        transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
        transform.position += center;

        Vector3 relativePos = _destinationTarget.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotation;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag.Equals("Enemy"))
        {
            collider.gameObject.GetComponent<Enemy>().HitByProjectile();
            Destroy(gameObject);
        }
    }
}
