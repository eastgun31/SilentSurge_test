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

    float time = 3f;    //���� ��Ÿ��
    public E_unitMove targetUnit;   //������ ����
    public Points point; // �������� ����

    public Slider Uslider;
    public float maxhp;

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
    }

    private void FixedUpdate()
    {
        navMeshAgent.speed = umoveSpeed;

        Uslider.value = uhealth / maxhp;

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

        if (Vector3.Distance(transform.position, dir) <= 3f && e_unit.ehealth > 0)
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
            e_unit.ehealth -= 10f;
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
            uhealth = GameManager.instance.health;
            uattackPower = GameManager.instance.attackPower;
            udefense = GameManager.instance.defense;
            umoveSpeed = GameManager.instance.moveSpeed;
        }
        if (unitnumber == 1)
        {
            uhealth = GameManager.instance.health + 50;
            uattackPower = GameManager.instance.attackPower - 2;
            udefense = GameManager.instance.defense + 2;
            umoveSpeed = GameManager.instance.moveSpeed - 2;
        }
        if (unitnumber == 2)
        {
            uhealth = GameManager.instance.health - 20;
            uattackPower = GameManager.instance.attackPower + 3;
            udefense = GameManager.instance.defense - 1;
            umoveSpeed = GameManager.instance.moveSpeed + 1;
        }
        if (unitnumber == 3)
        {
            uhealth = GameManager.instance.health + 100;
            uattackPower = GameManager.instance.attackPower + 5;
            udefense = GameManager.instance.defense + 5;
            umoveSpeed = GameManager.instance.moveSpeed + 3;
        }

        if (unitnumber == 4)
        {
            uhealth = GameManager.instance.health + 50;
            uattackPower = GameManager.instance.attackPower + 5;
            udefense = GameManager.instance.defense + 5;
            umoveSpeed = GameManager.instance.moveSpeed;
        }
        if (unitnumber == 5)
        {
            uhealth = GameManager.instance.health + 100;
            uattackPower = GameManager.instance.attackPower + 3;
            udefense = GameManager.instance.defense + 7;
            umoveSpeed = GameManager.instance.moveSpeed - 2;
        }
        if (unitnumber == 6)
        {
            uhealth = GameManager.instance.health + 30;
            uattackPower = GameManager.instance.attackPower + 8;
            udefense = GameManager.instance.defense + 4;
            umoveSpeed = GameManager.instance.moveSpeed + 1;
        }
        if (unitnumber == 7)
        {
            uhealth = GameManager.instance.health + 150;
            uattackPower = GameManager.instance.attackPower + 10;
            udefense = GameManager.instance.defense + 10;
            umoveSpeed = GameManager.instance.moveSpeed + 3;
        }


        if (unitnumber == 8)
        {
            uhealth = GameManager.instance.health + 100;
            uattackPower = GameManager.instance.attackPower + 10;
            udefense = GameManager.instance.defense + 10;
            umoveSpeed = GameManager.instance.moveSpeed;
        }
        if (unitnumber == 9)
        {
            uhealth = GameManager.instance.health + 150;
            uattackPower = GameManager.instance.attackPower + 8;
            udefense = GameManager.instance.defense + 12;
            umoveSpeed = GameManager.instance.moveSpeed - 2;
        }
        if (unitnumber == 10)
        {
            uhealth = GameManager.instance.health + 80;
            uattackPower = GameManager.instance.attackPower + 13;
            udefense = GameManager.instance.defense + 9;
            umoveSpeed = GameManager.instance.moveSpeed + 1;
        }
        if (unitnumber == 11)
        {
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


    public void ApolloHeal(float heal)
    {
        uhealth += heal;
    }
}

