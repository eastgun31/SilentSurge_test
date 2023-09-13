using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ArcherStat : MonoBehaviour
{
    bool isDie = false;

    public float ArcherHp = 100;
    public float power = 10;

    float originalDamage; // �ɷ�ġ ���� ������ ������
    float buffDuration = 5f; // ��ų ���� �ð�
    bool isBuffActive = false; // ��ų Ȱ��ȭ ����
    float buffEndTime; // ��ų ���� �ð�

    //��Ȱ �� ���ð�
    float resurrectionTime = 5f;
    float resurrectionTimer = 0f;

    void Start()
    {
        isDie = false;
        originalDamage = power;
    }

    // Update is called once per frame
    void Update()
    {
        if (ArcherHp <= 0)
        {
            isDie = true; // ���� ǥ��
            resurrectionTimer += Time.deltaTime;

            if (resurrectionTimer >= resurrectionTime)
            {
                // �ִ� ��� �ð� �ʰ� �� ���� ������Ʈ ����
                Destroy(gameObject);
            }
        }
        else
        {
            resurrectionTimer = 0f; // ������ ��� Ÿ�̸� �ʱ�ȭ
        }

        if (isBuffActive && Time.time >= buffEndTime)
        {
            EndBuffSkill();
        }

    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "PowerBuff")
    //    {
    //        UseBuffSkill();
    //        Debug.Log("����");
    //    }
    //}

    //void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.transform.tag == "PowerBuff")
    //    {
    //        UseBuffSkill();
    //        Debug.Log("����");
    //    }
    //}

    void UseBuffSkill()
    {
        if (!isBuffActive)
        {
            // ��ų�� Ȱ��ȭ�ϰ� �ɷ�ġ�� ����
            isBuffActive = true;
            buffEndTime = Time.time + buffDuration;

            

            float buffDamage = originalDamage * 2;

            UpdateUnitDamage(buffDamage);

            Debug.Log("���� ���");
        }
    }

    void EndBuffSkill()
    {
        if (isBuffActive)
        {
            // ��ų�� ��Ȱ��ȭ�ϰ� ���� �ɷ�ġ�� �ǵ���
            isBuffActive = false;

            // ���� ������ �������� ������� ���������ϴ�.
            UpdateUnitDamage(originalDamage);

            Debug.Log("���� ����");
        }
    }

    void UpdateUnitDamage(float newDamage)
    {
        power = newDamage;
        Debug.Log("���� ����");
    }

    void Die()
    {
        this.gameObject.layer = 7;  //DieUnit����
    }

    void Resurrection()
    {
        this.gameObject.layer = 6;
        isDie = false;
        resurrectionTimer = 0f;

        ArcherHp = 50;
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
