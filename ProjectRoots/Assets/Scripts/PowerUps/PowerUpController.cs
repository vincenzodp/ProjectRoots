using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A controller that handles the core actions doable to or done by a powerup
/// </summary>
public class PowerUpController : MonoBehaviour
{
    ///A one parameter delegate function
    public delegate void PowerUpUnlocked(PowerUpData powerUpData);

    public event PowerUpUnlocked onUnlocked;

    [Tooltip("Required Data to start from")]
    [SerializeField] private PowerUpData _powerUpData;

    [Tooltip("The RootNodes to unlock to use this power up")]
    [SerializeField] List<TreeRootNode> _linkedRootNodes;

    
    /// <summary>
    /// Handles the unlock process of the power up
    /// </summary>
    public void Unlock()
    {
        onUnlocked?.Invoke(_powerUpData);
    }
}
