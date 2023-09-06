using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    public int value;
    public int pointcheck;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            pointcheck = 1;

            switch(value)
            {
                case 0:
                    GameManager_KDG.Instance.aCheck = pointcheck;
                    break;
                case 1:
                    GameManager_KDG.Instance.bCheck = pointcheck;
                    break;
                case 2:
                    GameManager_KDG.Instance.cCheck = pointcheck;
                    break;
                       
            }
        }
    }
}
