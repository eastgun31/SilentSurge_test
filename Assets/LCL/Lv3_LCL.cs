using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lv3_LCL : MonoBehaviour
{
    int Gold;
    int U;

    public Button button_warrio_3Lr; //�˻� 3���� �Ǹ� ��ư
    public GameObject Warrior_3L_Obj;//�˻� 3���� ������Ʈ

    public Button button_shield_3Lr; //���к� 3���� �Ǹ� ��ư
    public GameObject Shield_3L_Obj;//���к� 3���� ������Ʈ

    public Button button_archer_3L; //�ü� 3���� �Ǹ� ��ư
    public GameObject Archer_3L_Obj; //�ü� 3���� ������Ʈ

    public Button button_horseMan_3L; //�⸶�� 3���� �Ǹ� ��ư
    public GameObject HorseMan_3L_Obj; //�⸶�� 3���� ������Ʈ

    // Start is called before the first frame update
    void Start()
    {

        button_warrio_3Lr.onClick.AddListener(Down_warrior_3L); //�˻� 3���� �Ǹ� ��ư Ŭ�� ����
        button_shield_3Lr.onClick.AddListener(Down_shield_3L); //���к� 3���� �Ǹ� ��ư Ŭ�� ����
        button_archer_3L.onClick.AddListener(Down_archer_3L); //�ü� 3���� �Ǹ� ��ư Ŭ�� ����
        button_horseMan_3L.onClick.AddListener(Down_horseMan_3L); //�⸶�� 3���� �Ǹ� ��ư Ŭ�� ����
    }

    // Update is called once per frame
    void Update()
    {
        Gold = GameManager.instance.gold;
        U = GameManager.instance.Obj;

        button_warrio_3Lr.interactable = (Gold >= 5); //�˻� 2���� 5������ ���� �� ��ư ��Ȱ��ȭ
        button_shield_3Lr.interactable = (Gold >= 5); //���к� 2���� 5������ ���� �� ��ư ��Ȱ��ȭ
        button_archer_3L.interactable = (Gold >= 8); //�ü� 2���� 8������ ���� �� ��ư ��Ȱ��ȭ
        button_horseMan_3L.interactable = (Gold >= 15); //�⸶�� 2���� 15������ ���� �� ��ư ��Ȱ��ȭ
    }


    private void Down_warrior_3L()
    {
        if (U < 30)//�˻� ���� ���� 30
        {
            if (Gold >= 5)
            {
                GameManager.instance.gold -= 5;
                GameObject newObject = Instantiate(Warrior_3L_Obj, transform.position, Quaternion.identity); //�˻� 2���� ���� ����
                GameManager.instance.Obj++;
                GameManager.instance.Aobj();
            }
        }
    }

    private void Down_shield_3L()
    {
        if (U < 30)//���к� ���� ���� 30
            if (Gold >= 5)
            {
                GameManager.instance.gold -= 5; //��ư�� ������ �˻� 1���� �ʿ� ��ȭ 5�� ����
                GameObject newObject = Instantiate(Shield_3L_Obj, transform.position, Quaternion.identity); //���к� 2���� ���� ����
                GameManager.instance.Obj++;
                GameManager.instance.Aobj();
            }
    }

    private void Down_archer_3L()
    {
        if (U < 30)//�ü� ���� ���� 30
            if (Gold >= 8)
            {
                GameManager.instance.gold -= 8; //��ư�� ������ �ü� 1���� �ʿ� ��ȭ 8�� ����
                GameObject newObject = Instantiate(Archer_3L_Obj, transform.position, Quaternion.identity); //�ü� 2���� ���� ����
                GameManager.instance.Obj++;
                GameManager.instance.Aobj();
            }
    }

    private void Down_horseMan_3L()
    {
        if (U < 30)//�⸶�� ���� ���� 30
            if (Gold >= 15)
            {
                GameManager.instance.gold -= 15; //��ư�� ������ �⸶�� 1���� �ʿ� ��ȭ 15�� ����
                GameObject newObject = Instantiate(HorseMan_3L_Obj, transform.position, Quaternion.identity); //�⸶�� 2���� ���� ����
                GameManager.instance.Obj++;
                GameManager.instance.Aobj();
            }
    }
}
