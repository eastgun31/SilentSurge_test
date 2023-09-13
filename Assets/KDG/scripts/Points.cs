using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    public int value;   //점령지 넘버링 ABCDE 차례대로 01234
    public int pointcheck; //중립상태 0 ,적점령시 1, 플레이어가 점령시 2
    public float time;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        //if(other.tag == "Enemy" && other.tag == "Player")
        //{
        //    pointcheck = 0;
        //}

        if(other.tag == "Enemy")
        {
            time += Time.deltaTime;

            if(time > 3f)
            {
                time = 0;
                pointcheck = 1;

                switch(value)
                {
                    case 0:
                        GameManager.instance.check[0] = pointcheck;
                        break;
                    case 1:
                        GameManager.instance.check[1] = pointcheck;
                        break;
                    case 2:
                        GameManager.instance.check[2] = pointcheck;
                        break;
                    case 3:
                        GameManager.instance.check[3] = pointcheck;
                        break;
                    case 4:
                        GameManager.instance.check[4] = pointcheck;
                        break;
                }
            }
        }

        if (other.tag == "Player")
        {
            time += Time.deltaTime;

            //GameManager.instance.attacking = true;

            if (pointcheck == 1) //적이 점령시
            {
                GameManager.instance.attacking = true;

                switch (value)
                {
                    case 0:
                        GameManager.instance.attackPoint = 0;
                        break;
                    case 1:
                        GameManager.instance.attackPoint = 1;
                        break;
                    case 2:
                        GameManager.instance.attackPoint = 2;
                        break;
                    case 3:
                        GameManager.instance.attackPoint = 3;
                        break;
                    case 4:
                        GameManager.instance.attackPoint = 4;
                        break;
                }
            }

            if (time > 3f) //플레이어가 점령시
            {
                time = 0;
                pointcheck = 2;
                
                switch (value)
                {
                    case 0:
                        GameManager.instance.check[0] = pointcheck;
                        break;
                    case 1:
                        GameManager.instance.check[1] = pointcheck;
                        break;
                    case 2:
                        GameManager.instance.check[2] = pointcheck;
                        break;
                    case 3:
                        GameManager.instance.check[3] = pointcheck;
                        break;
                    case 4:
                        GameManager.instance.check[4] = pointcheck;
                        break;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Enemy")
        {
            time = 0;
        }

        if(other.tag == "Player")
        {
            time = 0;
            GameManager.instance.attacking = false;
        }
    }
}
