using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseEvent : MonoBehaviour
{
    public GameObject image_Change;
    //����ȭ�鿡�� �̿�Ǵ� �¸��콺 �Լ����� ���� �ǵ��� ����

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
