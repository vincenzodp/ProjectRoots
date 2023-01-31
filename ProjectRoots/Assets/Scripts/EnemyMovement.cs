//using UnityEngine;

//public class EnemyMovement : MonoBehaviour
//{
//    //private Animation anim;
//    private Transform target;
//    private Enemy enemy;

//    void Start()
//    {
//        //anim = gameObject.GetComponent<Animation>();
//        target = GameObject.FindGameObjectWithTag("Tree").transform;
//        enemy = GetComponent<Enemy>();
//    }

//    void Update()
//    {
//        //anim.Play();
//        Vector3 dir = target.position - transform.position;
//        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

//        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
//        {
//            Debug.Log("Tree reached");
//            Destroy(enemy);
//        }
//        enemy.speed = enemy.startSpeed;
//    }
//}