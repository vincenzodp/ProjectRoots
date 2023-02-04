using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    [Header("Growth")]
    [SerializeField] private Vector3 _growthIncrementValue;
    [SerializeField] private float _growthTime;


    public void Grow()
    {
        StartCoroutine(Growth());
    }

    #region COROUTINES

    private IEnumerator Growth()
    {
        Vector3 initialScale = transform.localScale;
        Vector3 finalScale = initialScale + _growthIncrementValue;

        float elapsedTime = 0;
        while(elapsedTime < _growthTime)
        {
            Vector3 newScale = Vector3.Slerp(initialScale, finalScale, elapsedTime / _growthTime);
            transform.localScale = newScale;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = finalScale;

    }

    #endregion
}
