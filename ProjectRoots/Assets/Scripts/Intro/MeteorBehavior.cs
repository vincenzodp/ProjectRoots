using System;
using System.Collections;
using UnityEngine;

public class MeteorBehavior : MonoBehaviour
{
    [SerializeField]
    Transform TargetToHit;

    [SerializeField]
    Transform Planet;

    [SerializeField]
    float SpeedIncrease;

    [SerializeField]
    GameObject ExplosionPrefab;


    float currentAccelleration;
    bool canMove = false;
    Action onSequenceEndCallback;


    void Start()
    {
        transform.LookAt(TargetToHit);
    }

    void Update()
    {
        if (!canMove)
            return;

        currentAccelleration += SpeedIncrease * Time.deltaTime;
        Vector3 accellerationVector = transform.forward * currentAccelleration;

        transform.position += accellerationVector;
    }

    public void StartCrashingSequence(Action OnSequenceEnd)
    {
        canMove = true;
        onSequenceEndCallback = OnSequenceEnd;
    }

    void OnCollisionEnter(Collision collision)
    {
        var contact = collision.GetContact(0);
        var hitRotation = Quaternion.LookRotation(contact.normal);
        var instance = Instantiate(ExplosionPrefab, contact.point, hitRotation);
        instance.transform.parent = Planet.transform;

        StartCoroutine(WaitForSequenceEnd());
    }

    IEnumerator WaitForSequenceEnd()
    {
        yield return new WaitForSeconds(4f);
        onSequenceEndCallback?.Invoke();
    }
}
