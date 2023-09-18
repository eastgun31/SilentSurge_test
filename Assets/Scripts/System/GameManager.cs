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

    public Text Allgold_text; //���� ��ȭ ����
    public int gold = 1000; //�ʱ� ��ȭ + 

    public int All_Obj = 0;//���� �� ���� ��
    public Text All_ObjText;//���� �� �����ִ� �ؽ�Ʈ

 
    //���ֽ���
    public int health = 100;
    public int attackPower = 10;
    public int defense = 3;
    public float moveSpeed = 7;
    //

    public int[] check = { 0, 0, 0,0,0 };   //����, 0 �߸�, 1 ��������, 2�÷��̾�, 3�������� ����

    public bool attacking = false;  //���� ���ݹ޴»���
    public int attackPoint = 0;     //���� ���ݹ޴� ����
    public int e_population = 4;    //�����ּ�

    public bool pointBattle = false; //��������
    public bool battle = false;      //��������

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
        All_ObjText.text = " " + All_Obj.ToString(); //���� ����
    }


}
