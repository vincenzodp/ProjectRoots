using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform Spawn;
    public Transform Spawn1;
    private Transform target;
    private Enemy enemy;
    private float startTime;

    void Start()
    {

        target = GameObject.FindGameObjectWithTag("Tree").transform;
        Spawn = GameObject.Find("SpawnPoint").transform;
        Spawn1 = GameObject.Find("SpawnPoint2").transform;
        enemy = GetComponent<Enemy>();
        startTime = Time.time;
    }

    void Update()
    {
        if (enemy.transform.localPosition.x < 0)
        {
            Vector3 center = (Spawn.position + target.position) * 0.5F;
            center -= new Vector3(0, 9, 0);
            Vector3 riseRelCenter = Spawn.position - center;
            Vector3 setRelCenter = target.position - center;

            float fracComplete = (Time.time - startTime) * enemy.speed / 150f;

            transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
            transform.position += center;

            Vector3 relativePos = target.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = rotation;
        }
        else
        {
            Vector3 center = (Spawn1.position + target.position) * 0.5F;
            center -= new Vector3(0, 12, 0);
            Vector3 riseRelCenter = Spawn1.position - center;
            Vector3 setRelCenter = target.position - center;

            float fracComplete = (Time.time - startTime) * enemy.speed / 150f;

            transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
            transform.position += center;

            Vector3 relativePos = target.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = rotation;
        }

        //Vector3 center = (Spawn.position + target.position) * 0.5F;
        //center -= new Vector3(0, 9, 0);
        //Vector3 riseRelCenter = Spawn.position - center;
        //Vector3 setRelCenter = target.position - center;

        //float fracComplete = (Time.time - startTime) * enemy.speed / 150f;

        //transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
        //transform.position += center;

        //Vector3 relativePos = target.position - transform.position;
        //Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        //transform.rotation = rotation;

        if (Vector3.Distance(transform.position, target.position) <= 2.5f)
        {
            this.enabled = false;
        }
    }
}