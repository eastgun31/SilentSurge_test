using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public GameObject[] points;

    int random;
    float time;
    float starttimer = 3f;
    float timer = 3f;
    float speed = 10f;

    enum EnemyState
    {
        Idle, move, spawn, back
    }

    EnemyState enemies;

    // Start is called before the first frame update
    void Start()
    {
        enemies = EnemyState.Idle;
        random = Random.Range(0, 3);
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        switch(enemies)
        {
            case EnemyState.Idle:
                E_Idle();
                break;
            case EnemyState.move:
                E_Move();
                break;
            case EnemyState.spawn:
                E_Spawn();
                break;
            case EnemyState.back:
                E_Back();
                break;
        }
            
    }

    void E_Idle()
    {
        //Debug.Log("d");
        
        if(time>starttimer)
        {
            time = 0;
            //Debug.Log("f");
            enemies = EnemyState.move;
        }
    }

    void E_Move()
    {
        //if (time > timer)
        //{
        //    time = 0;
        //}

        //random = Random.Range(0, 3);
        gameObject.transform.position = 
        Vector3.MoveTowards(transform.position, new Vector3(points[random].transform.position.x,gameObject.transform.position.y, points[random].transform.position.z), Time.deltaTime * speed);
        
        if(gameObject.transform.position.x == points[random].transform.position.x && gameObject.transform.position.z == points[random].transform.position.z)
        {
            random = Random.Range(0, 3);
            enemies = EnemyState.Idle;
            time = 0;
        }
    }

    void E_Spawn()
    {

    }

    void E_Back()
    {

    }
}
