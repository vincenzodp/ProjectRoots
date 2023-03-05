using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float _speed;

    [SerializeField] ParticleSystem hitVFX;
    
    protected float _totalDamage;
    
    protected float _startTime;

    protected Vector3 _startingPos;

    protected Vector3 _destinationPos;

    protected ITarget _target;

    private void Start()
    {
        _startingPos = transform.position;
    }

    public void StartMovement(ITarget target)
    {

        _target = target;

        _startTime = Time.time;

        // Destroys the projectile after 8 seconds
        Invoke("OnDestroy", 8f);
    }

    private void Update()
    {
        if (_target == null) return;
        
        ApplyMovement();

        CheckIfTargetWasHit();
    }

    protected virtual void CheckIfTargetWasHit()
    {
        if(Vector3.Distance(_target.GetHitPosition(), transform.position) < .2f)
        {
            _target.Hit();
            Instantiate(hitVFX, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    protected virtual void ApplyMovement()
    {
        Vector3 targetPos = _target.GetHitPosition();

        Vector3 center = (_startingPos + targetPos) * 0.9F;

        center -= new Vector3(0, 12, 0);

        Vector3 riseRelCenter = _startingPos  - center;
        Vector3 setRelCenter = targetPos - center;

        float fracComplete = (Time.time - _startTime) * _speed;

        transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
        transform.position += center;

        Vector3 relativePos = targetPos - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotation;
    }

    //void OnTriggerEnter(Collider collider)
    //{
    //    if (collider.gameObject.CompareTag("Enemy") && (collider.GetComponent<ITarget>() != null && collider.GetComponent<ITarget>() == _target))
    //    {
    //        collider.gameObject.GetComponent<Enemy>().HitByProjectile(_totalDamage);


    //        Instantiate(hitVFX, gameObject.transform.position, Quaternion.identity);
            

            //Instantiate(hitVFX, gameObject.transform.position, Quaternion.identity);

            //collider.gameObject.GetComponent<Enemy>().CreateFloatingDamageFeedbackText(this.transform.position, (int)_totalDamage);

            //collider.gameObject.GetComponent<AudioSource>().Play();


    //        Destroy(gameObject);
    //    }
    //}
}
