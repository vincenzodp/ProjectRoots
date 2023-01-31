using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //private Animation anim;
    public Transform Spawn;
    private Transform target;
    private Enemy enemy;
    private float startTime;

    void Start()
    {
        //anim = gameObject.GetComponent<Animation>();
        target = GameObject.FindGameObjectWithTag("Tree").transform;
        Spawn = GameObject.Find("SpawnPoint").transform;
        enemy = GetComponent<Enemy>();
        startTime = Time.time;
        //anim.Play();
    }

    void Update()
    {
        Vector3 center = (Spawn.position + target.position) * 0.8F;
        center -= new Vector3(0, 1, 0);
        Vector3 riseRelCenter = Spawn.position - center;
        Vector3 setRelCenter = target.position - center;

        // The fraction of the animation that has happened so far is
        // equal to the elapsed time divided by the desired enemy speed
        
        float fracComplete = (Time.time - startTime) * enemy.speed / 150f;

        transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
        transform.position += center;
        
        //Vector3 dir = target.position - transform.position;
        //transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        //if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        //{
        //    Debug.Log("Tree reached");
        //    Destroy(enemy);
        //}
        //enemy.speed = enemy.startSpeed;
    }
}