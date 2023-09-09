using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Text Allgold_text; //���� ��ȭ ����
    public int gold = 1000; //�ʱ� ��ȭ + 

    public int Obj = 0;//���� �� ���� ��
    public Text ObjText;//���� �� �����ִ� �ؽ�Ʈ

    public static GameManager instance;

    public int health = 100;
    public int attackPower = 10;
    public int defense = 3;
    public float moveSpeed = 7;

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

    private void Start()
    {
        InvokeRepeating("Upgold", 1.0f, 1.0f); //1�� �Ŀ� 1�ʸ���
    }

    private void Update()
    {
        Allgold_text.text = " " + gold; //���� ��ȭ
    }

    private void Upgold()
    {
        gold += 2; //��ȭ 2�� ����
    }

    public  void Aobj()
    {
        ObjText.text = " " + Obj.ToString(); //���� ����
    }

    public void SetAttributes(int health, int attackPower, int defense, float moveSpeed)
    {
        this.health = health;
        this.attackPower = attackPower;
        this.defense = defense;
        this.moveSpeed = moveSpeed;
    }

}
