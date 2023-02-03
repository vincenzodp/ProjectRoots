using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{

    public float startSpeed;
    public float speed;
    public float startHealth;
    public float health;
    public Slider slider;
    //public Gradient gradient;
    //public Image fill;

    void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

    public void SetHealthBarMaxHealth()
    {
        slider.maxValue = health;
        slider.value = health;

        //fill.color = gradient.Evaluate(1f);
    }

    public void SetHealthBarHealth()
    {
        slider.value = health;
        //fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void HitByProjectile()
    {
        health -= 10;
        if (health <= 0)
        {
            //nextTarget(); <-- Turret keep firing the same target till is out of the scene
            GetComponent<EnemyMovement>().stopmoving();
            GetComponent<EnemyDeath>().TriggerDeath();
        }
    }

    public void HitByHeavyProjectile()
    {
        health -= 20;
        if (health <= 0)
        {
            //nextTarget();
            GetComponent<EnemyMovement>().stopmoving();
            GetComponent<EnemyDeath>().TriggerDeath();
        }
    }

    public void HitBySniperProjectile()
    {
        health -= 7;
        if (health <= 0)
        {
            //nextTarget();
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

    protected virtual void Attack() {}
}