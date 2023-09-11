using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseEvent : MonoBehaviour
{
    public GameObject image_Change;
    //메인화면에서 이용되는 온마우스 함수들임 절대 건들지 말것

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        if (true)
        {
            image_Change.SetActive(true);
        }
        else
        {
        }
    }

    private void OnMouseExit()
    {
        if (true)
        {
            image_Change.SetActive(false);
        }
        else
        {
        }
    }
}
