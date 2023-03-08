using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A target detector is responsible of finding a targetable target. Currently, the nearest one from a core position.
/// </summary>
public class TargetDetector
{
    public event Action<ITarget> OnNewNearestEnemyFound; // Triggered anytime a new targetable enemy is found

    Vector3 _corePos; // The position from where a distance with targets is calculated

    List<ITargetProvider> _targetProviders; // Elements in charge of providing new targets

    List<ITarget> _detectedTargets = new List<ITarget>(); // Currently found targets that can be chosen to be the nearest ones

    ITarget nearestTarget = null; // The current nearest target


    public TargetDetector(Vector3 corePos, List<ITargetProvider> targetProviders)
    {
        this._corePos = corePos;
        this._targetProviders = targetProviders;

        // Listens to target providers to get new targets
        foreach (ITargetProvider targetProvider in _targetProviders)
        {
            targetProvider.OnNewTargetReady += TargetProvider_OnNewTargetReady;
        }
    }

    public void Disable()
    {
        // Stops from listening to target providers
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
    /// Finds the nearest targetable target 
    /// </summary>
    private void FindNearestTarget()
    {
        float minDistance = 99999;
        ITarget tempNearestEnemy = null;
        foreach(ITarget target in _detectedTargets)
        {
            if (!target.Targetable()) continue;

            float dist = Vector3.Distance(target.GetHitPosition(), _corePos);
            dist = Mathf.Abs(dist);
            if (dist < minDistance)
            {
                tempNearestEnemy = target;
                minDistance = dist;
            }
        }

        if (tempNearestEnemy != null && !tempNearestEnemy.Equals(nearestTarget))
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
        if (nearestTarget != null && nearestTarget.Targetable())
        {
            return true;
        }

        nearestTarget = null;
        FindNearestTarget();
        return nearestTarget != null;
    }
}
