using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class All_Lv_LCL : MonoBehaviour
{
    public static All_Lv_LCL instance;

    private int Gold;
    private int U;

    public Vector3 warrio_spawnPosition;
    public Vector3 shield_spawnPosition;
    public Vector3 Archer_spawnPosition;
    public Vector3 HorseMan_spawnPosition;


    public Button button_warrio_1Lr; //�˻� 1���� �Ǹ� ��ư
    public GameObject Warrior_1L_Obj;//�˻� 1���� ������Ʈ
    public Button button_shield_1Lr; //���к� 1���� �Ǹ� ��ư
    public GameObject Shield_1L_Obj;//���к� 1���� ������Ʈ
    public Button button_archer_1L; //�ü� 1���� �Ǹ� ��ư
    public GameObject Archer_1L_Obj; //�ü� 1���� ������Ʈ
    public Button button_horseMan_1L; //�⸶�� 1���� �Ǹ� ��ư
    public GameObject HorseMan_1L_Obj; //�⸶�� 1���� ������Ʈ

    public Button button_warrio_2Lr; //�˻� 2���� �Ǹ� ��ư
    public GameObject Warrior_2L_Obj;//�˻� 2���� ������Ʈ
    public Button button_shield_2Lr; //���к� 2���� �Ǹ� ��ư
    public GameObject Shield_2L_Obj;//���к� 2���� ������Ʈ
    public Button button_archer_2L; //�ü� 2���� �Ǹ� ��ư
    public GameObject Archer_2L_Obj; //�ü� 2���� ������Ʈ
    public Button button_horseMan_2L; //�⸶�� 2���� �Ǹ� ��ư
    public GameObject HorseMan_2L_Obj; //�⸶�� 2���� ������Ʈ

    public Button button_warrio_3Lr; //�˻� 3���� �Ǹ� ��ư
    public GameObject Warrior_3L_Obj;//�˻� 3���� ������Ʈ
    public Button button_shield_3Lr; //���к� 3���� �Ǹ� ��ư
    public GameObject Shield_3L_Obj;//���к� 3���� ������Ʈ
    public Button button_archer_3L; //�ü� 3���� �Ǹ� ��ư
    public GameObject Archer_3L_Obj; //�ü� 3���� ������Ʈ
    public Button button_horseMan_3L; //�⸶�� 3���� �Ǹ� ��ư
    public GameObject HorseMan_3L_Obj; //�⸶�� 3���� ������Ʈ

    public Button UpButton2L; //���� 2������ ���׷��̵� ��Ű�� ��ư
    public Button UpButton3L; //���� 3������ ���׷��̵� ��Ű�� ��ư

    public GameObject WarrioPosition;
    public GameObject ShieldPoaition;
    public GameObject ArcherPoaition;
    public GameObject HorseManPoaition;

    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    void Start()
    {
        button_warrio_1Lr.onClick.AddListener(Down_warrior_1L); //�˻� 1���� �Ǹ� ��ư Ŭ�� ����
        button_shield_1Lr.onClick.AddListener(Down_shield_1L); //���к� 1���� �Ǹ� ��ư Ŭ�� ����
        button_archer_1L.onClick.AddListener(Down_archer_1L); //�ü� 1���� �Ǹ� ��ư Ŭ�� ����
        button_horseMan_1L.onClick.AddListener(Down_horseMan_1L); //�⸶�� 1���� �Ǹ� ��ư Ŭ�� ����

        button_warrio_2Lr.onClick.AddListener(Down_warrior_2L); //�˻� 2���� �Ǹ� ��ư Ŭ�� ����
        button_shield_2Lr.onClick.AddListener(Down_shield_2L); //���к� 2���� �Ǹ� ��ư Ŭ�� ����
        button_archer_2L.onClick.AddListener(Down_archer_2L); //�ü� 2���� �Ǹ� ��ư Ŭ�� ����
        button_horseMan_2L.onClick.AddListener(Down_horseMan_2L); //�⸶�� 2���� �Ǹ� ��ư Ŭ�� ����

        button_warrio_3Lr.onClick.AddListener(Down_warrior_3L); //�˻� 3���� �Ǹ� ��ư Ŭ�� ����
        button_shield_3Lr.onClick.AddListener(Down_shield_3L); //���к� 3���� �Ǹ� ��ư Ŭ�� ����
        button_archer_3L.onClick.AddListener(Down_archer_3L); //�ü� 3���� �Ǹ� ��ư Ŭ�� ����
        button_horseMan_3L.onClick.AddListener(Down_horseMan_3L); //�⸶�� 3���� �Ǹ� ��ư Ŭ�� ����

        UpButton2L.onClick.AddListener(Up_2L); //���� 2������ ���׷��̵� ��ư Ŭ�� ����                                       
        UpButton3L.onClick.AddListener(Up_3L); //���� 3������ ���׷��̵� ��ư Ŭ�� ����
    }

    // Update is called once per frame
    void Update()
    {
        Gold = GameManager.instance.gold;
        U = GameManager.instance.All_Obj;
        //warrio_spawnPosition = new Vector3(25, 0, 35);
        //warrio_spawnPosition = new Vector3(30, 0, 36); //��ġ ����
        //shield_spawnPosition = new Vector3(32, 0, 34);
        //Archer_spawnPosition = new Vector3(34, 0, 32);
        //HorseMan_spawnPosition = new Vector3(36, 0, 30);

        button_warrio_1Lr.interactable = (Gold >= 50); //�˻� 1���� 5������ ���� �� ��ư ��Ȱ��ȭ
        button_shield_1Lr.interactable = (Gold >= 70); //���к� 1���� 5������ ���� �� ��ư ��Ȱ��ȭ
        button_archer_1L.interactable = (Gold >= 70); //�ü� 1���� 8������ ���� �� ��ư ��Ȱ��ȭ
        button_horseMan_1L.interactable = (Gold >= 150); //�⸶�� 1���� 15������ ���� �� ��ư ��Ȱ��ȭ

        button_warrio_2Lr.interactable = (Gold >= 50); //�˻� 2���� 5������ ���� �� ��ư ��Ȱ��ȭ
        button_shield_2Lr.interactable = (Gold >= 70); //���к� 2���� 5������ ���� �� ��ư ��Ȱ��ȭ
        button_archer_2L.interactable = (Gold >= 70); //�ü� 2���� 8������ ���� �� ��ư ��Ȱ��ȭ
        button_horseMan_2L.interactable = (Gold >= 150); //�⸶�� 2���� 15������ ���� �� ��ư ��Ȱ��ȭ

        button_warrio_3Lr.interactable = (Gold >= 50); //�˻� 2���� 5������ ���� �� ��ư ��Ȱ��ȭ
        button_shield_3Lr.interactable = (Gold >= 70); //���к� 2���� 5������ ���� �� ��ư ��Ȱ��ȭ
        button_archer_3L.interactable = (Gold >= 70); //�ü� 2���� 8������ ���� �� ��ư ��Ȱ��ȭ
        button_horseMan_3L.interactable = (Gold >= 150); //�⸶�� 2���� 15������ ���� �� ��ư ��Ȱ��ȭ

        UpButton2L.interactable = (Gold >= 350); //���� 2������ ���׷��̵� 350������ ���� �� ��ư ��Ȱ��ȭ
        UpButton3L.interactable = (Gold >= 600); //���� 3������ ���׷��̵� 600 ���� �� ��ư ��Ȱ��ȭ
    }


    private void Down_warrior_1L()
    {
        if (U < 30)//�˻� ���� ���� 30
        {
            if (Gold >= 5)
            {
                GameManager.instance.gold -= 50;
                GameObject newObject = Instantiate(Warrior_1L_Obj, WarrioPosition.transform.position, Quaternion.identity); //�˻� 1���� ���� ����
               // newObject.transform.position = WarrioPosition.transform.position;
                GameManager.instance.All_Obj++;
                GameManager.instance.Aobj();


                UnitController unit = newObject.GetComponent<UnitController>();
                RTSUnitController.instance.UnitList.Add(unit);
            }
        }
    }

    private void Down_shield_1L()
    {
        if (U < 30)//���к� ���� ���� 30
            if (Gold >= 70)
            {
                GameManager.instance.gold -= 70; //��ư�� ������ ���к� 1���� �ʿ� ��ȭ �Ҹ�
                GameObject newObject = Instantiate(Shield_1L_Obj, ShieldPoaition.transform.position, Quaternion.identity); //���к� 1���� ���� ����
               // newObject.transform.position = ShieldPoaition.transform.position;
                GameManager.instance.All_Obj++;
                GameManager.instance.Aobj();

                UnitController unit = newObject.GetComponent<UnitController>();
                RTSUnitController.instance.UnitList.Add(unit);

            }
    }

    private void Down_archer_1L()
    {
        if (U < 30)//�ü� ���� ���� 30
            if (Gold >= 70)
            {
                GameManager.instance.gold -= 70; //��ư�� ������ �ü� 1���� �ʿ� ��ȭ 8�� ����
                GameObject newObject = Instantiate(Archer_1L_Obj, ArcherPoaition.transform.position, Quaternion.identity); //�ü� 1���� ���� ����
                //newObject.transform.position = ArcherPoaition.transform.position;
                GameManager.instance.All_Obj++;
                GameManager.instance.Aobj();

                UnitController unit = newObject.GetComponent<UnitController>();
                RTSUnitController.instance.UnitList.Add(unit);
            }
    }

    private void Down_horseMan_1L()
    {
        if (U < 30)//�⸶�� ���� ���� 30
            if (Gold >= 150)
            {
                GameManager.instance.gold -= 150; //��ư�� ������ �⸶�� 1���� �ʿ� ��ȭ 15�� ����
                GameObject newObject = Instantiate(HorseMan_1L_Obj, HorseManPoaition.transform.position, Quaternion.identity); //�⸶�� 1���� ���� ����
                //newObject.transform.position = HorseManPoaition.transform.position;
                GameManager.instance.All_Obj++;
                GameManager.instance.Aobj();

                UnitController unit = newObject.GetComponent<UnitController>();
                RTSUnitController.instance.UnitList.Add(unit);
            }
    }


    private void Down_warrior_2L()
    {
        if (U < 30)//�˻� ���� ���� 30
        {
            if (Gold >= 50)
            {
                GameManager.instance.gold -= 50;
                GameObject newObject = Instantiate(Warrior_2L_Obj, WarrioPosition.transform.position, Quaternion.identity); //�˻� 2���� ���� ����
                //newObject.transform.position = WarrioPosition.transform.position;
                GameManager.instance.All_Obj++;
                GameManager.instance.Aobj();

                UnitController unit = newObject.GetComponent<UnitController>();
                RTSUnitController.instance.UnitList.Add(unit);
            }
        }
    }

    private void Down_shield_2L()
    {
        if (U < 30)//���к� ���� ���� 30
            if (Gold >= 70)
            {
                GameManager.instance.gold -= 70; //��ư�� ������ �˻� 1���� �ʿ� ��ȭ 5�� ����
                GameObject newObject = Instantiate(Shield_2L_Obj, ShieldPoaition.transform.position, Quaternion.identity); //���к� 2���� ���� ����
                //newObject.transform.position = ShieldPoaition.transform.position;
                GameManager.instance.All_Obj++;
                GameManager.instance.Aobj();

                UnitController unit = newObject.GetComponent<UnitController>();
                RTSUnitController.instance.UnitList.Add(unit);
            }
    }

    private void Down_archer_2L()
    {
        if (U < 30)//�ü� ���� ���� 30
            if (Gold >= 70)
            {
                GameManager.instance.gold -= 70; //��ư�� ������ �ü� 1���� �ʿ� ��ȭ 8�� ����
                GameObject newObject = Instantiate(Archer_2L_Obj, ArcherPoaition.transform.position, Quaternion.identity); //�ü� 2���� ���� ����
                //newObject.transform.position = ArcherPoaition.transform.position;
                GameManager.instance.All_Obj++;
                GameManager.instance.Aobj();

                UnitController unit = newObject.GetComponent<UnitController>();
                RTSUnitController.instance.UnitList.Add(unit);
            }
    }

    private void Down_horseMan_2L()
    {
        if (U < 30)//�⸶�� ���� ���� 30
            if (Gold >= 150)
            {
                GameManager.instance.gold -= 150; //��ư�� ������ �⸶�� 1���� �ʿ� ��ȭ 15�� ����
                GameObject newObject = Instantiate(HorseMan_2L_Obj, HorseManPoaition.transform.position, Quaternion.identity); //�⸶�� 2���� ���� ����
                //newObject.transform.position = HorseManPoaition.transform.position;
                GameManager.instance.All_Obj++;
                GameManager.instance.Aobj();

                UnitController unit = newObject.GetComponent<UnitController>();
                RTSUnitController.instance.UnitList.Add(unit);
            }
    }


    private void Down_warrior_3L()
    {
        if (U < 30)//�˻� ���� ���� 30
        {
            if (Gold >= 50)
            {
                GameManager.instance.gold -= 50;
                GameObject newObject = Instantiate(Warrior_3L_Obj, WarrioPosition.transform.position, Quaternion.identity); //�˻� 2���� ���� ����
                //newObject.transform.position = WarrioPosition.transform.position;
                GameManager.instance.All_Obj++;
                GameManager.instance.Aobj();

                UnitController unit = newObject.GetComponent<UnitController>();
                RTSUnitController.instance.UnitList.Add(unit);
            }
        }
    }

    private void Down_shield_3L()
    {
        if (U < 30)//���к� ���� ���� 30
            if (Gold >= 70)
            {
                GameManager.instance.gold -= 70; //��ư�� ������ �˻� 1���� �ʿ� ��ȭ 5�� ����
                GameObject newObject = Instantiate(Shield_3L_Obj, ShieldPoaition.transform.position, Quaternion.identity); //���к� 2���� ���� ����
                //newObject.transform.position = ShieldPoaition.transform.position;
                GameManager.instance.All_Obj++;
                GameManager.instance.Aobj();

                UnitController unit = newObject.GetComponent<UnitController>();
                RTSUnitController.instance.UnitList.Add(unit);
            }
    }

    private void Down_archer_3L()
    {
        if (U < 30)//�ü� ���� ���� 30
            if (Gold >= 70)
            {
                GameManager.instance.gold -= 70; //��ư�� ������ �ü� 1���� �ʿ� ��ȭ 8�� ����
                GameObject newObject = Instantiate(Archer_3L_Obj, ArcherPoaition.transform.position, Quaternion.identity); //�ü� 2���� ���� ����
                //newObject.transform.position = ArcherPoaition.transform.position;
                GameManager.instance.All_Obj++;
                GameManager.instance.Aobj();

                UnitController unit = newObject.GetComponent<UnitController>();
                RTSUnitController.instance.UnitList.Add(unit);
            }
    }

    private void Down_horseMan_3L()
    {
        if (U < 30)//�⸶�� ���� ���� 30
            if (Gold >= 150)
            {
                GameManager.instance.gold -= 150; //��ư�� ������ �⸶�� 1���� �ʿ� ��ȭ 15�� ����
                GameObject newObject = Instantiate(HorseMan_3L_Obj, HorseManPoaition.transform.position, Quaternion.identity); //�⸶�� 2���� ���� ����
                //newObject.transform.position = HorseManPoaition.transform.position;
                GameManager.instance.All_Obj++;
                GameManager.instance.Aobj();

                UnitController unit = newObject.GetComponent<UnitController>();
                RTSUnitController.instance.UnitList.Add(unit);
            }
    }


    private void Up_3L()
    {
        if (Gold >= 600)
        {
            GameManager.instance.gold -= 600; //��ư�� ������ ���� 3������ ���׷��̵� �ϱ� ���� �ʿ� ��ȭ 600 ����

        }
    }

    private void Up_2L()
    {
        if (Gold >= 350)
        {
            GameManager.instance.gold -= 350; //��ư�� ������ ���� 2������ ���׷��̵� �ϱ� ���� �ʿ� ��ȭ 350 ����

        }
    }

    //�����ε��� ��ų�� �����Ǵ� ����
    public void Aphrodite_Warrior(Vector3 spawnPoint)
    {
        GameObject newObject = Instantiate(Warrior_1L_Obj, spawnPoint, Quaternion.identity);
        GameManager.instance.All_Obj++;
        GameManager.instance.e_population--;
        GameManager.instance.Aobj();

        //����Ʈ�� �߰�
        UnitController unit = newObject.GetComponent<UnitController>();
        
        unit.u_State = UnitController.unitState.Battle;
        RTSUnitController.instance.UnitList.Add(unit);
    }
    public void Aphrodite_Shield(Vector3 spawnPoint)
    {
        GameObject newObject = Instantiate(Shield_1L_Obj, spawnPoint, Quaternion.identity);
        GameManager.instance.All_Obj++;
        GameManager.instance.e_population--;
        GameManager.instance.Aobj();

        //����Ʈ�� �߰�
        UnitController unit = newObject.GetComponent<UnitController>();
        unit.u_State = UnitController.unitState.Battle;
        RTSUnitController.instance.UnitList.Add(unit);
    }
    public void Aphrodite_Archer(Vector3 spawnPoint)
    {
        GameObject newObject = Instantiate(Archer_1L_Obj, spawnPoint, Quaternion.identity);
        GameManager.instance.All_Obj++;
        GameManager.instance.e_population--;
        GameManager.instance.Aobj();

        //����Ʈ�� �߰�
        UnitController unit = newObject.GetComponent<UnitController>();
        unit.u_State = UnitController.unitState.Battle;
        RTSUnitController.instance.UnitList.Add(unit);
    }
    public void Aphrodite_HorseMan(Vector3 spawnPoint)
    {
        GameObject newObject = Instantiate(HorseMan_1L_Obj, spawnPoint, Quaternion.identity);
        GameManager.instance.All_Obj++;
        GameManager.instance.e_population--;
        GameManager.instance.Aobj();

        //����Ʈ�� �߰�
        UnitController unit = newObject.GetComponent<UnitController>();
        unit.u_State = UnitController.unitState.Battle;
        RTSUnitController.instance.UnitList.Add(unit);
    }
}
