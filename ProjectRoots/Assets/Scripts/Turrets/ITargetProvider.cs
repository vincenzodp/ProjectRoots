using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A target provider is responsible of communicating a new generated target ready to be chosen
/// </summary>
public interface ITargetProvider
{
    public event System.Action<ITarget> OnNewTargetReady;

    public void CommunicateNewTarget(ITarget target);
}
