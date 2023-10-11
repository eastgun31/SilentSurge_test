using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using static E_unitMove;
using static UnityEngine.UI.CanvasScaler;

public class E_unitMove : MonoBehaviour
{
    public GameObject[] points;
    public int unitNum;

    float speed = 7f;
    public float health;
    float time = 0;
    Rigidbody rigid;

    NavMeshAgent moving;
    public Vector3 lastDesti;
    public UnitController targetUnit;

    public float ehealth;
    public float eattackPower;
    public float edefense;
    public float emoveSpeed;

    float original_eshieldValue;
    public float eshieldValue; //보호막 변수

    public int unitType;

    public Slider Eslider;
    public float maxhp;
    public Points point;

    private Animator enemyAnim;

    //최적화 변수들
    string run = "run";
    string attack = "attack";
    string _point = "Point";


    public enum E_UnitState //적 상태머신
    {
        Battle, Idle, goPoint, noBattle, Stun
    }

    public E_UnitState e_State;

    // Start is called before the first frame update
    void Start()
    {
        e_State = E_UnitState.noBattle;
        rigid = GetComponent<Rigidbody>();
        moving = GetComponent<NavMeshAgent>();
        enemyAnim = GetComponent<Animator>();

        maxhp = ehealth;
        StartCoroutine(Pcheck()); //유닛 hp체크후 죽음
        StartCoroutine(usingItem());
    }

    private void FixedUpdate()
    {
        //if (ehealth <= 0)         //현재 미사용인데 혹시몰라 일단 놔둠
        //{
        //    Invoke("E_Die", 3f);
        //}

        Eslider.value = ehealth / maxhp;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        switch (e_State)
        {
            case E_UnitState.Idle:
                E_Idle();
                break;
            case E_UnitState.goPoint:
                E_GoPoint();
                break;
        }

        enemyAnim.SetFloat(run, moving.desiredVelocity.magnitude);
    }

    void E_Idle()
    {
        time = 0;
        moving.isStopped = false;
        e_State = E_UnitState.goPoint;
    }

    void E_GoPoint()
    {
        MovePoint(lastDesti);
        e_State = E_UnitState.noBattle;
    }

    public void MovePoint(Vector3 i)
    {
        lastDesti = i;
        moving.speed = emoveSpeed;
        moving.SetDestination(i);

        //enemyAnim.SetFloat("run", Vector3.Distance(transform.position,i));

        transform.SetParent(null);
    }

    public void Attakc(Vector3 dir, UnitController p_unit)
    {
        if (ehealth <= 0)
            return;

        if (p_unit.uhealth > 0)
        {
            targetUnit = p_unit;
            Find_Target(dir, p_unit);
        }
    }

    void Find_Target(Vector3 dir, UnitController p_unit)
    {
        moving.SetDestination(dir);
        moving.stoppingDistance = 2f;

        if (p_unit.uhealth <= 0)
        {
            targetUnit = null;
        }

        if (unitType == 1 && Vector3.Distance(transform.position, dir) <= 10f && p_unit.uhealth > 0)
        {
            moving.isStopped = true;
            moving.velocity = Vector3.zero;

            StartCoroutine(Damage(dir, p_unit));
        }
        else if (Vector3.Distance(transform.position, dir) <= 3f && p_unit.uhealth > 0)
        {
            moving.isStopped = true;
            moving.velocity = Vector3.zero;

            StartCoroutine(Damage(dir, p_unit));
        }
        else if (Vector3.Distance(transform.position, dir) > 3f)
        {
            moving.isStopped = false;
            moving.SetDestination(dir);
            moving.stoppingDistance = 2f;
            //enemyAnim.SetFloat("run", attackspeed);
        }
    }

    IEnumerator Damage(Vector3 dir, UnitController p_unit)
    {
        WaitForSeconds wait = new WaitForSeconds(1f);

        if (EnemySkillManager.instance.useSkill)
        {
            EnemySkillManager.instance.E_UseSkill(dir, gameObject.transform.position);
            //EnemySkillManager.instance.useSkill = false;
        }

        if (p_unit.uhealth > 0 && time > 2f && e_State == E_UnitState.Battle)
        {
            time = 0;
            transform.LookAt(dir);
            enemyAnim.SetTrigger(attack);

            if (p_unit.ushieldValue > 0)
            {
                p_unit.ushieldValue -= eattackPower;
            }
            else if (p_unit.ushieldValue < 0)
            {
                p_unit.uhealth += p_unit.ushieldValue;

                p_unit.ushieldValue = 0;
            }
            else
                p_unit.uhealth -= eattackPower;

            Debug.Log("공격");

            yield return wait;

            StartCoroutine(Damage(dir, p_unit));
        }

        if (p_unit.uhealth <= 0 && ehealth > 0)
        {
            e_State = E_UnitState.Idle;
            targetUnit = null;
            Debug.Log("적 죽음");

            if (targetUnit == null && ehealth > 0)
            {
                Debug.Log("다시 출발");
                moving.isStopped = false;
                //enemyAnim.SetFloat("run", Vector3.Distance(transform.position, lastDesti));
                moving.SetDestination(lastDesti);
            }

            StopCoroutine("Damage");
        }
    }

    void E_Die()
    {
        moving.isStopped = true;
        moving.velocity = Vector3.zero;

        GameManager.instance.e_population--;
        GameManager.instance.gold += 2;

        if (point)
        {
            point.e_distance = 100f;
        }

        //enemyAnim.SetTrigger("death");

        Destroy(gameObject);
    }

    private void OnEnable()
    {
        if (unitNum == 0)
        {
            unitType = 0;
            ehealth = GameManager.instance.health;
            eattackPower = GameManager.instance.attackPower;
            edefense = GameManager.instance.defense;
            emoveSpeed = GameManager.instance.moveSpeed;
        }
        if (unitNum == 1)
        {
            unitType = 0;
            ehealth = GameManager.instance.health + 50;
            eattackPower = GameManager.instance.attackPower - 2;
            edefense = GameManager.instance.defense + 2;
            emoveSpeed = GameManager.instance.moveSpeed - 2;
        }
        if (unitNum == 2)
        {
            unitType = 1;
            ehealth = GameManager.instance.health - 20;
            eattackPower = GameManager.instance.attackPower + 3;
            edefense = GameManager.instance.defense - 1;
            emoveSpeed = GameManager.instance.moveSpeed + 1;
        }
        if (unitNum == 3)
        {
            unitType = 0;
            ehealth = GameManager.instance.health + 100;
            eattackPower = GameManager.instance.attackPower + 5;
            edefense = GameManager.instance.defense + 5;
            emoveSpeed = GameManager.instance.moveSpeed + 3;
        }

        if (unitNum == 4)
        {
            unitType = 0;
            ehealth = GameManager.instance.health + 50;
            eattackPower = GameManager.instance.attackPower + 5;
            edefense = GameManager.instance.defense + 5;
            emoveSpeed = GameManager.instance.moveSpeed;
        }
        if (unitNum == 5)
        {
            unitType = 0;
            ehealth = GameManager.instance.health + 100;
            eattackPower = GameManager.instance.attackPower + 3;
            edefense = GameManager.instance.defense + 7;
            emoveSpeed = GameManager.instance.moveSpeed - 2;
        }
        if (unitNum == 6)
        {
            unitType = 1;
            ehealth = GameManager.instance.health + 30;
            eattackPower = GameManager.instance.attackPower + 8;
            edefense = GameManager.instance.defense + 4;
            emoveSpeed = GameManager.instance.moveSpeed + 1;
        }
        if (unitNum == 7)
        {
            unitType = 0;
            ehealth = GameManager.instance.health + 150;
            eattackPower = GameManager.instance.attackPower + 10;
            edefense = GameManager.instance.defense + 10;
            emoveSpeed = GameManager.instance.moveSpeed + 3;
        }

        if (unitNum == 8)
        {
            unitType = 0;
            ehealth = GameManager.instance.health + 100;
            eattackPower = GameManager.instance.attackPower + 10;
            edefense = GameManager.instance.defense + 10;
            emoveSpeed = GameManager.instance.moveSpeed;
        }
        if (unitNum == 9)
        {
            unitType = 0;
            ehealth = GameManager.instance.health + 150;
            eattackPower = GameManager.instance.attackPower + 8;
            edefense = GameManager.instance.defense + 12;
            emoveSpeed = GameManager.instance.moveSpeed - 2;
        }
        if (unitNum == 10)
        {
            unitType = 1;
            ehealth = GameManager.instance.health + 80;
            eattackPower = GameManager.instance.attackPower + 13;
            edefense = GameManager.instance.defense + 9;
            emoveSpeed = GameManager.instance.moveSpeed + 1;
        }
        if (unitNum == 11)
        {
            unitType = 0;
            ehealth = GameManager.instance.health + 200;
            eattackPower = GameManager.instance.attackPower + 15;
            edefense = GameManager.instance.defense + 15;
            emoveSpeed = GameManager.instance.moveSpeed + 3;
        }
    }

    IEnumerator Pcheck()
    {
        WaitForSeconds wait = new WaitForSeconds(0.5f);

        if (ehealth <= 0)
        {
            moving.isStopped = true;
            moving.velocity = Vector3.zero;

            GameManager.instance.e_population--;
            GameManager.instance.gold += 2;

            enemyAnim.SetTrigger("death");

            yield return new WaitForSeconds(3f);

            if (point)
            {
                point.e_distance = 100f;
            }

            Destroy(gameObject);

            StopCoroutine(Pcheck());
        }

        yield return wait;
        StartCoroutine(Pcheck());
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_point))
        {
            point = other.GetComponent<Points>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_point))
        {
            point.e_distance = 100f;
        }
    }

    public void ZeusDamage(float damage)
    {
        ehealth -= damage;
    }

    public void ApolloHeal(float heal)
    {
        ehealth += heal;
    }

    public IEnumerator HeraStun(float sec)
    {
        e_State = E_UnitState.Stun;

        Transform heraStun = transform.GetChild(5);
        Transform E_attackRange = transform.GetChild(0); //번호 나중에 바꾸기

        if (e_State == E_UnitState.Stun)
        {
            moving.isStopped = true;
            moving.velocity = Vector3.zero;
        }

        E_attackRange.gameObject.SetActive(false);
        heraStun.gameObject.SetActive(true);

        yield return new WaitForSeconds(sec);

        e_State = E_UnitState.Idle;
        moving.isStopped = false;
        E_attackRange.gameObject.SetActive(true);
        heraStun.gameObject.SetActive(false);
    }

    IEnumerator usingItem()
    {
        WaitForSeconds wait = new WaitForSeconds(1f);

        if (EnemySkillManager.instance.usingItem)
        {
            Transform skillEffect;

            if (EnemySkillManager.instance.e_item_skillnum == 1)
            {
                skillEffect = transform.GetChild(1);
                skillEffect.gameObject.SetActive(true);
                //EnemySkillManager.instance.usingItem = false;

                float originalSpeed = emoveSpeed;
                emoveSpeed += 3;

                yield return new WaitForSeconds(5f);

                skillEffect.gameObject.SetActive(false);
                emoveSpeed = originalSpeed;
            }

            if (EnemySkillManager.instance.e_item_skillnum == 2)
            {
                skillEffect = transform.GetChild(2);
                skillEffect.gameObject.SetActive(true);
                //EnemySkillManager.instance.usingItem = false;

                ehealth += 20;

                yield return new WaitForSeconds(2f);

                skillEffect.gameObject.SetActive(false);
            }

            if (EnemySkillManager.instance.e_item_skillnum == 3)
            {
                skillEffect = transform.GetChild(3);
                skillEffect.gameObject.SetActive(true);
                //EnemySkillManager.instance.usingItem = false;

                float originalDamage = eattackPower;
                eattackPower += 5;

                yield return new WaitForSeconds(5f);

                skillEffect.gameObject.SetActive(false);
                eattackPower = originalDamage;
            }

            EnemySkillManager.instance.usingItem = false;
        }

        yield return wait;

        if (EnemySkillManager.instance.itemLimit == 0)
        {
            StopCoroutine("usingItem");
        }

        StartCoroutine(usingItem());
    }
}
