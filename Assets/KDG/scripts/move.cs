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
    float speed = 6f;

    enum EnemyState
    {
        Idle, move, spawn, back, firstMove
    }

    EnemyState enemies;

    // Start is called before the first frame update
    void Start()
    {
        random = Random.Range(0, 3);
        enemies = EnemyState.firstMove;
        time = 0;
        for(int i = 0; i<30; i++)
        {            
            if (transform.GetChild(i).gameObject == null)
                break;

            Debug.Log("切縦持失");

            units.Add(transform.GetChild(i).gameObject);
            GameManager.instance.e_population++;
        }
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
        if(unitCount >= 5)
        {
            gameObject.transform.position =
            Vector3.MoveTowards(transform.position, new Vector3(points[random].transform.position.x, gameObject.transform.position.y, points[random].transform.position.z), Time.deltaTime * speed);

            if (gameObject.transform.position.x == points[random].transform.position.x && gameObject.transform.position.z == points[random].transform.position.z)
            {
                //random = Random.Range(0, 3);
                enemies = EnemyState.Idle;
                time = 0;
            }
        }
    }

    void E_Move()
    {
        if(GameManager.instance.attacking == true)
        {
            gameObject.transform.position =
            Vector3.MoveTowards(transform.position, new Vector3(points[GameManager.instance.attackPoint].transform.position.x, gameObject.transform.position.y, points[GameManager.instance.attackPoint].transform.position.z), Time.deltaTime * speed);

            if (gameObject.transform.position.x == points[GameManager.instance.attackPoint].transform.position.x && gameObject.transform.position.z == points[GameManager.instance.attackPoint].transform.position.z)
            {
                GameManager.instance.attacking = false;
                //random = Random.Range(0, 3);
                enemies = EnemyState.Idle;
                time = 0;
            }
        }
        else if(GameManager.instance.check[0] == 0 || GameManager.instance.check[0] == 2)
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
        else if(GameManager.instance.check[1] == 0 || GameManager.instance.check[1] == 2)
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
        else if(GameManager.instance.check[2] == 0 || GameManager.instance.check[2] == 2)
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
            //Debug.Log("empty");
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
        //for (int i = 0; i < 30; i++)
        //{
        //    if (transform.GetChild(i).gameObject == null)
        //        break;

        //    Debug.Log("切縦持失");

        //    units.Add(transform.GetChild(i).gameObject);
        //    GameManager.instance.e_population++;
        //}
        int i = 0;

        units.Add(transform.GetChild(i).gameObject);
        GameManager.instance.e_population++;


        if (units.Count >= 5 || unitCount >= 5)
        {
            //Debug.Log("spawning");
            enemies = EnemyState.Idle;
        }

        i++;
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

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.tag == "Enemy")
    //    {
    //        //unitController unit = other.GetComponent<unitController>();
    //        //int unitIndex = unit.unitNum;
    //        //units[unitIndex] = other.gameObject;
    //        units.Add(other.gameObject);
    //        GameManager.instance.e_population++;
    //    }
    //    if (other.tag == "Player")
    //    {
    //        //speed = 0;
    //    }

    //}

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            //unitController unit = other.GetComponent<unitController>();
            //int unitIndex = unit.unitNum;
            //units[unitIndex] = null;
            units.Remove(other.gameObject);
            GameManager.instance.e_population--;
        }
        if (other.tag == "Player")
        {
            //speed = 5f;
        }

    }
}
