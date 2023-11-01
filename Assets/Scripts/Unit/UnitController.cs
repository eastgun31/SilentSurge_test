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

    //���� ���� ����Ʈ
    public GameObject P_attackRange;
    public GameObject Hermes;
    public GameObject Dionysus;
    public GameObject Poseidon;
    public GameObject Hera;
    public GameObject Hestia;
    public GameObject Hades;

    public int unitnumber = 0;

    public float uhealth;
    public float uattackPower;
    public float udefense;
    public float umoveSpeed;
    public Transform starPosition;

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
    public GameObject arrow; //ȭ��
    public Transform shotpos; //�߻���ġ


    //����ȭ ������
    string run = "run";
    string attack = "attack";
    string _point = "Point";
    string reattack = "ReAttack";

    //�ϵ��� ����
    public bool isHades = false;

    public enum unitState //���ֻ��¸ӽ�
    {
        Battle, Idle, goPoint
    }

    public enum typeState
    {
        Sword, Shield, Archer, Horse
    }

    public unitState u_State;
    public typeState t_State;

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
        if(Skill_Set.instance.Poseidon_S)
        {
            StartCoroutine(Shieldcheck()); //��ȣ�� üũ
        }
    }

    private void FixedUpdate()
    {
        navMeshAgent.speed = umoveSpeed;

        Uslider.value = uhealth / maxhp;
        Sslider.value = ushieldValue / maxS; //
    }

    public void SelectUnit()
    {
        unitMarker.SetActive(true);
    }

    public void DeselectUnit()
    {
        unitMarker.SetActive(false);
    }

    void ReAttack()
    {
        P_attackRange.SetActive(true);
    }

    public void MoveTo(Vector3 end)
    {
        if(uhealth>0)
        {
            Unit_AttackRange enemylist = P_attackRange.GetComponent<Unit_AttackRange>();
            enemylist.targets.Clear();
            P_attackRange.SetActive(false);
            targetUnit = null;
            navMeshAgent.SetDestination(end);
            Invoke(reattack, 2f);
            if (P_attackRange)
            {
                Invoke("ReAttack", 2f);
            }
        }
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

        targetUnit = null;
        navMeshAgent.isStopped = false;

        //if (time > 2 && uhealth <= 0)
        //{
        //    time = 0;
        //    StopAllCoroutines();
        //}
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
            Find_Target(dir, targetUnit);
        }
    }

    void Find_Target(Vector3 dir, E_unitMove e_unit)    //������ ���� ����
    {
        if (uhealth <= 0)
            return;

        if (uhealth > 0)
        {
            navMeshAgent.SetDestination(dir);
            //navMeshAgent.stoppingDistance = 2f;

            if(Vector3.Distance(transform.position, dir) <= 3f)
            {
                navMeshAgent.isStopped = true;
                navMeshAgent.velocity = Vector3.zero;
            }
        }

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

        navMeshAgent.isStopped = true;
        navMeshAgent.velocity = Vector3.zero;
        transform.LookAt(dir);

        if (uhealth > 0 && e_unit.ehealth > 0 && time > 1f && u_State == unitState.Battle)
        {
            time = 0;
            transform.LookAt(dir);
            playerAnim.SetTrigger(attack);

            //if(unitType == 1)
            //{
            //    Instantiate(arrow, shotpos.position, Quaternion.identity);
            //    //arrow.transform.rotation = Quaternion.Euler(-90, 0, 0);
            //    arrow.GetComponent<Arrow>().shotdir = dir;
            //}

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


            yield return wait;

            StartCoroutine(Damage(dir, e_unit));
        }

        if (e_unit.ehealth <= 0 && uhealth > 0)
        {
            u_State = unitState.Idle;
            targetUnit = null;
            navMeshAgent.isStopped = false;
            StopCoroutine("Damage");
        }
    }

    private void OnEnable()
    {
        //1������
        if (unitnumber == 0)
        {
            unitType = 0;
            t_State = typeState.Sword;
            uhealth = GameManager.instance.health;
            uattackPower = GameManager.instance.attackPower;
            udefense = GameManager.instance.defense;
            umoveSpeed = GameManager.instance.moveSpeed;
        }
        if (unitnumber == 1)
        {
            unitType = 0;
            t_State = typeState.Shield;
            uhealth = GameManager.instance.health + 50;
            uattackPower = GameManager.instance.attackPower - 2;
            udefense = GameManager.instance.defense + 2;
            umoveSpeed = GameManager.instance.moveSpeed - 2;
        }
        if (unitnumber == 2)
        {
            unitType = 1;
            t_State = typeState.Archer;
            uhealth = GameManager.instance.health - 20;
            uattackPower = GameManager.instance.attackPower + 3;
            udefense = GameManager.instance.defense - 1;
            umoveSpeed = GameManager.instance.moveSpeed + 1;
        }
        if (unitnumber == 3)
        {
            unitType = 0;
            t_State = typeState.Horse;
            uhealth = GameManager.instance.health + 100;
            uattackPower = GameManager.instance.attackPower + 5;
            udefense = GameManager.instance.defense + 5;
            umoveSpeed = GameManager.instance.moveSpeed + 3;
        }
        //2������
        if (unitnumber == 4)
        {
            unitType = 0;
            t_State = typeState.Sword;
            uhealth = GameManager.instance.health + 50;
            uattackPower = GameManager.instance.attackPower + 5;
            udefense = GameManager.instance.defense + 5;
            umoveSpeed = GameManager.instance.moveSpeed;
        }
        if (unitnumber == 5)
        {
            unitType = 0;
            t_State = typeState.Shield;
            uhealth = GameManager.instance.health + 100;
            uattackPower = GameManager.instance.attackPower + 3;
            udefense = GameManager.instance.defense + 7;
            umoveSpeed = GameManager.instance.moveSpeed - 2;
        }
        if (unitnumber == 6)
        {
            unitType = 1;
            t_State = typeState.Archer;
            uhealth = GameManager.instance.health + 30;
            uattackPower = GameManager.instance.attackPower + 8;
            udefense = GameManager.instance.defense + 4;
            umoveSpeed = GameManager.instance.moveSpeed + 1;
        }
        if (unitnumber == 7)
        {
            unitType = 0;
            t_State = typeState.Horse;
            uhealth = GameManager.instance.health + 150;
            uattackPower = GameManager.instance.attackPower + 10;
            udefense = GameManager.instance.defense + 10;
            umoveSpeed = GameManager.instance.moveSpeed + 3;
        }
        //3�� ����
        if (unitnumber == 8)
        {
            unitType = 0;
            t_State = typeState.Sword;
            uhealth = GameManager.instance.health + 100;
            uattackPower = GameManager.instance.attackPower + 10;
            udefense = GameManager.instance.defense + 10;
            umoveSpeed = GameManager.instance.moveSpeed;
        }
        if (unitnumber == 9)
        {
            unitType = 0;
            t_State = typeState.Shield;
            uhealth = GameManager.instance.health + 150;
            uattackPower = GameManager.instance.attackPower + 8;
            udefense = GameManager.instance.defense + 12;
            umoveSpeed = GameManager.instance.moveSpeed - 2;
        }
        if (unitnumber == 10)
        {
            unitType = 1;
            t_State = typeState.Archer;
            uhealth = GameManager.instance.health + 80;
            uattackPower = GameManager.instance.attackPower + 13;
            udefense = GameManager.instance.defense + 9;
            umoveSpeed = GameManager.instance.moveSpeed + 1;
        }
        if (unitnumber == 11)
        {
            unitType = 0;
            t_State = typeState.Horse;
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

        transform.position = Vector3.MoveTowards(transform.position, starPosition.position, umoveSpeed * Time.deltaTime);
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
        WaitForSeconds wait = new WaitForSeconds(0.5f);

        if (uhealth <= 0 && isHades)
        {
            Hades.SetActive(false);
            uhealth = maxhp / 2;
            isHades = false;
            StopCoroutine("HadesDuration");
            //�ϵ��� �ڷ�ƾ �ߴ� �̰Ÿ��� �ٸ�������� ��ž�ϰ������ �ӽ÷� �̰� �־����ϴ�
        }

        if (uhealth <= 0 && !isHades)
        {
            navMeshAgent.isStopped = true;
            navMeshAgent.velocity = Vector3.zero;
            P_attackRange.SetActive(false);
            targetUnit = null;

            RTSUnitController.instance.UnitList.Remove(this);
            RTSUnitController.instance.selectedUnitList.Remove(this);

            EnemySpawn.instance.gold += 2; //�Ʊ� ���� �׿��� �� �� ��ȭ ȹ��

            playerAnim.SetTrigger("death");
            GameManager.instance.All_Obj--;
            GameManager.instance.Aobj();

            yield return new WaitForSeconds(2f);

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
        playerAnim.SetTrigger("hit");
    }

    public void ApolloHeal(float heal)
    {
        uhealth += heal;
    }

    public void PoseidonShield(int shield)
    {
        ushieldValue += shield;

        StartCoroutine(Delay(original_ushieldValue, 7f));
    }

    IEnumerator Shieldcheck()
    {
        WaitForSeconds wait = new WaitForSeconds(0.5f);

        if (ushieldValue <= 0)
        {
            Poseidon.SetActive(false);
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
            Poseidon.SetActive(false);
        }
    }

    public IEnumerator HeraStun(float sec)
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.velocity = Vector3.zero;
        P_attackRange.SetActive(false);
        targetUnit = null;

        Hera.SetActive(true);

        yield return new WaitForSeconds(sec);

        navMeshAgent.isStopped = false;
        P_attackRange.SetActive(true);
        Hera.SetActive(false);
    }

    public IEnumerator HadesDuration(float duration)
    {
        yield return new WaitForSeconds(duration);

        if (isHades)
        {
            isHades = false;
            Hades.SetActive(false);
        }
    }

    public void AphroditeChange(Vector3 spawnPoint, Vector3 pointPosition)
    {
        StartCoroutine(_AphroditeChange(spawnPoint, pointPosition));
    }

    public IEnumerator _AphroditeChange(Vector3 spawnPoint, Vector3 pointPosition)
    {
        if (unitnumber == 0 || unitnumber == 4 || unitnumber == 8)
            EnemySpawn.instance.Aphrodite_Warrior(spawnPoint, pointPosition);
        else if (unitnumber == 1 || unitnumber == 5 || unitnumber == 9)
            EnemySpawn.instance.Aphrodite_Shield(spawnPoint, pointPosition);
        else if (unitnumber == 2 || unitnumber == 6 || unitnumber == 10)
            EnemySpawn.instance.Aphrodite_Archer(spawnPoint, pointPosition);
        else
            EnemySpawn.instance.Aphrodite_HorseMan(spawnPoint, pointPosition);

        uhealth = 0;

        yield return new WaitForSeconds(0.2f);

        uhealth = 0;

        if (point)
        {
            point.p_distance = 100f;
        }

        RTSUnitController.instance.UnitList.Remove(this);
        RTSUnitController.instance.selectedUnitList.Remove(this);

        Destroy(gameObject);
    }
}