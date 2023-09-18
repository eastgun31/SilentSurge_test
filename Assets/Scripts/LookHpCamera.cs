using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookHpCamera : MonoBehaviour
{
    public Camera hpCamera; //hp�� ȭ�� ���� �ϴ� �ڵ�
    void Start()
    {
        hpCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }



    // Update is called once per frame

    void Update()
    {
        transform.LookAt(transform.position + hpCamera.transform.rotation * Vector3.back,

         hpCamera.transform.rotation * Vector3.down);
    }
}
