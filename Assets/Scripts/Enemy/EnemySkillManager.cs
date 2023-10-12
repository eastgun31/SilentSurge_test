using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillManager : MonoBehaviour
{
    public static EnemySkillManager instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    public GameObject enemySkills;

    public bool e_Zeus_S ;
    public bool e_Poseidon_S;
    public bool e_Hades_S;

    public bool e_Hephaestus_S;
    public bool e_Artemis_S;
    public bool e_Ares_S;

    public bool e_Hera_S;
    public bool e_Apollo_S;
    public bool e_Athena_S;
    public bool e_Aphrodite_S;

    public bool e_Hermes_S;
    public bool e_Hestia_S;
    public bool e_Dionysus_S;
    public bool e_Demeter_S;

    public int e_active_skillnum;
    public int e_buff_skillnum;
    public int e_item_skillnum;
    public int e_passiveNow;

    public int itemLimit;
    public float currentCooldown_1 = 0.0f; //���� ��Ÿ��
    public float currentCooldown_2 = 0.0f; //���� ��Ÿ��
    public float currentCooldown_3 = 0.0f; //���� ��Ÿ��
    public bool useSkill;
    public bool usingItem;
    public int random;

    public GameObject ZeusSkill; //���콺 ��Ƽ��
    public GameObject PoseidonSkill;
    public GameObject HeraSkill;
    public GameObject ApolloSkill; //������ ��Ƽ��

    // Start is called before the first frame update
    void Start()
    {
        useSkill = true;
        itemLimit = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void E_UseSkill(Vector3 dir, Vector3 dir2)
    {
        //int random;

        if (itemLimit > 0)
            random = Random.Range(0, 3);
        else
            random = Random.Range(0, 2);

        if (random == 0 && currentCooldown_1 == 0f && useSkill)
        {
            switch (e_active_skillnum)
            {
                case 1:
                    UseZeusSkill(dir);
                    break;
                case 2:
                    UsePoseidonSkill(dir2);
                    break;
                case 3:
                    UseHadesSkill();
                    break;
            }
        }

        if (random == 1 && currentCooldown_2 == 0f && useSkill)
        {
            switch (e_buff_skillnum)
            {
                case 1:
                    UseHeraSkill(dir);
                    break;
                case 2:
                    UseApolloSkill(dir2);
                    break;
                case 3:
                    UseAthenaSkill();
                    break;
                case 4:
                    UseAphroditeSkill();
                    break;
            }
        }

        if (random == 2 && currentCooldown_3 == 0f && useSkill)
        {
            switch (e_item_skillnum)
            {
                case 1:
                    UseHermesSkill();
                    break;
                case 2:
                    UseHestiaSkill();
                    break;
                case 3:
                    UseDionysusSkill();
                    break;
                case 4:
                    UseDemeterSkill();
                    break;
            }
        }
    }

    //��ų ��ٿ�
    private IEnumerator Num1_Skill_Cooldown(float cooldown)
    {
        currentCooldown_1 = cooldown; //��Ÿ�� ����

        while (currentCooldown_1 >= 0.0f)
        {
            currentCooldown_1 -= Time.deltaTime;
            useSkill = true;
            yield return null;
        }

        currentCooldown_1 = 0f;
        //Debug.Log(cooldown + " ��ų ��Ÿ�� ����!");
    }

    private IEnumerator Num2_Skill_Cooldown(float cooldown)
    {
        currentCooldown_2 = cooldown; //��Ÿ�� ����

        while (currentCooldown_2 >= 0.0f)
        {
            currentCooldown_2 -= Time.deltaTime;
            useSkill = true;
            yield return null;
        }

        currentCooldown_2 = 0f;
        //Debug.Log(cooldown + " ��ų ��Ÿ�� ����!");
    }

    private IEnumerator Num3_Skill_Cooldown(float cooldown)
    {
        currentCooldown_3 = cooldown; //��Ÿ�� ����

        while (currentCooldown_3 >= 0.0f)
        {
            currentCooldown_3 -= Time.deltaTime;
            useSkill = true;
            yield return null;
        }

        currentCooldown_3 = 0f;
        //Debug.Log(cooldown + " ��ų ��Ÿ�� ����!");
    }

    IEnumerator DeactiveSkill(GameObject skill, float time)
    {
        yield return new WaitForSeconds(time); // ��Ȱ��ȭ������ ��� �ð�(3��)
        skill.SetActive(false);
    }


    //1�� ��Ƽ�� ��ų-------------------------------------------------------------------------------------------
    //���콺 ��ų
    public void UseZeusSkill(Vector3 dir)
    {
        ZeusSkill.SetActive(true);
        ZeusSkill.transform.position = dir;

        Collider[] colliders = Physics.OverlapSphere(dir, 4.5f, LayerMask.GetMask("Unit"));
        foreach (Collider collider in colliders)
        {
            UnitController unit = collider.GetComponent<UnitController>();
            if (unit != null)
            {
                // ��ų�� ���� ������ ����
                unit.ZuesDamage(20);
            }
        }

        useSkill = false;

        StartCoroutine(Num1_Skill_Cooldown(10f));
        StartCoroutine(DeactiveSkill(ZeusSkill, 3f));
    }
    void UsePoseidonSkill(Vector3 dir)
    {
        PoseidonSkill.SetActive(true);
        PoseidonSkill.transform.position = dir;

        Collider[] colliders = Physics.OverlapSphere(dir, 4.5f, LayerMask.GetMask("E_Unit"));
        foreach (Collider collider in colliders)
        {
            E_unitMove e_unit = collider.GetComponent<E_unitMove>();
            if (e_unit != null)
            {
                Transform poseidonSkill = e_unit.transform.GetChild(4);
                poseidonSkill.gameObject.SetActive(true);

                //��ȣ�� ����
                e_unit.PoseidonShield(50);
            }
        }

        useSkill = false;
        StartCoroutine(Num1_Skill_Cooldown(10f));
        StartCoroutine(DeactiveSkill(PoseidonSkill, 4f));
    }
    void UseHadesSkill()
    {
        useSkill = false;
        StartCoroutine(Num1_Skill_Cooldown(5f));
    }
    //2�� ��Ƽ�� ��ų-------------------------------------------------------------------------------------------
    void UseHeraSkill(Vector3 dir)
    {
        HeraSkill.SetActive(true);
        HeraSkill.transform.position = dir;

        Collider[] colliders = Physics.OverlapSphere(dir, 4.5f, LayerMask.GetMask("Unit"));
        foreach (Collider collider in colliders)
        {
            UnitController unit = collider.GetComponent<UnitController>();
            if (unit != null)
            {
                //�ӹ� �ڵ�
                StartCoroutine(unit.HeraStun(3));
            }
        }

        useSkill = false;
        StartCoroutine(Num2_Skill_Cooldown(10f));
        StartCoroutine(DeactiveSkill(HeraSkill, 1f));
    }

    //������ ��ų
    void UseApolloSkill(Vector3 dir)
    {
        ApolloSkill.SetActive(true);
        ApolloSkill.transform.position = dir;

        Collider[] colliders = Physics.OverlapSphere(dir, 4.5f, LayerMask.GetMask("E_Unit"));
        foreach (Collider collider in colliders)
        {
            E_unitMove e_unit = collider.GetComponent<E_unitMove>();
            if (e_unit != null)
            {
                //���� ����
                e_unit.ApolloHeal(50);
            }
        }

        useSkill = false;
        StartCoroutine(Num2_Skill_Cooldown(10f));
        StartCoroutine(DeactiveSkill(ApolloSkill,3f));
    }
    void UseAthenaSkill()
    {
        useSkill = false;
        StartCoroutine(Num2_Skill_Cooldown(3f));
    }
    void UseAphroditeSkill()
    {
        useSkill = false;
        StartCoroutine(Num2_Skill_Cooldown(3f));
    }

    //�Ҹ�ų-------------------------------------------------------------------------------------------
    void UseHermesSkill() //�̵��ӵ�
    {
        useSkill = false;
        usingItem = true;
        StartCoroutine(Num3_Skill_Cooldown(15f));
        itemLimit--;
    }

    void UseHestiaSkill() //ȸ��
    {
        useSkill = false;
        usingItem = true;
        StartCoroutine(Num3_Skill_Cooldown(15f));
        itemLimit--;
    }

    void UseDionysusSkill() //���ݷ�
    {
        useSkill = false;
        usingItem = true;
        StartCoroutine(Num3_Skill_Cooldown(15f));
        itemLimit--;
    }

    void UseDemeterSkill() //��ȭ
    {
        useSkill = false;

        EnemySpawn.instance.gold += 100;

        StartCoroutine(Num3_Skill_Cooldown(15f));
        itemLimit--;
    }

}
