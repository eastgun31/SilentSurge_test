using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public GameObject[] points;
    public List<GameObject> units = new List<GameObject>();
    public GameObject home;

    //int unitNum = 0;
    public int unitCount;
    int random;
    float time;
    float starttimer = 1.5f;
    float timer = 3f;
    float speed = 5f;

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
        unitCount = GameManager_KDG.Instance.unitcheck;

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

        if (units.Count == 0 || unitCount <= 0)
        {
            units.Clear();
            enemies = EnemyState.back;
            Debug.Log("empty");
        }

        if (time>starttimer)
        {
            time = 0;
            //Debug.Log("f");
            enemies = EnemyState.move;
        }


    }

    void E_Move()
    {
        if(GameManager_KDG.Instance.aCheck == 0)
        {
            gameObject.transform.position =
            Vector3.MoveTowards(transform.position, new Vector3(points[0].transform.position.x, gameObject.transform.position.y, points[0].transform.position.z), Time.deltaTime * speed);
            
            if (gameObject.transform.position.x == points[0].transform.position.x && gameObject.transform.position.z == points[0].transform.position.z)
            {
                //random = Random.Range(0, 3);
                enemies = EnemyState.Idle;
                time = 0;
            }
        }
        else if(GameManager_KDG.Instance.bCheck == 0)
        {
            gameObject.transform.position =
            Vector3.MoveTowards(transform.position, new Vector3(points[1].transform.position.x, gameObject.transform.position.y, points[1].transform.position.z), Time.deltaTime * speed);

            if (gameObject.transform.position.x == points[1].transform.position.x && gameObject.transform.position.z == points[1].transform.position.z)
            {
                //random = Random.Range(0, 3);
                enemies = EnemyState.Idle;
                time = 0;
            }
        }
        else if(GameManager_KDG.Instance.cCheck == 0)
        {
            gameObject.transform.position =
            Vector3.MoveTowards(transform.position, new Vector3(points[2].transform.position.x, gameObject.transform.position.y, points[2].transform.position.z), Time.deltaTime * speed);

            if (gameObject.transform.position.x == points[2].transform.position.x && gameObject.transform.position.z == points[2].transform.position.z)
            {
                //random = Random.Range(0, 3);
                enemies = EnemyState.Idle;
                time = 0;
            }
        }
        else
        {
            enemies = EnemyState.Idle;
        }


        if (units.Count == 0 || unitCount<=0)
        {
            units.Clear();
            enemies = EnemyState.back;
            Debug.Log("empty");
        }

        //gameObject.transform.position = 
        //Vector3.MoveTowards(transform.position, new Vector3(points[random].transform.position.x,gameObject.transform.position.y, points[random].transform.position.z), Time.deltaTime * speed);
        
        //if(gameObject.transform.position.x == points[random].transform.position.x && gameObject.transform.position.z == points[random].transform.position.z)
        //{
        //    random = Random.Range(0, 3);
        //    enemies = EnemyState.Idle;
        //    time = 0;
        //}

        //if(unitcheck == 30)
        //{
        //    enemies = EnemyState.back;
        //    unitcheck = 0;
        //}
    }

    void E_Spawn()
    {
        if (units.Count >= 0 || unitCount >= 0)
        {
            Debug.Log("spawning");
            enemies = EnemyState.Idle;
        }

    }

    void E_Back()
    {
        gameObject.transform.position = 
        Vector3.MoveTowards(transform.position, new Vector3(home.transform.position.x, gameObject.transform.position.y, home.transform.position.z), Time.deltaTime * speed);

        if(gameObject.transform.position.x == home.transform.position.x && gameObject.transform.position.z == home.transform.position.z)
        {
            enemies = EnemyState.spawn;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            //unitController unit = other.GetComponent<unitController>();
            //int unitIndex = unit.unitNum;
            //units[unitIndex] = other.gameObject;
            units.Add(other.gameObject);
            GameManager_KDG.Instance.unitcheck++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            //unitController unit = other.GetComponent<unitController>();
            //int unitIndex = unit.unitNum;
            //units[unitIndex] = null;
            units.Remove(other.gameObject);
            GameManager_KDG.Instance.unitcheck--;
        }
    }
}
