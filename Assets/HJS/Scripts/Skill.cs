using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public enum Skills
{
    Zeus,       //제우스
    Poseidon,   //포세이돈
    Hades,      //하데스
    Hera,       //헤라
    Apollo,     //아폴론
    Athena,     //아테나
    Aphrodite,  //아프로디테
    Hephaestus, //헤파이토스
    Artemis,    //아르테미스
    Ares,       //아레스
    Hermes,     //헤르메스
    Hestia,     //헤스티아
    Dionysus,   //디오니소스
    Demeter     //데메테르
}

public class Skill : MonoBehaviour
{
    public GameObject skillRangePrefab; // 범위 표시용 프리팹
    private GameObject skillRangeInstance; // 범위 표시용 인스턴스

    private float skillRangeHeight = 0.1f; // 스킬 범위 높이

    private bool isSkillReady = true; // 스킬 사용 가능한지 여부
    private bool isShowSkillRange = false; //범위 표시여부 

    bool isSkillReady_1 = false;
    bool isSkillReady_2 = false;

    private float skillCooldown = 5.0f; //쿨타임
    private float currentCooldown = 0.0f; //현재 쿨타임

    public GameObject skillNum_1; //제우스 액티브

    public GameObject skillNum_5; //아폴론 액티브

    public GameObject skillNum_8; //헤파이토스 방패병강화 패시브

    public GameObject skillNum_11; //헤르메스 이속강화 소모

    public GameObject skills { get; set; }

    void Update()
    {
        // 1번 키를 누를 때 스킬 범위를 표시
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (isSkillReady)
            {
                if (isShowSkillRange)
                {
                    isSkillReady_1 = false;
                    CancelSkill();
                }
                else
                {
                    isSkillReady_1 = true;
                    ShowSkillRange();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (isSkillReady)
            {
                if (isShowSkillRange)
                {
                    isSkillReady_2 = false;
                    CancelSkill();
                }
                else
                {
                    isSkillReady_2 = true;
                    ShowSkillRange();
                }
            }

        }

        if (Input.GetMouseButtonDown(0) && skillRangeInstance != null)
        {
            if (isSkillReady_1 && skillRangeInstance != null)
                UseZeusSkill();

            if (isSkillReady_2 && skillRangeInstance != null)
                UseApolloSkill();
        }
        // 스킬 쿨다운 갱신
        if (!isSkillReady)
        {
            currentCooldown -= Time.deltaTime;

            // 쿨다운이 끝나면 스킬을 다시 사용할 수 있게 설정
            if (currentCooldown <= 0.0f)
            {
                isSkillReady = true;
            }
        }

        // 마우스 위치에 스킬 범위를 따라다니도록 업데이트
        if (skillRangeInstance != null)
        {
            UpdateSkillRangePosition();
        }
    }

    //스킬 범위 표시
    void ShowSkillRange()
    {
        isShowSkillRange = true;

        // 스킬 범위 표시용 프리팹을 인스턴스화
        skillRangeInstance = Instantiate(skillRangePrefab);
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

    //스킬 취소
    void CancelSkill()
    {
        isShowSkillRange = false;
        Destroy(skillRangeInstance);
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

        currentCooldown = skillCooldown; //쿨타임 적용
        isSkillReady = false;
        isSkillReady_1 = false;
        isShowSkillRange = false;
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

        currentCooldown = skillCooldown; //쿨타임 적용
        isSkillReady = false;
        isSkillReady_2 = false;
        isShowSkillRange = false;
        Destroy(skillRangeInstance);
    }







    // 다른 이벤트 또는 조건에서 스킬을 사용 가능하게 하려면 isSkillReady를 true로 설정하십시오.
    public void SetSkillReady(bool skillReady)
    {
        isSkillReady = skillReady;
    }
}
