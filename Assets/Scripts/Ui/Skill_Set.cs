using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Set : MonoBehaviour
{
    public bool Zeus_S;  //��ų�� true, false���� �Ǵ��Ѵ�
    public bool Poseidon_S;
    public bool Hades_S;

    public bool Hephaestus_S;
    public bool Artemis_S;
    public bool Ares_S;

    public bool Hera_S;
    public bool Apollo_S;
    public bool Athena_S;
    public bool Aphrodite_S;

    public bool Hermes_S;
    public bool Hestia_S;
    public bool Dionysus_S;
    public bool Demeter_S;

    public GameObject page_00; //��ų��åâ �������� Ȯ���Ѵ�
    public GameObject page_01;
    public GameObject page_02;
    public GameObject page_03;

    public GameObject Zeus_i; //���ӹٿ��� ��ܿ� ���ӽ�ų ǥ�ø� �����ش�
    public GameObject Poseidon_i;
    public GameObject Hades_i;
           
    public GameObject Hephaestus_i;
    public GameObject Artemis_i;
    public GameObject Ares_i;
           
    public GameObject Hera_i;
    public GameObject Apollo_i;
    public GameObject Athena_i;
    public GameObject Aphrodite_i;
           
    public GameObject Hermes_i;
    public GameObject Hestia_i;
    public GameObject Dionysus_i;
    public GameObject Demeter_i;

    bool _1_1 = false; // true, false ���� �������ָ�  ������Ʈ���� ���������� �ȶ߰� ���ش�
    bool _1_2 = false;
    bool _1_3 = false;
             
    bool _2_1 = false;
    bool _2_2 = false;
    bool _2_3 = false;
              
    bool _3_1 = false;
    bool _3_2 = false;
    bool _3_3 = false;
    bool _3_4 = false;
              
    bool _4_1 = false;
    bool _4_2 = false;
    bool _4_3 = false;
    bool _4_4 = false;


    public GameObject start; // �� ���� ������Ʈ�������� ���� true ���̸� ��� ������Ʈ���� �߰Եɰ��̴�
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (page_00.activeSelf || page_01.activeSelf ||  //�������� Ȱ��ȭ �����϶��� bool������ �޾ƿ´�
            page_02.activeSelf || page_03.activeSelf == true)
        {
            Zeus_S = FindAnyObjectByType<Skill_Select>().Zeus_Select;
            Poseidon_S = FindAnyObjectByType<Skill_Select>().Poseidon_Select;
            Hades_S = FindAnyObjectByType<Skill_Select>().Hades_Select;

            Hephaestus_S = FindAnyObjectByType<Skill_Select>().Hephaestus_Select;
            Artemis_S = FindAnyObjectByType<Skill_Select>().Artemis_Select;
            Ares_S = FindAnyObjectByType<Skill_Select>().Ares_Select;

            Hera_S = FindAnyObjectByType<Skill_Select>().Hera_Select;
            Apollo_S = FindAnyObjectByType<Skill_Select>().Apollo_Select;
            Athena_S = FindAnyObjectByType<Skill_Select>().Athena_Select;
            Aphrodite_S = FindAnyObjectByType<Skill_Select>().Aphrodite_Select;

            Hermes_S = FindAnyObjectByType<Skill_Select>().Hermes_Select;
            Hestia_S = FindAnyObjectByType<Skill_Select>().Hestia_Select;
            Dionysus_S = FindAnyObjectByType<Skill_Select>().Dionysus_Select;
            Demeter_S = FindAnyObjectByType<Skill_Select>().Demeter_Select;
        }
        else
        {

        }
        //-------------------------------��Ƽ��--------------------------------------------//
        if ((Zeus_S == true) && (_1_1 == false) && (start.activeSelf == true)) //start�� Ȱ��ȭ �Ǿ�����
        {
            Zeus_i.SetActive(true);
            _1_1 = true;
        }
        else if ((Poseidon_S == true) && (_1_2 == false) && (start.activeSelf == true))
        {
            Poseidon_i.SetActive(true);
            _1_2 = true;
        }
        else if ((Hades_S == true) && (_1_3 == false) && (start.activeSelf == true))
        {
            Hades_i.SetActive(true);
            _1_3 = true;
        }
        //---------------------------------�нú�-------------------------------------------//
        else if ((Hephaestus_S == true) && (_2_1 == false) && (start.activeSelf == true))
        {
            Hephaestus_i.SetActive(true);
            _2_1 = true;
        }
        else if ((Artemis_S == true) && (_2_2 == false) && (start.activeSelf == true))
        {
            Artemis_i.SetActive(true);
            _2_2 = true;
        }
        else if ((Ares_S == true) && (_2_3 == false) && (start.activeSelf == true))
        {
            Ares_i.SetActive(true);
            _2_3 = true;
        }
        //---------------------------------����---------------------------------------------//
        else if ((Hera_S == true) && (_3_1 == false) && (start.activeSelf == true))
        {
            Hera_i.SetActive(true);
            _3_1 = true;
        }
        else if ((Apollo_S == true) && (_3_2 == false) && (start.activeSelf == true))
        {
            Apollo_i.SetActive(true);
            _3_2 = true;
        }
        else if ((Athena_S == true) && (_3_3 == false) && (start.activeSelf == true))
        {
            Athena_i.SetActive(true);
            _3_3 = true;
        }
        else if ((Aphrodite_S == true) && (_3_4 == false) && (start.activeSelf == true))
        {
            Aphrodite_i.SetActive(true);
            _3_4 = true;
        }
        //--------------------------------�Ҹ�ǰ--------------------------------------------//
        else if ((Hermes_S == true) && (_4_1 == false) && (start.activeSelf == true))
        {
            Hermes_i.SetActive(true);
            _4_1 = true;
        }
        else if ((Hestia_S == true) && (_4_2 == false) && (start.activeSelf == true))
        {
            Hestia_i.SetActive(true);
            _4_2 = true;
        }
        else if ((Dionysus_S == true) && (_4_3 == false) && (start.activeSelf == true))
        {
            Dionysus_i.SetActive(true);
            _4_3 = true;
        }
        else if ((Demeter_S == true) && (_4_4 == false) && (start.activeSelf == true))
        {
            Demeter_i.SetActive(true);
            _4_4 = true;
        }
        else
        {

        }
        //----------------------------------------------------------------------------------//
        //-------------------------------��Ƽ��--------------------------------------------//
        if (Zeus_S == true) //start�� Ȱ��ȭ �Ǿ�����
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
              
            }
        }
        else if (Poseidon_S == true)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
              
            }
        }
        else if (Hades_S == true)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
              
            }
        }
        //---------------------------------�нú�-------------------------------------------//
        //else if (Hephaestus_S == true)
        //{
         
        //}
        //else if (Artemis_S == true)
        //{
         
        //}
        //else if (Ares_S == true)
        //{
         
        //}
        //---------------------------------����---------------------------------------------//
        else if (Hera_S == true)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {

            }
        }
        else if (Apollo_S == true)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {

            }
        }
        else if (Athena_S == true)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {

            }
        }
        else if (Aphrodite_S == true)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {

            }
        }
        //--------------------------------�Ҹ�ǰ--------------------------------------------//
        else if (Hermes_S == true)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {

            }
        }
        else if (Hestia_S == true)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {

            }
        }
        else if (Dionysus_S == true)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {

            }
        }
        else if (Demeter_S == true)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {

            }
        }
        else
        {

        }
        //----------------------------------------------------------------------------------//
    }
}
