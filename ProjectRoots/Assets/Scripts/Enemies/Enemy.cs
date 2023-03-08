using System;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, ITarget, IEquatable<Enemy>
{

    [SerializeField] private float _baseHealth;
    [SerializeField] private float _baseSpeed;

    [SerializeField] private float _currentHealth;
    [SerializeField] private float _currentSpeed;

    public event Action<ITarget> OnTargetDestroy;


    [HideInInspector] public AudioSource hitAudioSource;
    public GameObject floatingDamageFeedbackText;

    private CapsuleCollider _capsuleCollider;

    //public Gradient gradient;
    //public Image fill;

    private void Awake()
    {
        _currentSpeed = _baseSpeed;
        _currentHealth = _baseHealth;
        hitAudioSource = GetComponent<AudioSource>();
        _capsuleCollider = GetComponent<CapsuleCollider>();

        if (GameManager.Instance == null)
        {
            return;
        }

        GameManager.Instance.onGameOver += onGameOver;
    }

    public void HitByProjectile()
    {
        GetComponent<HealthBar>().SetHealthBarHealth(_currentHealth);
        
        if (_currentHealth <= 0)
        {
            GetComponent<EnemyMovement>().stopmoving();
            GetComponent<EnemyDeath>().TriggerDeath();
        }
    }

    public float getHealth()
    {
        return _currentHealth;
    }

    public void DestroyEnemyEvent()
    {
        Destroy(gameObject);
    }

    private void onGameOver()
    {
        GetComponent<EnemyMovement>().stopmoving();
        GetComponent<EnemyVictory>().TriggerVictory();
    }

    private void OnDisable()
    {
        if (GameManager.Instance == null) return;
        GameManager.Instance.onGameOver -= onGameOver;
    }

    private void OnDestroy()
    {
        OnTargetDestroy?.Invoke(this);
    }


    public void CreateFloatingDamageFeedbackText(Vector3 position, int damage)
    {
        var newFloatingText = Instantiate(floatingDamageFeedbackText, position, floatingDamageFeedbackText.transform.rotation);
        var floatingTextManager = newFloatingText.GetComponentInChildren<FloatingDamageFeedbackManager>();
        floatingTextManager.DisplayDamage(damage);
    }

    protected virtual void Attack() {}

    public bool Equals(Enemy other)
    {
        if (other == null && this == null) return true;
        if (other == null || this == null) return false;

        return this.GetInstanceID() == other.GetInstanceID();

    }

    public void ApplyDamage(float damage)
    {
        _currentHealth -= damage;      
    }

    internal float GetSpeed()
    {
        return _currentSpeed;
    }

    public bool Targetable()
    {
        return _currentHealth > 0;
    }

    public Vector3 GetHitPosition()
    {
        return transform.position;
    }

    public void Hit()
    {
        HitByProjectile();
    }
}