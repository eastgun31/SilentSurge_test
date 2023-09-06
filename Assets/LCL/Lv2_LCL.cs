using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lv2_LCL : MonoBehaviour
{
    int Gold;
    int U;

    public Button button_warrio_2Lr; //�˻� 2���� �Ǹ� ��ư
    public GameObject Warrior_2L_Obj;//�˻� 2���� ������Ʈ

    public Button button_shield_2Lr; //���к� 2���� �Ǹ� ��ư
    public GameObject Shield_2L_Obj;//���к� 2���� ������Ʈ

    public Button button_archer_2L; //�ü� 2���� �Ǹ� ��ư
    public GameObject Archer_2L_Obj; //�ü� 2���� ������Ʈ

    public Button button_horseMan_2L; //�⸶�� 2���� �Ǹ� ��ư
    public GameObject HorseMan_2L_Obj; //�⸶�� 2���� ������Ʈ

    public Button UpButton3L; //���� 3������ ���׷��̵� ��Ű�� ��ư

    // Start is called before the first frame update
    void Start()
    {

        button_warrio_2Lr.onClick.AddListener(Down_warrior_2L); //�˻� 2���� �Ǹ� ��ư Ŭ�� ����
        button_shield_2Lr.onClick.AddListener(Down_shield_2L); //���к� 2���� �Ǹ� ��ư Ŭ�� ����
        button_archer_2L.onClick.AddListener(Down_archer_2L); //�ü� 2���� �Ǹ� ��ư Ŭ�� ����
        button_horseMan_2L.onClick.AddListener(Down_horseMan_2L); //�⸶�� 2���� �Ǹ� ��ư Ŭ�� ����
        UpButton3L.onClick.AddListener(Up_3L); //���� 3������ ���׷��̵� ��ư Ŭ�� ����
    }

    // Update is called once per frame
    void Update()
    {
        Gold = GameManager.instance.gold;
        U = GameManager.instance.Obj;

        button_warrio_2Lr.interactable = (Gold >= 5); //�˻� 2���� 5������ ���� �� ��ư ��Ȱ��ȭ
        button_shield_2Lr.interactable = (Gold >= 5); //���к� 2���� 5������ ���� �� ��ư ��Ȱ��ȭ
        button_archer_2L.interactable = (Gold >= 8); //�ü� 2���� 8������ ���� �� ��ư ��Ȱ��ȭ
        button_horseMan_2L.interactable = (Gold >= 15); //�⸶�� 2���� 15������ ���� �� ��ư ��Ȱ��ȭ
        UpButton3L.interactable = (Gold >= 600); //���� 3������ ���׷��̵� 600 ���� �� ��ư ��Ȱ��ȭ
    }


    private void Down_warrior_2L()
    {
        if (U < 30)//�˻� ���� ���� 30
        {
            if (Gold >= 5)
            {
                GameManager.instance.gold -= 5;
                GameObject newObject = Instantiate(Warrior_2L_Obj, transform.position, Quaternion.identity); //�˻� 2���� ���� ����
                GameManager.instance.Obj++;
                GameManager.instance.Aobj();
            }
        }
    }

    private void Down_shield_2L()
    {
        if (U < 30)//���к� ���� ���� 30
            if (Gold >= 5)
            {
                GameManager.instance.gold -= 5; //��ư�� ������ �˻� 1���� �ʿ� ��ȭ 5�� ����
                GameObject newObject = Instantiate(Shield_2L_Obj, transform.position, Quaternion.identity); //���к� 2���� ���� ����
                GameManager.instance.Obj++;
                GameManager.instance.Aobj();
            }
    }

    private void Down_archer_2L()
    {
        if (U < 30)//�ü� ���� ���� 30
            if (Gold >= 8)
            {
                GameManager.instance.gold -= 8; //��ư�� ������ �ü� 1���� �ʿ� ��ȭ 8�� ����
                GameObject newObject = Instantiate(Archer_2L_Obj, transform.position, Quaternion.identity); //�ü� 2���� ���� ����
                GameManager.instance.Obj++;
                GameManager.instance.Aobj();
            }
    }

    private void Down_horseMan_2L()
    {
        if (U < 30)//�⸶�� ���� ���� 30
            if (Gold >= 15)
            {
                GameManager.instance.gold -= 15; //��ư�� ������ �⸶�� 1���� �ʿ� ��ȭ 15�� ����
                GameObject newObject = Instantiate(HorseMan_2L_Obj, transform.position, Quaternion.identity); //�⸶�� 2���� ���� ����
                GameManager.instance.Obj++;
                GameManager.instance.Aobj();
            }
    }

    private void Up_3L()
    {
        if (Gold >= 600)
        {
            GameManager.instance.gold -= 600; //��ư�� ������ ���� 3������ ���׷��̵� �ϱ� ���� �ʿ� ��ȭ 600 ����

        }
    }
}
