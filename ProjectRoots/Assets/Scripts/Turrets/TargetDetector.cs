using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetector
{
    public event Action<ITarget> OnNewNearestEnemyFound;

    Vector3 _corePos;

    List<ITargetProvider> _targetProviders;

    List<ITarget> _detectedTargets = new List<ITarget>();

    ITarget nearestTarget = null;


    public TargetDetector(Vector3 corePos, List<ITargetProvider> targetProviders)
    {
        this._corePos = corePos;
        this._targetProviders = targetProviders;

        foreach (ITargetProvider targetProvider in _targetProviders)
        {
            targetProvider.OnNewTargetReady += TargetProvider_OnNewTargetReady;
        }
    }

    public void Disable()
    {
        foreach (ITargetProvider targetProvider in _targetProviders)
        {
            targetProvider.OnNewTargetReady -= TargetProvider_OnNewTargetReady;
        }
    }

    /// What should be done when a target provider generates a target
    private void TargetProvider_OnNewTargetReady(ITarget obj)
    {
        // Adds the generated target to the detected list
        _detectedTargets.Add(obj);

        obj.OnTargetDestroy += Target_OnTargetDestroy;
    }

    /// What should be done when a target is destroyed
    private void Target_OnTargetDestroy(ITarget obj)
    {
        obj.OnTargetDestroy -= Target_OnTargetDestroy;
        _detectedTargets.Remove(obj);
    }

    public void Update()
    {
        FindNearestTarget();
    }

    /// <summary>
    /// Finds the nearest tagetable target 
    /// </summary>
    private void FindNearestTarget()
    {
        float minDistance = 99999;
        ITarget tempNearestEnemy = null;
        foreach(ITarget target in _detectedTargets)
        {
            if (!target.Targetable()) continue;

            float dist = Vector3.Distance(target.GetHitPosition(), _corePos);
            if (dist < minDistance)
            {
                tempNearestEnemy = target;
                minDistance = dist;
            }
        }

        if (!tempNearestEnemy.Equals(nearestTarget))
        {
            nearestTarget = tempNearestEnemy;
            OnNewNearestEnemyFound?.Invoke(nearestTarget);
        }
    }

    /// <summary>
    /// Tries to find a new nearest addressable target 
    /// </summary>
    /// <returns></returns>
    public bool TryGetNewTarget()
    {
        if (nearestTarget != null || nearestTarget.Targetable())
        {
            return true;
        }

        FindNearestTarget();
        return nearestTarget != null;
    }
}
