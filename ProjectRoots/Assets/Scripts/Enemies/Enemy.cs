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

    void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

    public void HitByProjectile()
    {
        health -= 10;
        GetComponent<HealthBar>().SetHealthBarHealth();
        if (health <= 0)
        {
            //nextTarget(); <-- Turret keep firing the same target till is out of the scene
            GetComponent<EnemyMovement>().stopmoving();
            GetComponent<EnemyDeath>().TriggerDeath();
        }
    }

    //public void HitByHeavyProjectile()
    //{
    //    health -= 20;
    //    SetHealthBarHealth();
    //    if (health <= 0)
    //    {
    //        //nextTarget();
    //        GetComponent<EnemyMovement>().stopmoving();
    //        GetComponent<EnemyDeath>().TriggerDeath();
    //    }
    //}

    //public void HitBySniperProjectile()
    //{
    //    health -= 7;
    //    SetHealthBarHealth();
    //    if (health <= 0)
    //    {
    //        //nextTarget();
    //        GetComponent<EnemyMovement>().stopmoving();
    //        GetComponent<EnemyDeath>().TriggerDeath();
    //    }
    //}

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