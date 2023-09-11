using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherStat : MonoBehaviour
{
    bool isDie = false;

    public float ArcherHp = 100;
    public float power = 10;

    //��Ȱ �� ���ð�
    float resurrectionTime = 5f;
    float resurrectionTimer = 0f;

    void Start()
    {
        isDie = false;
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
