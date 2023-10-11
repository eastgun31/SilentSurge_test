using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections;

public class UnitController : MonoBehaviour
{
    [SerializeField]
    private GameObject unitMarker;
    private NavMeshAgent navMeshAgent;
    private Animator playerAnim;

    public int unitnumber = 0;

    public float uhealth;
    public float uattackPower;
    public float udefense;
    public float umoveSpeed;

    float original_ushieldValue;
    public float ushieldValue; //��ȣ�� ����

    float time = 3f;    //���� ��Ÿ��
    public E_unitMove targetUnit;   //������ ����
    public Points point; // �������� ����

    public int unitType; //���ֺ���

    public Slider Uslider;
    public float maxhp;
    public Slider Sslider; //
    public float maxS; //
    

    //����ȭ ������
    string run = "run";
    string attack = "attack";
    string _point = "Point";

    public enum unitState //���ֻ��¸ӽ�
    {
        Battle, Idle, goPoint
    }

    public unitState u_State;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        StartCoroutine(Pcheck());   //���� hpüũ�� �״� �ڷ�ƾ�Լ�
        playerAnim = GetComponent<Animator>();
        maxhp = uhealth;
        maxS = ushieldValue; //
        maxS = maxhp;

        StartCoroutine(Shieldcheck()); //��ȣ�� üũ
    }

    private void FixedUpdate()
    {
        navMeshAgent.speed = umoveSpeed;

        Uslider.value = uhealth / maxhp;
        Sslider.value = ushieldValue / maxS; //


        //if (uhealth <= 0)             //���� �̻���ε� Ȥ�ø��� �ϴ� ����
        //{
        //    Invoke("P_Die", 3f);
        //}

    }


    public void SelectUnit()
    {
        unitMarker.SetActive(true);
    }

    public void DeselectUnit()
    {
        unitMarker.SetActive(false);
    }

    public void MoveTo(Vector3 end)
    {
        //playerAnim.SetFloat("run", navMeshAgent.velocity.magnitude);
        navMeshAgent.SetDestination(end);
    }

    void Update()
    {
        time += Time.deltaTime;

        switch (u_State)
        {
            case unitState.Idle:
                U_Idle();
                break;
            case unitState.goPoint:
                U_GoPoint();
                break;
        }

        playerAnim.SetFloat(run, navMeshAgent.velocity.magnitude);

    }

    void RemoveList()
    {
        RTSUnitController.instance.UnitList.Remove(this);
        RTSUnitController.instance.selectedUnitList.Remove(this);

        //Destroy(gameObject);
    }

    void U_Idle()   //���ֻ����Լ� ������������
    {
        time = 0;

        targetUnit = null;
        navMeshAgent.isStopped = false;

        if (time > 2)
        {
            time = 0;
            StopAllCoroutines();
        }
    }

    void U_GoPoint()    //���ֻ����Լ�
    {

    }


    public void Attack(Vector3 dir, E_unitMove e_unit)  //�� ���� �ν�
    {
        if (uhealth <= 0)
            return;

        if (e_unit.ehealth > 0)
        {
            targetUnit = e_unit;
            Find_Target(dir, e_unit);
        }
    }

    void Find_Target(Vector3 dir, E_unitMove e_unit)    //������ ���� ����
    {
        navMeshAgent.SetDestination(dir);
        navMeshAgent.stoppingDistance = 2f;

        if (e_unit.ehealth <= 0)
        {
            targetUnit = null;
        }

        if (unitType == 1 && Vector3.Distance(transform.position, dir) <= 10f && e_unit.ehealth > 0)
        {
            navMeshAgent.isStopped = true;
            navMeshAgent.velocity = Vector3.zero;

            StartCoroutine(Damage(dir, e_unit));
        }
        else if (Vector3.Distance(transform.position, dir) <= 3f && e_unit.ehealth > 0)
        {
            navMeshAgent.isStopped = true;
            navMeshAgent.velocity = Vector3.zero;

            StartCoroutine(Damage(dir, e_unit));
        }
        else if (Vector3.Distance(transform.position, dir) > 3f)
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(dir);
            navMeshAgent.stoppingDistance = 2f;
        }
    }

    IEnumerator Damage(Vector3 dir, E_unitMove e_unit)  //�� ����
    {
        WaitForSeconds wait = new WaitForSeconds(1f);

        if (e_unit.ehealth > 0 && time > 2f && u_State == unitState.Battle)
        {
            time = 0;
            transform.LookAt(dir);
            playerAnim.SetTrigger(attack);

            if (e_unit.eshieldValue > 0)
            {
                e_unit.eshieldValue -= uattackPower;
            }
            else if (e_unit.eshieldValue < 0)
            {
                e_unit.ehealth += e_unit.eshieldValue;

                e_unit.eshieldValue = 0;
            }
            else
                e_unit.ehealth -= uattackPower;


            Debug.Log("�� ����");

            yield return wait;

            StartCoroutine(Damage(dir, e_unit));
        }

        if (e_unit.ehealth <= 0 && uhealth > 0)
        {
            u_State = unitState.Idle;
            targetUnit = null;
            Debug.Log("���� ����");
            navMeshAgent.isStopped = false;
            StopCoroutine("Damage");
        }
    }


    void P_Die()    //�÷��̾� ���� ����
    {
        RTSUnitController.instance.UnitList.Remove(this);
        RTSUnitController.instance.selectedUnitList.Remove(this);

        GameManager.instance.All_Obj--;
        GameManager.instance.Aobj();
        EnemySpawn.instance.gold += 2; //�Ʊ� ���� �׿��� �� �� ��ȭ ȹ��

        if (point)
        {
            point.p_distance = 100f;
        }

        Destroy(gameObject);
    }

    private void OnEnable()
    {
        if (unitnumber == 0)
        {
            unitType = 0;
            uhealth = GameManager.instance.health;
            uattackPower = GameManager.instance.attackPower;
            udefense = GameManager.instance.defense;
            umoveSpeed = GameManager.instance.moveSpeed;
        }
        if (unitnumber == 1)
        {
            unitType = 0;
            uhealth = GameManager.instance.health + 50;
            uattackPower = GameManager.instance.attackPower - 2;
            udefense = GameManager.instance.defense + 2;
            umoveSpeed = GameManager.instance.moveSpeed - 2;
        }
        if (unitnumber == 2)
        {
            unitType = 1;
            uhealth = GameManager.instance.health - 20;
            uattackPower = GameManager.instance.attackPower + 3;
            udefense = GameManager.instance.defense - 1;
            umoveSpeed = GameManager.instance.moveSpeed + 1;
        }
        if (unitnumber == 3)
        {
            unitType = 0;
            uhealth = GameManager.instance.health + 100;
            uattackPower = GameManager.instance.attackPower + 5;
            udefense = GameManager.instance.defense + 5;
            umoveSpeed = GameManager.instance.moveSpeed + 3;
        }

        if (unitnumber == 4)
        {
            unitType = 0;
            uhealth = GameManager.instance.health + 50;
            uattackPower = GameManager.instance.attackPower + 5;
            udefense = GameManager.instance.defense + 5;
            umoveSpeed = GameManager.instance.moveSpeed;
        }
        if (unitnumber == 5)
        {
            unitType = 0;
            uhealth = GameManager.instance.health + 100;
            uattackPower = GameManager.instance.attackPower + 3;
            udefense = GameManager.instance.defense + 7;
            umoveSpeed = GameManager.instance.moveSpeed - 2;
        }
        if (unitnumber == 6)
        {
            unitType = 1;
            uhealth = GameManager.instance.health + 30;
            uattackPower = GameManager.instance.attackPower + 8;
            udefense = GameManager.instance.defense + 4;
            umoveSpeed = GameManager.instance.moveSpeed + 1;
        }
        if (unitnumber == 7)
        {
            unitType = 0;
            uhealth = GameManager.instance.health + 150;
            uattackPower = GameManager.instance.attackPower + 10;
            udefense = GameManager.instance.defense + 10;
            umoveSpeed = GameManager.instance.moveSpeed + 3;
        }


        if (unitnumber == 8)
        {
            unitType = 0;
            uhealth = GameManager.instance.health + 100;
            uattackPower = GameManager.instance.attackPower + 10;
            udefense = GameManager.instance.defense + 10;
            umoveSpeed = GameManager.instance.moveSpeed;
        }
        if (unitnumber == 9)
        {
            unitType = 0;
            uhealth = GameManager.instance.health + 150;
            uattackPower = GameManager.instance.attackPower + 8;
            udefense = GameManager.instance.defense + 12;
            umoveSpeed = GameManager.instance.moveSpeed - 2;
        }
        if (unitnumber == 10)
        {
            unitType = 1;
            uhealth = GameManager.instance.health + 80;
            uattackPower = GameManager.instance.attackPower + 13;
            udefense = GameManager.instance.defense + 9;
            umoveSpeed = GameManager.instance.moveSpeed + 1;
        }
        if (unitnumber == 11)
        {
            unitType = 0;
            uhealth = GameManager.instance.health + 200;
            uattackPower = GameManager.instance.attackPower + 15;
            udefense = GameManager.instance.defense + 15;
            umoveSpeed = GameManager.instance.moveSpeed + 3;
        }

        //�нú�--------------------------------------------------------------------
        //�˻� �нú갡 ������   
        if (Skill_Set.instance.Ares_S)
        {
            if (unitnumber == 0 || unitnumber == 4 || unitnumber == 8)
            {
                uhealth += 30;
                uattackPower += 3;
                udefense += 3;
            }
        }
        //���к�
        if (Skill_Set.instance.Hephaestus_S)
        {
            if (unitnumber == 1 || unitnumber == 5 || unitnumber == 9)
            {
                uhealth += 30;
                uattackPower += 3;
                udefense += 3;
            }
        }
        //�ü�
        if (Skill_Set.instance.Artemis_S)
        {
            if (unitnumber == 2 || unitnumber == 6 || unitnumber == 10)
            {
                uhealth += 30;
                uattackPower += 3;
                udefense += 3;
            }
        }
    }

    private void OnTriggerEnter(Collider other) //������Ȯ��
    {
        if (other.CompareTag(_point))
        {
            point = other.GetComponent<Points>();
        }
    }
    private void OnTriggerExit(Collider other)  //������ �ʱ�ȭ
    {
        if (other.CompareTag(_point))
        {
            point.p_distance = 100f;
        }
    }


    IEnumerator Pcheck()    //���� �״� �ڷ�ƾ �Լ�
    {
        WaitForSeconds wait = new WaitForSeconds(1f);

        if (uhealth <= 0)
        {
            navMeshAgent.isStopped = true;
            navMeshAgent.velocity = Vector3.zero;

            RTSUnitController.instance.UnitList.Remove(this);
            RTSUnitController.instance.selectedUnitList.Remove(this);

            GameManager.instance.All_Obj--;
            GameManager.instance.Aobj();
            EnemySpawn.instance.gold += 2; //�Ʊ� ���� �׿��� �� �� ��ȭ ȹ��


            playerAnim.SetTrigger("death");
            yield return new WaitForSeconds(3f);

            if (point)
            {
                point.p_distance = 100f;
            }

            Destroy(gameObject);
            StopCoroutine(Pcheck());
        }

        yield return wait;
        StartCoroutine(Pcheck());
    }

    public void ZuesDamage(float damage)
    {
        uhealth -= damage;
    }

    public void ApolloHeal(float heal)
    {
        uhealth += heal;
    }

    public void PoseidonShield(int shield)
    {
        ushieldValue += shield;

        StartCoroutine(Delay(original_ushieldValue, 10f));
    }

    IEnumerator Shieldcheck()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);

        if (ushieldValue <= 0)
        {
            Transform poseidonSkill = transform.GetChild(3);
            poseidonSkill.gameObject.SetActive(false);
        }

        yield return wait;
        StartCoroutine(Shieldcheck());
    }

    private IEnumerator Delay(float originalValue, float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);

        ushieldValue = originalValue;

        if (Skill_Set.instance.Poseidon_S)
        {
            Transform poseidonSkill = transform.GetChild(3);
            poseidonSkill.gameObject.SetActive(false);
        }
    }

    public IEnumerator HeraStun(float sec)
    {
        Transform heraStun = transform.GetChild(5);
        Transform E_attackRange = transform.GetChild(0); //��ȣ ���߿� �ٲٱ�

        navMeshAgent.isStopped = true;
        E_attackRange.gameObject.SetActive(false);
        heraStun.gameObject.SetActive(true);

        yield return new WaitForSeconds(sec);

        navMeshAgent.isStopped = false;
        E_attackRange.gameObject.SetActive(true);
        heraStun.gameObject.SetActive(false);
    }

}

