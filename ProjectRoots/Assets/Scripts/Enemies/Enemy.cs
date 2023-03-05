using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour, ITarget, IEquatable<Enemy>
{

    public float startSpeed;
    public float speed;
    public float startHealth;
    public float health;

    public event Action<ITarget> OnTargetDestroy;


    public AudioSource hitaudiosource;
    public GameObject FloatingDamageFeedbackText;

    //public Gradient gradient;
    //public Image fill;

    private void Awake()
    {
        if (GameManager.Instance == null)
        {
            return;
        }

        GameManager.Instance.onGameOver += onGameOver;
    }


    void Start()
    {
        speed = startSpeed;
        health = startHealth;
        hitaudiosource = GetComponent<AudioSource>();
    }

    public void HitByProjectile()
    {
        GetComponent<HealthBar>().SetHealthBarHealth(health);
        
        if (health <= 0)
        {
            GetComponent<EnemyMovement>().stopmoving();
            GetComponent<EnemyDeath>().TriggerDeath();
        }
    }

    public float getHealth()
    {
        return health;
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
        var newFloatingText = Instantiate(FloatingDamageFeedbackText, position, FloatingDamageFeedbackText.transform.rotation);
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
        health -= damage;      
    }

    public bool Targetable()
    {
        return health <= 0;
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