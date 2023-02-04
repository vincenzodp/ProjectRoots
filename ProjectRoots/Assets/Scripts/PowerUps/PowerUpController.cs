using System.Collections;
using System.Collections.Generic;
using System.Linq;
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


    private void Awake()
    {
        //FindObjectOfType<TreeRootManager>().onRootUnlocked += OnRootUnlocked;
        FindObjectOfType<GameManager>().onRootUnlocked += OnRootUnlocked;

    }

    /// <summary>
    /// Handles what should be done when a new root is unlocked
    /// </summary>
    /// <param name="treeRootNode"></param>
    private void OnRootUnlocked(TreeRootNode treeRootNode)
    {
        // if the root is one of the linked ones...
        if (_linkedRootNodes.Contains(treeRootNode))
        {
            // removes it from them
            _linkedRootNodes.Remove(treeRootNode);
            
            // if there are no other linked roots...
            if(_linkedRootNodes.Count <= 0)
            {
                Unlock();
            }
        }
    }

    /// <summary>
    /// Handles the unlock process of the power up
    /// </summary>
    public void Unlock()
    {
        onUnlocked?.Invoke(_powerUpData);
    }
}
