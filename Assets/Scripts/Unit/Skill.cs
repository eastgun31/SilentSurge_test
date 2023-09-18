using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public enum Skills
{
    Zeus,       //���콺        1
    Poseidon,   //�����̵�      2
    Hades,      //�ϵ���        3
    Hera,       //���          4
    Apollo,     //������        5
    Athena,     //���׳�        6
    Aphrodite,  //�����ε���    7
    Hermes,     //�츣�޽�      8
    Hestia,     //�콺Ƽ��      9
    Dionysus,   //����ϼҽ�    10
    Demeter,     //�����׸�     11
    Hephaestus, //�������佺    12
    Artemis,    //�Ƹ��׹̽�    13
    Ares       //�Ʒ���         14
}

public class Skill : MonoBehaviour
{
    public static Skill instance;

    float originalDamage; // �ɷ�ġ ���� ������ ������
    float buffDuration = 5f; // ��ų ���� �ð�
    bool isBuffActive = false; // ��ų Ȱ��ȭ ����
    float buffEndTime; // ��ų ���� �ð�

    public GameObject skillRangePrefab; // ���� ǥ�ÿ� ������
    private GameObject skillRangeInstance; // ���� ǥ�ÿ� �ν��Ͻ�

    private float skillRangeHeight = 0.1f; // ��ų ���� ����

    private bool isSkillReady = true; // ��ų ��� �������� ����
    private bool isShowSkillRange = false; //���� ǥ�ÿ��� 

    bool isSkillReady_1 = false;
    bool isSkillReady_2 = false;
    bool isSkillReady_3 = false;

    float skillCooldown_1 = 5.0f; //��Ÿ��
    float skillCooldown_2 = 10.0f; //��Ÿ��
    float skillCooldown_3 = 2.0f; //��Ÿ��
    float currentCooldown_1 = 0.0f; //���� ��Ÿ��
    float currentCooldown_2 = 0.0f; //���� ��Ÿ��
    float currentCooldown_3 = 0.0f; //���� ��Ÿ��

    public GameObject skillNum_1; //���콺 ��Ƽ��

    public GameObject skillNum_5; //������ ��Ƽ��  

    public GameObject skillNum_10; //����ϼҽ� ������ �Ҹ�

    public bool SelectDemeter = true;

    public bool SelectHephaestus = false;
    public bool SelectArtemis = false;
    public bool SelectAres = true;

    public GameObject skills { get; set; }

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
    }

    void Update()
    {
        SkillCoolDown_1();

        // ���콺 ��ġ�� ��ų ������ ����ٴϵ��� ������Ʈ
        if (skillRangeInstance != null)
        {
            UpdateSkillRangePosition();
        }

        if (Input.GetMouseButtonDown(0) && skillRangeInstance != null)
        {
            if (isSkillReady_1 && skillRangeInstance != null && isShowSkillRange)
                UseZeusSkill();

            if (isSkillReady_2 && skillRangeInstance != null && isShowSkillRange)
                UseApolloSkill();
        }

        // 1�� Ű�� ���� �� ��ų ������ ǥ��
        if (Input.GetKeyDown(KeyCode.Alpha1) && currentCooldown_1 <= 0f)
        {
            if (isShowSkillRange)
            {
                isSkillReady_1 = false;
                CancelSkill();
            }
            else
            {
                isSkillReady_1 = true;
                isSkillReady_2 = false;
                ShowSkillRange();
            }
        }



        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (isSkillReady_2)
            {
                if (isShowSkillRange)
                {
                    CancelSkill();
                }
                else
                {
                    isSkillReady_2 = true;
                    ShowSkillRange();
                }
            }

        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UseDionysusSkill();
        }

    }
    //------------------------------------------------------------------

    //��ų ���� ǥ��
    void ShowSkillRange()
    {
        isShowSkillRange = true;

        // ��ų ���� ǥ�ÿ� �������� �ν��Ͻ�ȭ
        skillRangeInstance = Instantiate(skillRangePrefab);
    }

    //��ų ���
    void CancelSkill()
    {
        isShowSkillRange = false;
        Destroy(skillRangeInstance);
    }

    //��ų��� ��ġ
    void UpdateSkillRangePosition()
    {
        // ���콺 ��ġ�� ������� ��ų ���� �ν��Ͻ� �̵�
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // �ٴڿ��� �浹���� �� ��ų ���� �̵�
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            Vector3 newPosition = hit.point;
            newPosition.y += skillRangeHeight; // Y ��ǥ ����
            skillRangeInstance.transform.position = newPosition;
        }
    }

    //��ų ��ٿ�
    void SkillCoolDown_1()
    {
        if (currentCooldown_1 >= 0.0f)
        {
            currentCooldown_1 -= Time.deltaTime;
        }
    }

    void SkillCoolDown_2()
    {
        if (!isSkillReady_2)
        {
            currentCooldown_2 -= Time.deltaTime;

            // ��ٿ��� ������ ��ų�� �ٽ� ����� �� �ְ� ����
            if (currentCooldown_2 <= 0.0f)
            {
                isSkillReady_2 = true;
            }
        }
    }

    void SkillCoolDown_3()
    {
        if (!isSkillReady_3)
        {
            currentCooldown_3 -= Time.deltaTime;

            // ��ٿ��� ������ ��ų�� �ٽ� ����� �� �ְ� ����
            if (currentCooldown_3 <= 0.0f)
            {
                isSkillReady_3 = true;
            }
        }
    }

    //���콺 ��ų
    void UseZeusSkill()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            Vector3 spawnPosition = hit.point;
            spawnPosition.y += skillRangeHeight; // Y ��ǥ ����

            Instantiate(skillNum_1, spawnPosition, Quaternion.identity);
        }

        currentCooldown_1 = skillCooldown_1; //��Ÿ�� ����
        isSkillReady_1 = false;
        isShowSkillRange = false;
        SkillCoolDown_1();
        Destroy(skillRangeInstance);
    }

    //������ ��ų
    void UseApolloSkill()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            Vector3 spawnPosition = hit.point;
            spawnPosition.y += skillRangeHeight; // Y ��ǥ ����

            Instantiate(skillNum_5, spawnPosition, Quaternion.identity);
        }

        currentCooldown_2 = skillCooldown_2; //��Ÿ�� ����
        isSkillReady_2 = false;
        isShowSkillRange = false;
        SkillCoolDown_1();
        Destroy(skillRangeInstance);
    }

    //���ݷ� ���� �Ҹ� ����
    void UseDionysusSkill()
    {
        foreach (UnitController unit in RTSUnitController.instance.UnitList)
        {
            // ������ ���� ��ǥ�� ȭ�� ��ǥ�� ��ȯ�� �巡�� ���� ���� �ִ��� �˻�
            if (IsUnitInList(unit))
            {
                Debug.Log("����Ʈ �ȿ� �ִ� ����: " + unit.name);
                RTSUnitController.instance.DragSelectUnit(unit);
            }
        }

        currentCooldown_3 = skillCooldown_3; //��Ÿ�� ����
        isSkillReady_3 = false;
        isShowSkillRange = false;
        SkillCoolDown_3();
        Destroy(skillRangeInstance);
    }

    private bool IsUnitInList(UnitController unit)
    {
        // ������ RTSUnitController.instance.UnitList �ȿ� �ִ��� Ȯ���ϴ� �ڵ�
        // ���� ����Ʈ �ȿ� �ִٸ� true�� ��ȯ, �׷��� ������ false�� ��ȯ
        return RTSUnitController.instance.UnitList.Contains(unit);
    }


    // �ٸ� �̺�Ʈ �Ǵ� ���ǿ��� ��ų�� ��� �����ϰ� �Ϸ��� isSkillReady�� true�� �����Ͻʽÿ�.
    //public void SetSkillReady(bool skillReady)
    //{
    //    isSkillReady = skillReady;
    //}
}
