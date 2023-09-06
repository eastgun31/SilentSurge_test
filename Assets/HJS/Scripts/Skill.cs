using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public GameObject skillRangePrefab; // 범위 표시용 프리팹
    private GameObject skillRangeInstance; // 범위 표시용 인스턴스

    private float skillRangeHeight = 0.1f; // 스킬 범위 높이

    private bool isSkillReady = true; // 스킬 사용 가능한지 여부
    private bool isShowSkillRange = false; //범위 표시여부 

    private float skillCooldown = 5.0f; //쿨타임
    private float currentCooldown = 0.0f; //현재 쿨타임

    public GameObject skillNum_1; //번개

    void Update()
    {
        // 1번 키를 누를 때 스킬 범위를 표시
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (isSkillReady)
            {
                if (isShowSkillRange)
                {
                    // 스킬을 취소하고 스킬 범위 제거
                    CancelSkill();
                }
                else
                {
                    ShowSkillRange();
                }
            }   
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


        // 마우스 왼쪽 클릭 시 스킬 사용
        if (Input.GetMouseButtonDown(0) && skillRangeInstance != null)
        {
            UseSkill();
        }

        // 마우스 위치에 스킬 범위를 따라다니도록 업데이트
        if (skillRangeInstance != null)
        {
            UpdateSkillRangePosition();
        }
    }

    void ShowSkillRange()
    {
        isShowSkillRange = true;

        // 스킬 범위 표시용 프리팹을 인스턴스화
        skillRangeInstance = Instantiate(skillRangePrefab);
    }

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

    void UseSkill()
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
        isShowSkillRange = false;
        Destroy(skillRangeInstance);
    }

    void CancelSkill()
    {
        isShowSkillRange = false;
        Destroy(skillRangeInstance);
    }

    // 다른 이벤트 또는 조건에서 스킬을 사용 가능하게 하려면 isSkillReady를 true로 설정하십시오.
    public void SetSkillReady(bool skillReady)
    {
        isSkillReady = skillReady;
    }

}
