using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public enum Skills
{
    Zeus,       //���콺        1 A
    Poseidon,   //�����̵�      2 A
    Hades,      //�ϵ���        3 A
    Hera,       //���          4 B
    Apollo,     //������        5 B
    Athena,     //���׳�        6 B
    Aphrodite,  //�����ε���    7 B
    Hermes,     //�츣�޽�      8 E
    Hestia,     //�콺Ƽ��      9 E
    Dionysus,   //����ϼҽ�    10 E
    Demeter,     //�����׸�     11 E
    Hephaestus, //�������佺    12 P
    Artemis,    //�Ƹ��׹̽�    13 P
    Ares       //�Ʒ���         14 P
}

public class Skill : MonoBehaviour
{
    //public static Skill instance;

    float originalDamage; // �ɷ�ġ ���� ������ ������
    float buffDuration = 5f; // ��ų ���� �ð�
    bool isBuffActive = false; // ��ų Ȱ��ȭ ����
    int buffLimit = 3; // ��ų Ƚ�� ����

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
    float currentCooldown_1 = 0.0f; //���� ��Ÿ��
    float currentCooldown_2 = 0.0f; //���� ��Ÿ��

    public GameObject skillNum_1; //���콺 ��Ƽ��
    private GameObject ZeusSkill;

    public GameObject skillNum_5; //������ ��Ƽ��  
    private GameObject ApolloSkill;

    public GameObject skillNum_10; //����ϼҽ� ������ �Ҹ�
    private GameObject DionysusSkill;

    public GameObject skills { get; set; }

    void Awake()
    {
        //instance = this;
    }

    void Start()
    {
    }

    void Update()
    {
        SkillCoolDown();

        // ���콺 ��ġ�� ��ų ������ ����ٴϵ��� ������Ʈ
        if (skillRangeInstance != null)
        {
            UpdateSkillRangePosition();
        }
        //���콺 Ŭ��
        if (Input.GetMouseButtonDown(0) && skillRangeInstance != null)
        {
            if (isSkillReady_1 && skillRangeInstance != null && isShowSkillRange)
                if (Skill_Set.instance.Zeus_S)
                    UseZeusSkill();
                else if (Skill_Set.instance.Poseidon_S)
                    UsePoseidonSkill();
                else if (Skill_Set.instance.Hades_S)
                    UseHadesSkill();
                else { }
            if (isSkillReady_2 && skillRangeInstance != null && isShowSkillRange)
                if (Skill_Set.instance.Hera_S)
                    UseHeraSkill();
                else if (Skill_Set.instance.Apollo_S)
                    UseApolloSkill();
                else if (Skill_Set.instance.Athena_S)
                    UseAthenaSkill();
                else if (Skill_Set.instance.Aphrodite_S)
                    UseAphroditeSkill();
                else { }
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
        // 2�� Ű�� ���� �� ��ų ������ ǥ��
        if (Input.GetKeyDown(KeyCode.Alpha2) && currentCooldown_2 <= 0f)
        {

            if (isShowSkillRange)
            {
                isSkillReady_2 = false;
                CancelSkill();
            }
            else
            {
                isSkillReady_2 = true;
                isSkillReady_1 = false;
                ShowSkillRange();
            }
        }
        // 3�� Ű�� ������ 3�� ��ų ���
        if (Input.GetKeyDown(KeyCode.Alpha3) && !isBuffActive && buffLimit > 0)
        {
            if (Skill_Set.instance.Hermes_S)
                UseHermesSkill();
            else if (Skill_Set.instance.Hestia_S)
                UseHestiaSkill();
            else if (Skill_Set.instance.Dionysus_S)
                UseDionysusSkill();
            else if (Skill_Set.instance.Demeter_S)
                UseDemeterSkill();
            else { }
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
    void SkillCoolDown()
    {
        if (currentCooldown_1 >= 0.0f)
        {
            currentCooldown_1 -= Time.deltaTime;
        }

        if (currentCooldown_2 >= 0.0f)
        {
            currentCooldown_2 -= Time.deltaTime;
        }
    }

    //1�� ��Ƽ�� ��ų-------------------------------------------------------------------------------------------
    //���콺 ��ų
    void UseZeusSkill()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            Vector3 spawnPosition = hit.point;
            spawnPosition.y += skillRangeHeight; // Y ��ǥ ����

            ZeusSkill = Instantiate(skillNum_1, spawnPosition, Quaternion.identity);

            Collider[] colliders = Physics.OverlapSphere(spawnPosition, 4.5f, LayerMask.GetMask("E_Unit"));
            foreach (Collider collider in colliders)
            {
                E_unitMove E_unit = collider.GetComponent<E_unitMove>();
                if (E_unit != null)
                {
                    // ��ų�� ���� ������ ����
                    E_unit.ZuesDamage(20);
                }
            }
        }

        currentCooldown_1 = skillCooldown_1; //��Ÿ�� ����
        isSkillReady_1 = false;
        isShowSkillRange = false;
        Destroy(skillRangeInstance);
        Destroy(ZeusSkill, 7f);
    }
    void UsePoseidonSkill()
    {

    }
    void UseHadesSkill()
    {

    }
    //2�� ��Ƽ�� ��ų-------------------------------------------------------------------------------------------
    void UseHeraSkill()
    {

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

            ApolloSkill = Instantiate(skillNum_5, spawnPosition, Quaternion.identity);

            Collider[] colliders = Physics.OverlapSphere(spawnPosition, 4.5f, LayerMask.GetMask("Unit"));
            foreach (Collider collider in colliders)
            {
                UnitController unit = collider.GetComponent<UnitController>();
                if (unit != null)
                {
                    //���� ����
                    unit.ApolloHeal(50);
                }
            }
        }

        currentCooldown_2 = skillCooldown_2; //��Ÿ�� ����
        isSkillReady_2 = false;
        isShowSkillRange = false;
        Destroy(skillRangeInstance);
        Destroy(ApolloSkill, 1.8f);
    }
    void UseAthenaSkill()
    {

    }
    void UseAphroditeSkill()
    {

    }
    //�Ҹ�ų-------------------------------------------------------------------------------------------
    void UseHermesSkill()
    {

    }

    void UseHestiaSkill()
    {

    }
    //����ϼҽ� ���ݷ� ���� �Ҹ� ����
    void UseDionysusSkill()
    {
        foreach (UnitController unit in RTSUnitController.instance.UnitList)
        {
            //����Ʈ�ȿ� �ִ� ���� ���ο��� ����
            if (IsUnitInList(unit))
            {
                originalDamage = unit.uattackPower;
                unit.uattackPower += 5;

                Vector3 unitPosition = unit.transform.position;
                Transform unitChild = unit.transform.GetChild(0);
                Vector3 spawnPosition = unitChild.position;
                DionysusSkill = Instantiate(skillNum_10, spawnPosition, Quaternion.identity);
                DionysusSkill.transform.parent = unitChild;

                StartCoroutine(RevertVariableAfterDelay(unit, originalDamage, 5.0f));
                Destroy(DionysusSkill, 5f);
            }
        }

        buffLimit--;
        isBuffActive = true;
        isShowSkillRange = false;
        Destroy(skillRangeInstance);
    }

    void UseDemeterSkill()
    {

    }

    private IEnumerator RevertVariableAfterDelay(UnitController unit, float originalValue, float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);

        // ���� ������ �ǵ����ϴ�.
        unit.uattackPower = originalValue;
        isBuffActive = false;
    }

    //���� Ȯ��------------------------------------------------------------------------------------------
    //���ֵ��� ����Ʈ�� �ִ��� Ȯ��
    private bool IsUnitInList(UnitController unit)
    {
        return RTSUnitController.instance.UnitList.Contains(unit);
    }


    // �ٸ� �̺�Ʈ �Ǵ� ���ǿ��� ��ų�� ��� �����ϰ� �Ϸ��� isSkillReady�� true�� �����Ͻʽÿ�.
    //public void SetSkillReady(bool skillReady)
    //{
    //    isSkillReady = skillReady;
    //}
}
