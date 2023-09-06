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
        ObjText.text = " " + Obj.ToString();
    }

}
