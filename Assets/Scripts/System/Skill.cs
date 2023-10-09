using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum Skills
{
    Zeus,       //���콺        1 A  o
    Poseidon,   //�����̵�      2 A
    Hades,      //�ϵ���        3 A
    Hera,       //���          4 B
    Apollo,     //������        5 B  o
    Athena,     //���׳�        6 B
    Aphrodite,  //�����ε���    7 B
    Hermes,     //�츣�޽�      8 I
    Hestia,     //�콺Ƽ��      9 I
    Dionysus,   //����ϼҽ�    10 I  o
    Demeter,     //�����׸�     11 I  o
    Hephaestus, //�������佺    12 P  o
    Artemis,    //�Ƹ��׹̽�    13 P  o
    Ares       //�Ʒ���         14 P  o
}

public class Skill : MonoBehaviour
{
    public static Skill instance;

    float originalDamage; // �ɷ�ġ ���� ������ ������
    float originalSpeed; // �ɷ�ġ ���� ������ �ӵ�

    float buffDuration = 5f; // ��ų ���� �ð�
    bool isBuffActive = false; // ��ų Ȱ��ȭ ����
    public int itemLimit = 3; // ��ų Ƚ�� ����

    public GameObject skillRangePrefab; // ���� ǥ�ÿ� ������
    private GameObject skillRangeInstance; // ���� ǥ�ÿ� �ν��Ͻ�

    private float skillRangeHeight = 0.1f; // ��ų ���� ����
    private bool isShowSkillRange = false; //���� ǥ�ÿ��� 

    bool isSkillReady_1 = false;
    bool isSkillReady_2 = false;

    public float currentCooldown_1 = 0.0f; //���� ��Ÿ��
    public float currentCooldown_2 = 0.0f; //���� ��Ÿ��

    public GameObject ZeusSkill; //���콺 ��Ƽ��

    public GameObject ApolloSkill; //������ ��Ƽ��

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        // ���콺 ��ġ�� ��ų ������ ����ٴϵ��� ������Ʈ
        if (isShowSkillRange)
        {
            UpdateSkillRangePosition();
        }

        //���콺 Ŭ��
        if (Input.GetMouseButtonDown(0))
        {
            if (isSkillReady_1 && isShowSkillRange)
            {
                if (Skill_Set.instance.Zeus_S)
                    UseZeusSkill();
                else if (Skill_Set.instance.Poseidon_S)
                    UsePoseidonSkill();
                else if (Skill_Set.instance.Hades_S)
                    UseHadesSkill();
                else { }
            }

            if (isSkillReady_2 && isShowSkillRange)
            {
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
        if (Input.GetKeyDown(KeyCode.Alpha3) && !isBuffActive && itemLimit > 0)
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
        //skillRangeInstance = Instantiate(skillRangePrefab);
        skillRangePrefab.SetActive(true);
    }

    //��ų ���, ��ų�� ����ؼ� ǥ�õǴ� ������ �����Ҷ��� ���
    void CancelSkill()
    {
        isShowSkillRange = false;
        //Destroy(skillRangeInstance);
        skillRangePrefab.SetActive(false);
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
            //skillRangeInstance.transform.position = newPosition;
            skillRangePrefab.transform.position = newPosition;
        }
    }

    //��ų ��ٿ�
    private IEnumerator Num1_Skill_Cooldown(float cooldown)
    {
        currentCooldown_1 = cooldown; //��Ÿ�� ����

        while (currentCooldown_1 >= 0.0f)
        {
            currentCooldown_1 -= Time.deltaTime;
            yield return null;
        }

        currentCooldown_1 = 0f;
        Debug.Log(cooldown + " ��ų ��Ÿ�� ����!");
    }

    private IEnumerator Num2_Skill_Cooldown(float cooldown)
    {
        currentCooldown_2 = cooldown; //��Ÿ�� ����

        while (currentCooldown_2 >= 0.0f)
        {
            currentCooldown_2 -= Time.deltaTime;
            yield return null;
        }

        currentCooldown_2 = 0f;
        Debug.Log(cooldown + " ��ų ��Ÿ�� ����!");
    }

    IEnumerator DeactiveSkill(GameObject skill)
    {
        yield return new WaitForSeconds(3f); // ��Ȱ��ȭ������ ��� �ð�(3��)
        skill.SetActive(false);
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

            ZeusSkill.SetActive(true);
            ZeusSkill.transform.position = spawnPosition;
            //ZeusSkill = Instantiate(skillNum_1, spawnPosition, Quaternion.identity);

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
        isSkillReady_1 = false;

        CancelSkill();
        StartCoroutine(Num1_Skill_Cooldown(5f));
        StartCoroutine(DeactiveSkill(ZeusSkill));
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

            ApolloSkill.SetActive(true);
            ApolloSkill.transform.position = spawnPosition;
            //ApolloSkill = Instantiate(skillNum_5, spawnPosition, Quaternion.identity);

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
        isSkillReady_2 = false;

        CancelSkill();
        StartCoroutine(Num2_Skill_Cooldown(3f)); //��Ÿ�� ����
        StartCoroutine(DeactiveSkill(ApolloSkill));
    }
    void UseAthenaSkill()
    {

    }
    void UseAphroditeSkill()
    {

    }

    //�Ҹ�ų-------------------------------------------------------------------------------------------
    void UseHermesSkill() //�̵��ӵ�
    {
        foreach (UnitController unit in RTSUnitController.instance.UnitList)
        {
            //����Ʈ�ȿ� �ִ� ���� ���ο��� ����
            if (IsUnitInList(unit))
            {
                originalSpeed = unit.umoveSpeed;
                unit.umoveSpeed += 3;

                Transform hermesSkill = unit.transform.GetChild(0);

                hermesSkill.gameObject.SetActive(true);

                StartCoroutine(BuffDelay(unit, originalSpeed, 5.0f, hermesSkill.gameObject));
            }
        }

        itemLimit--;
        isBuffActive = true;
    }

    void UseHestiaSkill() //ȸ��
    {
        foreach (UnitController unit in RTSUnitController.instance.UnitList)
        {
            //����Ʈ�ȿ� �ִ� ���� ���ο��� ����
            if (IsUnitInList(unit))
            {
                unit.uhealth += 20;

                Transform hestiaSkill = unit.transform.GetChild(1);

                hestiaSkill.gameObject.SetActive(true);

                StartCoroutine(BuffDelay(unit, 0, 2.0f, hestiaSkill.gameObject));
            }
        }

        itemLimit--;
        isBuffActive = true;
    }

    void UseDionysusSkill() //���ݷ�
    {
        foreach (UnitController unit in RTSUnitController.instance.UnitList)
        {
            //����Ʈ�ȿ� �ִ� ���� ���ο��� ����
            if (IsUnitInList(unit))
            {
                originalDamage = unit.uattackPower;
                unit.uattackPower += 5;

                Transform dionysusSkill = unit.transform.GetChild(2);

                dionysusSkill.gameObject.SetActive(true);

                StartCoroutine(BuffDelay(unit, originalDamage, 5.0f, dionysusSkill.gameObject));
            }
        }

        itemLimit--;
        isBuffActive = true;
    }

    void UseDemeterSkill() //��ȭ
    {
        GameManager.instance.gold += 100;

        itemLimit--;
    }

    private IEnumerator BuffDelay(UnitController unit, float originalValue, float delayInSeconds, GameObject effectObject)
    {
        yield return new WaitForSeconds(delayInSeconds);

        // ���� ������ �ǵ����ϴ�.
        if (Skill_Set.instance.Hermes_S)
        {
            unit.umoveSpeed = originalValue;
            isBuffActive = false;
        }

        if (Skill_Set.instance.Ares_S)
        {
            unit.uattackPower = originalValue;
            isBuffActive = false;
        }

        effectObject.SetActive(false);
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
