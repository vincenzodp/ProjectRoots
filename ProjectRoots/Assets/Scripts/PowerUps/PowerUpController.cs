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

    [SerializeField] SpriteRenderer _powerUpSpriteRenderer;

    [Header("Animations")]
    [Header("Unlock")]
    [SerializeField] private Vector3 scaleIncrement;
    [SerializeField] private float timeToScale;
    [SerializeField] private float timeToFade;

    [SerializeField] private GameObject _vfxUnlockPrefab;

    private void Awake()
    {
        //FindObjectOfType<TreeRootManager>().onRootUnlocked += OnRootUnlocked;
        FindObjectOfType<GameManager>().onRootUnlocked += OnRootUnlocked;

        VisualizePowerUp();
    }


    private void VisualizePowerUp()
    {
        _powerUpSpriteRenderer.sprite = _powerUpData.sprite;
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

        StartCoroutine(ScaleNFade());

        Debug.Log($"Power Up unlocked: {_powerUpData.name}");
    }


    #region COROUTINES
    
    private IEnumerator ScaleNFade()
    {
        Vector3 initialScale = transform.localScale;
        Vector3 finalScale = initialScale + scaleIncrement;
        float elapsedTime = 0;

        Color initialColor = _powerUpSpriteRenderer.color;
        Color finalColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0);

        while (elapsedTime < timeToScale)
        {
            Vector3 newScale = Vector3.Lerp(initialScale, finalScale, elapsedTime / timeToScale);
            transform.localScale = newScale;

            Color newColor = Color.Lerp(initialColor, finalColor, elapsedTime / timeToScale);
            _powerUpSpriteRenderer.color = newColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //Color initialColor = _powerUpSpriteRenderer.color;
        //Color finalColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0);
        //elapsedTime = 0;

        //while (elapsedTime < timeToFade)
        //{
        //    Color newColor = Color.Lerp(initialColor, finalColor, elapsedTime / timeToFade);
        //    _powerUpSpriteRenderer.color = newColor;
        //    elapsedTime += Time.deltaTime;
        //    yield return null;
        //}
    }
    
    #endregion
}
