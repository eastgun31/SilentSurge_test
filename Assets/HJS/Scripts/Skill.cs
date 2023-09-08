using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public enum Skills
{
    Zeus,       //���콺
    Poseidon,   //�����̵�
    Hades,      //�ϵ���
    Hera,       //���
    Apollo,     //������
    Athena,     //���׳�
    Aphrodite,  //�����ε���
    Hephaestus, //�������佺
    Artemis,    //�Ƹ��׹̽�
    Ares,       //�Ʒ���
    Hermes,     //�츣�޽�
    Hestia,     //�콺Ƽ��
    Dionysus,   //����ϼҽ�
    Demeter     //�����׸�
}

public class Skill : MonoBehaviour
{
    public GameObject skillRangePrefab; // ���� ǥ�ÿ� ������
    private GameObject skillRangeInstance; // ���� ǥ�ÿ� �ν��Ͻ�

    private float skillRangeHeight = 0.1f; // ��ų ���� ����

    private bool isSkillReady = true; // ��ų ��� �������� ����
    private bool isShowSkillRange = false; //���� ǥ�ÿ��� 

    bool isSkillReady_1 = false;
    bool isSkillReady_2 = false;

    private float skillCooldown = 5.0f; //��Ÿ��
    private float currentCooldown = 0.0f; //���� ��Ÿ��

    public GameObject skillNum_1; //���콺 ��Ƽ��

    public GameObject skillNum_5; //������ ��Ƽ��

    public GameObject skillNum_8; //�������佺 ���к���ȭ �нú�

    public GameObject skillNum_11; //�츣�޽� �̼Ӱ�ȭ �Ҹ�

    public GameObject skills { get; set; }

    void Update()
    {
        // 1�� Ű�� ���� �� ��ų ������ ǥ��
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

        // ���콺 ��ġ�� ��ų ������ ����ٴϵ��� ������Ʈ
        if (skillRangeInstance != null)
        {
            UpdateSkillRangePosition();
        }
    }

    //��ų ���� ǥ��
    void ShowSkillRange()
    {
        isShowSkillRange = true;

        // ��ų ���� ǥ�ÿ� �������� �ν��Ͻ�ȭ
        skillRangeInstance = Instantiate(skillRangePrefab);
    }

    //��ų��� ��ġ
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

    //��ų ���
    void CancelSkill()
    {
        isShowSkillRange = false;
        Destroy(skillRangeInstance);
    }

    //���콺 ��ų
    void UseZeusSkill()
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
        isSkillReady_1 = false;
        isShowSkillRange = false;
        Destroy(skillRangeInstance);
    }

    //������ ��ų
    void UseApolloSkill()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            Vector3 spawnPosition = hit.point;
            spawnPosition.y += skillRangeHeight; // Y ��ǥ ����

            Instantiate(skillNum_5, spawnPosition, Quaternion.identity);
        }

        currentCooldown = skillCooldown; //��Ÿ�� ����
        isSkillReady = false;
        isSkillReady_2 = false;
        isShowSkillRange = false;
        Destroy(skillRangeInstance);
    }







    // �ٸ� �̺�Ʈ �Ǵ� ���ǿ��� ��ų�� ��� �����ϰ� �Ϸ��� isSkillReady�� true�� �����Ͻʽÿ�.
    public void SetSkillReady(bool skillReady)
    {
        isSkillReady = skillReady;
    }
}
