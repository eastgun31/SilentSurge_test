using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page_Button : MonoBehaviour //�ǵ��� ����
{   [SerializeField]
    GameObject Page_On;
    [SerializeField]
    GameObject Page_Off;

    public GameObject opacity_image;
    public GameObject bar_image;

    void Start()
    {
        //Time �Ŵ����� �ϳ� �����ΰ� NEXT��ư�� Ȱ��ȭ ���¿����� 0 ��Ȱ��ȭ ���¿����� 1�� ��� �ð��� �帣�� �����
    }
    void Update()
    {
        
    }

    public void Page_button()
    {
        Page_On.SetActive(true);
        Page_Off.SetActive(false);
    }

    public void Page00_NEXT_Page01_button()
    {
        if (FindAnyObjectByType<Skill_Select>().Zeus_ON.activeSelf || FindAnyObjectByType<Skill_Select>().Poseidon_ON.activeSelf ||
            FindAnyObjectByType<Skill_Select>().Hades_ON.activeSelf == true)
        {
            Page_On.SetActive(true);
            Page_Off.SetActive(false);
        }
        else
        {
            Debug.Log("��Ƽ�� ��ų�� Ȱ��ȭ �Ǿ������ʽ��ϴ�");
        }
    }
    public void Page01_NEXT_Page02_button()
    {
        if (FindAnyObjectByType<Skill_Select>().Hephaestus_ON.activeSelf || FindAnyObjectByType<Skill_Select>().Artemis_ON.activeSelf ||
            FindAnyObjectByType<Skill_Select>().Ares_ON.activeSelf == true)
        {
            Page_On.SetActive(true);
            Page_Off.SetActive(false);
        }
        else
        {
            Debug.Log("�нú� ��ų�� Ȱ��ȭ �Ǿ������ʽ��ϴ�");
        }
    }

    public void Page02_NEXT_Page03_button()
    {
        if (FindAnyObjectByType<Skill_Select>().Hera_ON.activeSelf || FindAnyObjectByType<Skill_Select>().Apollo_ON.activeSelf ||
            FindAnyObjectByType<Skill_Select>().Athena_ON.activeSelf || FindAnyObjectByType<Skill_Select>().Aphrodite_ON.activeSelf == true)
        {
            Page_On.SetActive(true);
            Page_Off.SetActive(false);
        }
        else
        {
            Debug.Log("�������� �� ����� ��ų�� Ȱ��ȭ �Ǿ������ʽ��ϴ�");
        }
    }

    public void NEXT_button()
    {
        if (FindAnyObjectByType<Skill_Select>().Hermes_ON.activeSelf || FindAnyObjectByType<Skill_Select>().Hestia_ON.activeSelf ||
            FindAnyObjectByType<Skill_Select>().Dionysus_ON.activeSelf || FindAnyObjectByType<Skill_Select>().Demeter_ON.activeSelf == true)
        {
            opacity_image.SetActive(false);
            bar_image.SetActive(false);
            Page_On.SetActive(true);
            Page_Off.SetActive(false);

        }
        else
        {
            Debug.Log("�Ҹ�ǰ ��ų�� Ȱ��ȭ �Ǿ������ʽ��ϴ�");
        }
        
    }
}
