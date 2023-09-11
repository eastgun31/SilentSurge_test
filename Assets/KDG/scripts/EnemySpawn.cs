using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public float gold;
    public int population;
    int random;

    public GameObject unitController;

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
        population = GameManager_KDG.Instance.e_population;

        if(unitController.transform.position.x == gameObject.transform.position.x && unitController.transform.position.z == gameObject.transform.position.z)
        {
            if(population <= 30)
            {
                random = Random.Range(0, 4);

                if(random == 0 && gold >=5)
                {
                    gold -= 5;
                    SpawnUnit(random);
                }
                if(random == 1 && gold >=5)
                {
                    gold -= 5;
                    SpawnUnit(random);
                }
                if(random == 2 && gold >=8)
                {
                    gold -= 8;
                    SpawnUnit(random);
                }
                if(random == 3 && gold >=15)
                {
                    gold -= 15;
                    SpawnUnit(random);
                }
            }
        }
    }

    private void Upgold()
    {
        gold += 2; //재화 2씩 증가
    }

    void SpawnUnit(int i)
    {
        Debug.Log("spawn");
        GameObject enemy = Instantiate(e_unit[i], this.transform.position, Quaternion.identity);
        enemy.transform.parent = unitController.transform;
        GameManager_KDG.Instance.e_population++;
    }

}
