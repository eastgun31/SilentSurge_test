using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public GameObject[] points;
    public List<GameObject> units = new List<GameObject>();
    public GameObject home;
    public GameObject playerhome;

    public int unitCount;
    int random;
    float time;
    float starttimer = 1.5f;
    float speed = 7f;

    enum EnemyState
    {
        Idle, move, spawn, back, firstMove
    }

    EnemyState enemies;

    // Start is called before the first frame update
    void Start()
    {
        random = Random.Range(0, 5);
        enemies = EnemyState.spawn;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        unitCount = GameManager.instance.e_population;

        switch(enemies)
        {
            case EnemyState.firstMove:
                E_firstMove();
                break;
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
        random = Random.Range(0, 5);

        if (units.Count == 0 || unitCount <= 0)
        {
            units.Clear();
            enemies = EnemyState.back;
            //Debug.Log("empty");
        }

        if (time>starttimer)
        {
            time = 0;
            //Debug.Log("f");
            enemies = EnemyState.move;
        }
    }

    void E_firstMove()
    {
        if (transform.childCount == 10)
        {
            gameObject.transform.position =
            Vector3.MoveTowards(transform.position, points[random].transform.position, Time.deltaTime * speed);

            if (gameObject.transform.position == points[random].transform.position)
            {
                //random = Random.Range(0, 3);
                enemies = EnemyState.Idle;
                time = 0;
            }
        }
    }

    void E_Move()
    {
        if (GameManager.instance.attacking == true)
        {
            gameObject.transform.position =
            Vector3.MoveTowards(transform.position, points[GameManager.instance.attackPoint].transform.position, Time.deltaTime * speed);

            if (gameObject.transform.position == points[GameManager.instance.attackPoint].transform.position)
            {
                GameManager.instance.attacking = false;
                //random = Random.Range(0, 3);
                enemies = EnemyState.Idle;
                time = 0;
            }
        }
        else if (GameManager.instance.check[random] == 0 || GameManager.instance.check[random] == 2)
        {
            gameObject.transform.position =
            Vector3.MoveTowards(transform.position, points[random].transform.position, Time.deltaTime * speed);

            if (gameObject.transform.position == points[random].transform.position)
            {
                //random = Random.Range(0, 3);
                enemies = EnemyState.Idle;
                time = 0;
            }
        }
        else if (GameManager.instance.check[0] == 1 && GameManager.instance.check[1] == 1 && GameManager.instance.check[2] == 1 && GameManager.instance.check[3] == 1 && GameManager.instance.check[4] == 1)
        {
            gameObject.transform.position =
            Vector3.MoveTowards(transform.position, playerhome.transform.position, Time.deltaTime * speed);

        }
        else
        {
            enemies = EnemyState.Idle;
        }

        if (transform.childCount == 0)
        {
            units.Clear();
            enemies = EnemyState.back;
            //Debug.Log("empty");
        }


    }

    void E_Spawn()
    {
        if (GameManager.instance.attacking == true && transform.childCount >= 5)
        {
            enemies = EnemyState.move;
        }

        if (transform.childCount >= 10)
        {
            //Debug.Log("spawning");
            enemies = EnemyState.Idle;
        }
    }

    void E_Back()
    {
        gameObject.transform.position =
        Vector3.MoveTowards(transform.position, home.transform.position, Time.deltaTime * speed);

        if (gameObject.transform.position == home.transform.position)
        {
            enemies = EnemyState.spawn;
        }
    }

}
