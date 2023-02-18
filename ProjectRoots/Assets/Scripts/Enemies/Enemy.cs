using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{

    public float startSpeed;
    public float speed;
    public float startHealth;
    public float health;
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
    }

    public void HitByProjectile(float damage)
    {
        health -= damage;
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

    protected virtual void Attack() {}
}