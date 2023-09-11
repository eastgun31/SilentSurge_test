using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    public int value;
    public int pointcheck;
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
                        GameManager_KDG.Instance.check[0] = pointcheck;
                        break;
                    case 1:
                        GameManager_KDG.Instance.check[1] = pointcheck;
                        break;
                    case 2:
                        GameManager_KDG.Instance.check[2] = pointcheck;
                        break;
                }
            }
        }

        if (other.tag == "Player")
        {
            time += Time.deltaTime;

            //GameManager_KDG.Instance.attacking = true;

            if (pointcheck == 1)
            {
                GameManager_KDG.Instance.attacking = true;

                switch (value)
                {
                    case 0:
                        GameManager_KDG.Instance.attackPoint = 0;
                        break;
                    case 1:
                        GameManager_KDG.Instance.attackPoint = 1;
                        break;
                    case 2:
                        GameManager_KDG.Instance.attackPoint = 2;
                        break;
                }
            }

            if (time > 3f)
            {
                time = 0;
                pointcheck = 2;
                
                switch (value)
                {
                    case 0:
                        GameManager_KDG.Instance.check[0] = pointcheck;
                        break;
                    case 1:
                        GameManager_KDG.Instance.check[1] = pointcheck;
                        break;
                    case 2:
                        GameManager_KDG.Instance.check[2] = pointcheck;
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
            GameManager_KDG.Instance.attacking = false;
        }
    }
}
