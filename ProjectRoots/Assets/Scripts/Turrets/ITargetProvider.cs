using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetProvider
{
    public event System.Action<ITarget> OnNewTargetReady;

    public void CommunicateNewTarget(ITarget target);
}
