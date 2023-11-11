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

    //유닛 내부 이펙트
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
    public float ushieldValue; //보호막 변수

    float time = 3f;    //공격 쿨타임
    public E_unitMove targetUnit;   //공격할 유닛
    public Points point; // 점령중인 거점

    public int unitType; //유닛병종

    public Slider Uslider;
    public float maxhp;
    public Slider Sslider; //
    public float maxS; //
    public GameObject arrow; //화살
    public Transform shotpos; //발사위치
    public ArrowSpawn arrowSpawn;

    //최적화 변수들
    string run = "run";
    string attack = "attack";
    string _point = "Point";
    string reattack = "ReAttack";

    //하데스 변수
    public bool isHades = false;

    public enum unitState //유닛상태머신
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
        StartCoroutine(Pcheck());   //유닛 hp체크후 죽는 코루틴함수
        playerAnim = GetComponent<Animator>();
        maxhp = uhealth;
        maxS = ushieldValue; //
        maxS = maxhp;
        if(Skill_Set.instance.Poseidon_S)
        {
            StartCoroutine(Shieldcheck()); //보호막 체크
        }
    }

    private void FixedUpdate()
    {
        time += Time.deltaTime;
        navMeshAgent.speed = umoveSpeed;

        Uslider.value = uhealth / maxhp;
        Sslider.value = ushieldValue / maxS; //

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
        Unit_AttackRange enemylist = P_attackRange.GetComponent<Unit_AttackRange>();
        enemylist.targets.Clear();
        enemylist.e_unit = null;
        targetUnit = null;
        
        if(uhealth>0)
        {
            navMeshAgent.SetDestination(end);
            P_attackRange.SetActive(false);
            Invoke("ReAttack", 3f);
            //if (!P_attackRange)
            //{

            //    Invoke("ReAttack", 3f);
            //}
        }
    }

    //void Update()
    //{
    //    time += Time.deltaTime;

    //    switch (u_State)
    //    {
    //        case unitState.Idle:
    //            U_Idle();
    //            break;
    //        case unitState.goPoint:
    //            U_GoPoint();
    //            break;
    //    }
    //}

    void RemoveList()
    {
        RTSUnitController.instance.UnitList.Remove(this);
        RTSUnitController.instance.selectedUnitList.Remove(this);

        //Destroy(gameObject);
    }

    void U_Idle()   //유닛상태함수 가만히있을때
    {

        targetUnit = null;
        navMeshAgent.isStopped = false;

        //if (time > 2 && uhealth <= 0)
        //{
        //    time = 0;
        //    StopAllCoroutines();
        //}
    }

    void U_GoPoint()    //유닛상태함수
    {

    }


    public void Attack(Vector3 dir, E_unitMove e_unit)  //적 유닛 인식
    {
        if (uhealth <= 0)
            return;

        if (e_unit.ehealth > 0)
        {
            targetUnit = e_unit;
            Find_Target(dir, targetUnit);
        }
    }

    void Find_Target(Vector3 dir, E_unitMove e_unit)    //공격할 유닛 지정
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

        if (unitType == 1 && Vector3.Distance(transform.position, dir) <= 15f && e_unit.ehealth > 0)
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

    IEnumerator Damage(Vector3 dir, E_unitMove e_unit)  //적 공격
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

            if (unitType == 1)
            {
                arrowSpawn = EnemySpawn.instance.arrowPool.GetComponent<ArrowSpawn>();

                for (int i = 0; i < arrowSpawn._arrow1.Length; i++)
                {
                    if (!arrowSpawn._arrow1[i].activeSelf)
                    {
                        arrowSpawn._arrow2[i].SetActive(true);
                        arrowSpawn._arrow2[i].transform.position = shotpos.position;

                        arrowSpawn._arrow2[i].GetComponent<Arrow>().target = dir;

                        break;
                    }
                }
            }

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

    private void OnTriggerEnter(Collider other) //점령지확인
    {
        if (other.CompareTag(_point))
        {
            point = other.GetComponent<Points>();
        }
    }
    private void OnTriggerExit(Collider other)  //점령지 초기화
    {
        if (other.CompareTag(_point))
        {
            point.p_distance = 100f;
        }
    }

    IEnumerator Pcheck()    //유닛 죽는 코루틴 함수
    {
        WaitForSeconds wait = new WaitForSeconds(0.5f);

        if (uhealth <= 0 && isHades)
        {
            Hades.SetActive(false);
            uhealth = maxhp / 2;
            isHades = false;
            StopCoroutine("HadesDuration");
            //하데스 코루틴 중단 이거말고 다른방식으로 스탑하고싶은데 임시로 이거 넣었습니다
        }

        if (uhealth <= 0 && !isHades)
        {
            navMeshAgent.isStopped = true;
            navMeshAgent.velocity = Vector3.zero;
            P_attackRange.SetActive(false);
            targetUnit = null;

            RTSUnitController.instance.UnitList.Remove(this);
            RTSUnitController.instance.selectedUnitList.Remove(this);

            EnemySpawn.instance.gold += 10; //아군 유닛 죽였을 때 적 재화 획득
            EnemySpawn.instance.etotal_gold += 10;

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
        playerAnim.SetTrigger("hit");

        if (ushieldValue >= damage)
        {
            ushieldValue -= damage;
        }
        else if (ushieldValue < damage)
        {
            uhealth += ushieldValue - damage;

            ushieldValue = 0;
        }
        else
            uhealth -= damage;
    }

    public void ApolloHeal(float heal)
    {
        uhealth += heal;

        if (uhealth > maxhp)
            uhealth = maxhp;
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
        P_attackRange.SetActive(false);
        targetUnit = null;
        navMeshAgent.isStopped = true;
        navMeshAgent.velocity = Vector3.zero;

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

    IEnumerator AttaRangeReset()
    {
        P_attackRange.SetActive(false);
        yield return new WaitForSeconds(2f);
        P_attackRange.SetActive(true);
    }

    private void OnEnable()
    {
        //1렙유닛
        if (unitnumber == 0)
        {
            unitType = 0;
            t_State = typeState.Sword;
            uhealth = GameManager.instance.health;
            uattackPower = GameManager.instance.attackPower;
            umoveSpeed = GameManager.instance.moveSpeed;
        }
        if (unitnumber == 1) //방패
        {
            unitType = 0;
            t_State = typeState.Shield;
            uhealth = GameManager.instance.health + 30;
            uattackPower = GameManager.instance.attackPower - 3;
            umoveSpeed = GameManager.instance.moveSpeed - 1;
        }
        if (unitnumber == 2) //궁수
        {
            unitType = 1;
            t_State = typeState.Archer;
            uhealth = GameManager.instance.health - 20;
            uattackPower = GameManager.instance.attackPower + 5;
            umoveSpeed = GameManager.instance.moveSpeed + 1;
        }
        if (unitnumber == 3) //기마
        {
            unitType = 0;
            t_State = typeState.Horse;
            uhealth = GameManager.instance.health + 130;
            uattackPower = GameManager.instance.attackPower + 7;
            umoveSpeed = GameManager.instance.moveSpeed + 3;
        }

        //2렙유닛
        if (unitnumber == 4)
        {
            unitType = 0;
            t_State = typeState.Sword;
            uhealth = GameManager.instance.health + 50;
            uattackPower = GameManager.instance.attackPower + 8;
            umoveSpeed = GameManager.instance.moveSpeed;
        }
        if (unitnumber == 5)
        {
            unitType = 0;
            t_State = typeState.Shield;
            uhealth = GameManager.instance.health + 80;
            uattackPower = GameManager.instance.attackPower + 2;
            umoveSpeed = GameManager.instance.moveSpeed - 1;
        }
        if (unitnumber == 6)
        {
            unitType = 1;
            t_State = typeState.Archer;
            uhealth = GameManager.instance.health + 30;
            uattackPower = GameManager.instance.attackPower + 10;
            umoveSpeed = GameManager.instance.moveSpeed + 1;
        }
        if (unitnumber == 7)
        {
            unitType = 0;
            t_State = typeState.Horse;
            uhealth = GameManager.instance.health + 180;
            uattackPower = GameManager.instance.attackPower + 12;
            umoveSpeed = GameManager.instance.moveSpeed + 3;
        }
        //3렙 유닛
        if (unitnumber == 8)
        {
            unitType = 0;
            t_State = typeState.Sword;
            uhealth = GameManager.instance.health + 100;
            uattackPower = GameManager.instance.attackPower + 10;
            umoveSpeed = GameManager.instance.moveSpeed;
        }
        if (unitnumber == 9)
        {
            unitType = 0;
            t_State = typeState.Shield;
            uhealth = GameManager.instance.health + 130;
            uattackPower = GameManager.instance.attackPower + 7;
            umoveSpeed = GameManager.instance.moveSpeed - 1;
        }
        if (unitnumber == 10)
        {
            unitType = 1;
            t_State = typeState.Archer;
            uhealth = GameManager.instance.health + 80;
            uattackPower = GameManager.instance.attackPower + 15;
            umoveSpeed = GameManager.instance.moveSpeed + 1;
        }
        if (unitnumber == 11)
        {
            unitType = 0;
            t_State = typeState.Horse;
            uhealth = GameManager.instance.health + 230;
            uattackPower = GameManager.instance.attackPower + 17;
            umoveSpeed = GameManager.instance.moveSpeed + 3;
        }

        //패시브--------------------------------------------------------------------
        //검사 패시브가 켜지면   
        if (Skill_Set.instance.Ares_S)
        {
            if (unitnumber == 0 || unitnumber == 4 || unitnumber == 8)
            {
                uhealth += uhealth * 0.2f;
                uattackPower += uattackPower * 0.2f;
            }
        }
        //방패병
        if (Skill_Set.instance.Hephaestus_S)
        {
            if (unitnumber == 1 || unitnumber == 5 || unitnumber == 9)
            {
                uhealth += uhealth * 0.2f;
                uattackPower += uattackPower * 0.2f;
            }
        }
        //궁수
        if (Skill_Set.instance.Artemis_S)
        {
            if (unitnumber == 2 || unitnumber == 6 || unitnumber == 10)
            {
                uhealth += uhealth * 0.2f;
                uattackPower += uattackPower * 0.2f;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, starPosition.position, umoveSpeed * Time.deltaTime);
        targetUnit = null;
        StartCoroutine(AttaRangeReset());
    }
}