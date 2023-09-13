using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public float gold;
    public int population;
    int random;
    float spawnCool = 0;

    public GameObject[] unitController;

    public GameObject e_Swordman;
    public GameObject e_Shieldman;
    public GameObject e_Archer;
    public GameObject e_Horseman;

    public GameObject[] e_unit;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Upgold", 1.0f, 1.0f); //1초 후에 1초마다
    }

    // Update is called once per frame
    void Update()
    {
        population = GameManager.instance.e_population;

        if (unitController[0].transform.position == gameObject.transform.position && unitController[0].transform.childCount <= 10)
        {
            RandomSpawn(0);
        }
        else if (unitController[1].transform.position == gameObject.transform.position && unitController[0].transform.childCount <= 10)
        {
            RandomSpawn(1);
        }
        else if (unitController[2].transform.position == gameObject.transform.position && unitController[0].transform.childCount <= 10)
        {
            RandomSpawn(2);
        }
    }

    private void Upgold()
    {
        gold += 2; //재화 2씩 증가
    }

    void RandomSpawn(int i)
    {
        spawnCool += Time.deltaTime;

        if (spawnCool > 1f)
        {
            if (population <= 30)
            {
                random = Random.Range(0, 4);

                if (random == 0 && gold >= 5)
                {
                    gold -= 5;
                    SpawnUnit(random, i);
                }
                if (random == 1 && gold >= 5)
                {
                    gold -= 5;
                    SpawnUnit(random, i);
                }
                if (random == 2 && gold >= 8)
                {
                    gold -= 8;
                    SpawnUnit(random, i);
                }
                if (random == 3 && gold >= 15)
                {
                    gold -= 15;
                    SpawnUnit(random, i);
                }
            }
            spawnCool = 0;
        }
    }

    void SpawnUnit(int i, int j)
    {
        Debug.Log("spawn");
        GameObject enemy = Instantiate(e_unit[i], this.transform.position, Quaternion.identity);
        enemy.transform.parent = unitController[j].transform;
        GameManager.instance.e_population++;
    }
}
