using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherStat : MonoBehaviour
{
    bool isDie = false;

    public float ArcherHp = 100;
    public float power = 10;

    //부활 전 대기시간
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
            isDie = true; // 죽음 표시
            resurrectionTimer += Time.deltaTime;

            if (resurrectionTimer >= resurrectionTime)
            {
                // 최대 대기 시간 초과 시 유닛 오브젝트 삭제
                Destroy(gameObject);
            }
        }
        else
        {
            resurrectionTimer = 0f; // 생존할 경우 타이머 초기화
        }
    }

    void Die()
    {
        this.gameObject.layer = 7;  //DieUnit변경
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
