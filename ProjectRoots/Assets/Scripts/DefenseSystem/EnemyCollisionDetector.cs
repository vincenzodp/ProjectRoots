using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionDetector : MonoBehaviour
{
    public delegate void OnNewEnemyEntered(Enemy enemy);
    public event OnNewEnemyEntered onNewEnemyEntered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            onNewEnemyEntered?.Invoke(other.GetComponent<Enemy>());
        }
    }
}
