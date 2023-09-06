using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public GameObject skillRangePrefab; // ���� ǥ�ÿ� ������
    private GameObject skillRangeInstance; // ���� ǥ�ÿ� �ν��Ͻ�

    private float skillRangeHeight = 0.1f; // ��ų ���� ����

    private bool isSkillReady = true; // ��ų ��� �������� ����
    private bool isShowSkillRange = false; //���� ǥ�ÿ��� 

    private float skillCooldown = 5.0f; //��Ÿ��
    private float currentCooldown = 0.0f; //���� ��Ÿ��

    public GameObject skillNum_1; //����

    void Update()
    {
        // 1�� Ű�� ���� �� ��ų ������ ǥ��
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (isSkillReady)
            {
                if (isShowSkillRange)
                {
                    // ��ų�� ����ϰ� ��ų ���� ����
                    CancelSkill();
                }
                else
                {
                    ShowSkillRange();
                }
            }   
        }

        // ��ų ��ٿ� ����
        if (!isSkillReady)
        {
            currentCooldown -= Time.deltaTime;

            // ��ٿ��� ������ ��ų�� �ٽ� ����� �� �ְ� ����
            if (currentCooldown <= 0.0f)
            {
                isSkillReady = true;
            }
        }


        // ���콺 ���� Ŭ�� �� ��ų ���
        if (Input.GetMouseButtonDown(0) && skillRangeInstance != null)
        {
            UseSkill();
        }

        // ���콺 ��ġ�� ��ų ������ ����ٴϵ��� ������Ʈ
        if (skillRangeInstance != null)
        {
            UpdateSkillRangePosition();
        }
    }

    void ShowSkillRange()
    {
        isShowSkillRange = true;

        // ��ų ���� ǥ�ÿ� �������� �ν��Ͻ�ȭ
        skillRangeInstance = Instantiate(skillRangePrefab);
    }

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
            spawnPosition.y += skillRangeHeight; // Y ��ǥ ����

            Instantiate(skillNum_1, spawnPosition, Quaternion.identity);
        }

        currentCooldown = skillCooldown; //��Ÿ�� ����
        isSkillReady = false;
        isShowSkillRange = false;
        Destroy(skillRangeInstance);
    }

    void CancelSkill()
    {
        isShowSkillRange = false;
        Destroy(skillRangeInstance);
    }

    // �ٸ� �̺�Ʈ �Ǵ� ���ǿ��� ��ų�� ��� �����ϰ� �Ϸ��� isSkillReady�� true�� �����Ͻʽÿ�.
    public void SetSkillReady(bool skillReady)
    {
        isSkillReady = skillReady;
    }

}
