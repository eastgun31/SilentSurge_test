using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class E_unitMove : MonoBehaviour
{
    public GameObject[] points;
    public int unitNum;

    float speed = 7f;
    public float health;
    float time;
    float timer = 2f;
    Rigidbody rigid;

    NavMeshAgent moving;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        health = 100;
        moving = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        //time += Time.deltaTime;

        //if (time > timer)
        //{
        //    time = 0;
        //    health -= 4;
        //}

        //if (health <= 0)
        //{
        //    Die();
        //}


    }

    //private void OnEnable()
    //{
    //    follower = transform.parent.gameObject;
    //}

    public void MovePoint(Vector3 i)
    {
        moving.SetDestination(i);

        transform.SetParent(null);
    }

    void Die()
    {
        GameManager.instance.e_population--;
        Destroy(gameObject);
    }
}
