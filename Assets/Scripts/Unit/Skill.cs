using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public enum Skills
{
    Zeus,       //제우스        1
    Poseidon,   //포세이돈      2
    Hades,      //하데스        3
    Hera,       //헤라          4
    Apollo,     //아폴론        5
    Athena,     //아테나        6
    Aphrodite,  //아프로디테    7
    Hermes,     //헤르메스      8
    Hestia,     //헤스티아      9
    Dionysus,   //디오니소스    10
    Demeter,     //데메테르     11
    Hephaestus, //헤파이토스    12
    Artemis,    //아르테미스    13
    Ares       //아레스         14
}

public class Skill : MonoBehaviour
{
    public static Skill instance;

    float originalDamage; // 능력치 변경 이전의 데미지
    float buffDuration = 5f; // 스킬 지속 시간
    bool isBuffActive = false; // 스킬 활성화 여부
    float buffEndTime; // 스킬 종료 시간

    public GameObject skillRangePrefab; // 범위 표시용 프리팹
    private GameObject skillRangeInstance; // 범위 표시용 인스턴스

    private float skillRangeHeight = 0.1f; // 스킬 범위 높이

    private bool isSkillReady = true; // 스킬 사용 가능한지 여부
    private bool isShowSkillRange = false; //범위 표시여부 

    bool isSkillReady_1 = false;
    bool isSkillReady_2 = false;
    bool isSkillReady_3 = false;

    float skillCooldown_1 = 5.0f; //쿨타임
    float skillCooldown_2 = 10.0f; //쿨타임
    float skillCooldown_3 = 2.0f; //쿨타임
    float currentCooldown_1 = 0.0f; //현재 쿨타임
    float currentCooldown_2 = 0.0f; //현재 쿨타임
    float currentCooldown_3 = 0.0f; //현재 쿨타임

    public GameObject skillNum_1; //제우스 액티브

    public GameObject skillNum_5; //아폴론 액티브  

    public GameObject skillNum_10; //디오니소스 공버프 소모

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

        // 마우스 위치에 스킬 범위를 따라다니도록 업데이트
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

        // 1번 키를 누를 때 스킬 범위를 표시
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

    //스킬 범위 표시
    void ShowSkillRange()
    {
        isShowSkillRange = true;

        // 스킬 범위 표시용 프리팹을 인스턴스화
        skillRangeInstance = Instantiate(skillRangePrefab);
    }

    //스킬 취소
    void CancelSkill()
    {
        isShowSkillRange = false;
        Destroy(skillRangeInstance);
    }

    //스킬사용 위치
    void UpdateSkillRangePosition()
    {
        // 마우스 위치를 기반으로 스킬 범위 인스턴스 이동
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // 바닥에만 충돌했을 때 스킬 범위 이동
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            Vector3 newPosition = hit.point;
            newPosition.y += skillRangeHeight; // Y 좌표 조절
            skillRangeInstance.transform.position = newPosition;
        }
    }

    //스킬 쿨다운
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

            // 쿨다운이 끝나면 스킬을 다시 사용할 수 있게 설정
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

            // 쿨다운이 끝나면 스킬을 다시 사용할 수 있게 설정
            if (currentCooldown_3 <= 0.0f)
            {
                isSkillReady_3 = true;
            }
        }
    }

    //제우스 스킬
    void UseZeusSkill()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            Vector3 spawnPosition = hit.point;
            spawnPosition.y += skillRangeHeight; // Y 좌표 조정

            Instantiate(skillNum_1, spawnPosition, Quaternion.identity);
        }

        currentCooldown_1 = skillCooldown_1; //쿨타임 적용
        isSkillReady_1 = false;
        isShowSkillRange = false;
        SkillCoolDown_1();
        Destroy(skillRangeInstance);
    }

    //아폴론 스킬
    void UseApolloSkill()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            Vector3 spawnPosition = hit.point;
            spawnPosition.y += skillRangeHeight; // Y 좌표 조정

            Instantiate(skillNum_5, spawnPosition, Quaternion.identity);
        }

        currentCooldown_2 = skillCooldown_2; //쿨타임 적용
        isSkillReady_2 = false;
        isShowSkillRange = false;
        SkillCoolDown_1();
        Destroy(skillRangeInstance);
    }

    //공격력 버프 소모 형식
    void UseDionysusSkill()
    {
        foreach (UnitController unit in RTSUnitController.instance.UnitList)
        {
            // 유닛의 월드 좌표를 화면 좌표로 변환해 드래그 범위 내에 있는지 검사
            if (IsUnitInList(unit))
            {
                Debug.Log("리스트 안에 있는 유닛: " + unit.name);
                RTSUnitController.instance.DragSelectUnit(unit);
            }
        }

        currentCooldown_3 = skillCooldown_3; //쿨타임 적용
        isSkillReady_3 = false;
        isShowSkillRange = false;
        SkillCoolDown_3();
        Destroy(skillRangeInstance);
    }

    private bool IsUnitInList(UnitController unit)
    {
        // 유닛이 RTSUnitController.instance.UnitList 안에 있는지 확인하는 코드
        // 만약 리스트 안에 있다면 true를 반환, 그렇지 않으면 false를 반환
        return RTSUnitController.instance.UnitList.Contains(unit);
    }


    // 다른 이벤트 또는 조건에서 스킬을 사용 가능하게 하려면 isSkillReady를 true로 설정하십시오.
    //public void SetSkillReady(bool skillReady)
    //{
    //    isSkillReady = skillReady;
    //}
}
