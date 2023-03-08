using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible of spawning a target
/// </summary>
public class SpawnPoint : MonoBehaviour, ITargetProvider
{
    public event Action<ITarget> OnNewTargetReady;



    public void CommunicateNewTarget(ITarget target)
    {
        OnNewTargetReady?.Invoke(target);
    }

    /// <summary>
    /// Spawns a new target on scene
    /// </summary>
    /// <param name="prefab"></param>
    public void SpawnTarget(GameObject prefab)
    {
        ITarget target = Instantiate(prefab, transform.position, Quaternion.identity).GetComponent<ITarget>();
        CommunicateNewTarget(target);
    }

}
