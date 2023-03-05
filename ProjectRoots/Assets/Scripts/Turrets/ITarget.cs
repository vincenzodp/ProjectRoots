using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Anything that can be targeted by the shooting system
/// </summary>
public interface ITarget
{
    // The target should invoke this event when it is about to leave the scene
    public event System.Action<ITarget> OnTargetDestroy;

    /// <summary>
    /// Handles the way a target should apply damage to itself
    /// </summary>
    /// <param name="damage"></param>
    public void ApplyDamage(float damage);

    /// <summary>
    /// Handles what should happen when the target has been hit
    /// </summary>
    public void Hit();

    /// <summary>
    /// Checks whether or not a target can be targeted
    /// </summary>
    /// <returns></returns>
    public bool Targetable();

    /// <summary>
    /// Returs the spatial position of the target
    /// </summary>
    /// <returns></returns>
    public Vector3 GetHitPosition();

}
