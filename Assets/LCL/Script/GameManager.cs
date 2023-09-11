using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public Text Allgold_text; //현재 재화 상태
    public int gold = 1000; //초기 재화 + 

    public int Obj = 0;//현재 총 유닛 수
    public Text ObjText;//유닛 수 보여주는 텍스트

 
    //유닛스탯
    public int health = 100;
    public int attackPower = 10;
    public int defense = 3;
    public float moveSpeed = 7;
    //

    public int[] check = { 0, 0, 0 };   //거점

    public bool attacking = false;  //적이 공격받는상태
    public int attackPoint = 0;     //적이 공격받는 거점
    public int e_population = 0;    //적유닛수


    private void Start()
    {
        InvokeRepeating("Upgold", 1.0f, 1.0f); //1초 후에 1초마다
    }

    private void Update()
    {
        Allgold_text.text = " " + gold; //현재 재화
    }

    private void Upgold()
    {
        gold += 2; //재화 2씩 증가
    }

    public  void Aobj()
    {
        ObjText.text = " " + Obj.ToString(); //현재 유닛
    }


}
