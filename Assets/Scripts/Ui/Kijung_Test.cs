using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kijung_Test : MonoBehaviour
{
    public GameObject A;
    public GameObject B;
    public GameObject C;
    public GameObject D;
    public GameObject E;

    public GameObject A_Red;
    public GameObject B_Red;
    public GameObject C_Red;
    public GameObject D_Red;
    public GameObject E_Red;

    public GameObject A_Blue;
    public GameObject B_Blue;
    public GameObject C_Blue;
    public GameObject D_Blue;
    public GameObject E_Blue;

    public GameObject A_3;
    public GameObject B_3;
    public GameObject C_3;
    public GameObject D_3;
    public GameObject E_3;

    public int[] check = { 0, 0, 0, 0, 0 };   //거점, 0 중립, 1 적이점령, 2플레이어, 3거점에서 전투

    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < check.Length; i++)
        {

        }
    }
}
