using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookHpCamera : MonoBehaviour
{
    public Camera hpCamera; //hp바 화면 보게 하는 코드
    void Start()
    {
        hpCamera = Camera.main;
    }

    void Update()
    {
        transform.LookAt(transform.position + hpCamera.transform.rotation * Vector3.back,

         hpCamera.transform.rotation * Vector3.down);
    }
}
