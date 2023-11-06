using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill_Set : MonoBehaviour
{
    public static Skill_Set instance;

    public bool Zeus_S;  //스킬의 true, false값을 판단한다
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

    public GameObject page_00; //스킬선책창 페이지를 확인한다
    public GameObject page_01;
    public GameObject page_02;
    public GameObject page_03;

    public GameObject Zeus_i; //게임바에서 상단에 게임스킬 표시를 보여준다
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

    public Text Skill_num1;
    public Text Skill_num2;
    public Text Skill_num3;
    public Text Skill_num4;

    bool _1_1 = false; // true, false 값을 변경해주며  업데이트문에 지속적으로 안뜨게 해준다
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

    public GameObject start; // 이 게임 오브젝트가없으면 모든게 true 값이며 계속 업데이트문에 뜨게될것이다

    public bool activeCool = false;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        if (page_00.activeSelf || page_01.activeSelf ||  //페이지가 활성화 상태일때만 bool값들을 받아온다
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

        //-------------------------------액티브--------------------------------------------//
        if ((Zeus_S == true) && (_1_1 == false) && (start.activeSelf == true)) //start가 활성화 되었을때
        {
            Zeus_i.SetActive(true);
            Skill_num1.text = "제우스";
            _1_1 = true;
        }
        else if ((Poseidon_S == true) && (_1_2 == false) && (start.activeSelf == true))
        {
            Poseidon_i.SetActive(true);
            Skill_num1.text = "포세이돈";
            _1_2 = true;
        }
        else if ((Hades_S == true) && (_1_3 == false) && (start.activeSelf == true))
        {
            Hades_i.SetActive(true);
            Skill_num1.text = "하데스";
            _1_3 = true;
        }
        //---------------------------------패시브-------------------------------------------//
        else if ((Hephaestus_S == true) && (_2_1 == false) && (start.activeSelf == true))
        {
            Hephaestus_i.SetActive(true);
            Skill_num4.text = "헤파이토스";
            _2_1 = true;
        }
        else if ((Artemis_S == true) && (_2_2 == false) && (start.activeSelf == true))
        {
            Artemis_i.SetActive(true);
            Skill_num4.text = "아르테미스";
            _2_2 = true;
        }
        else if ((Ares_S == true) && (_2_3 == false) && (start.activeSelf == true))
        {
            Ares_i.SetActive(true);
            Skill_num4.text = "아레스";
            _2_3 = true;
        }
        //---------------------------------버프---------------------------------------------//
        else if ((Hera_S == true) && (_3_1 == false) && (start.activeSelf == true))
        {
            Hera_i.SetActive(true);
            Skill_num2.text = "헤라";
            _3_1 = true;
        }
        else if ((Apollo_S == true) && (_3_2 == false) && (start.activeSelf == true))
        {
            Apollo_i.SetActive(true);
            Skill_num2.text = "아폴로";
            _3_2 = true;
        }
        else if ((Athena_S == true) && (_3_3 == false) && (start.activeSelf == true))
        {
            Athena_i.SetActive(true);
            Skill_num2.text = "아테나";
            _3_3 = true;
        }
        else if ((Aphrodite_S == true) && (_3_4 == false) && (start.activeSelf == true))
        {
            Aphrodite_i.SetActive(true);
            Skill_num2.text = "아프로디테";
            _3_4 = true;
        }
        //--------------------------------소모품--------------------------------------------//
        else if ((Hermes_S == true) && (_4_1 == false) && (start.activeSelf == true))
        {
            Hermes_i.SetActive(true);
            GameManager.instance.item_Skill.gameObject.SetActive(true);
            Skill_num3.text = "헤르메스";
            _4_1 = true;
        }
        else if ((Hestia_S == true) && (_4_2 == false) && (start.activeSelf == true))
        {
            Hestia_i.SetActive(true);
            GameManager.instance.item_Skill.gameObject.SetActive(true);
            Skill_num3.text = "헤스티아";
            _4_2 = true;
        }
        else if ((Dionysus_S == true) && (_4_3 == false) && (start.activeSelf == true))
        {
            Dionysus_i.SetActive(true);
            GameManager.instance.item_Skill.gameObject.SetActive(true);
            Skill_num3.text = "디오니소스";
            _4_3 = true;
        }
        else if ((Demeter_S == true) && (_4_4 == false) && (start.activeSelf == true))
        {
            Demeter_i.SetActive(true);
            GameManager.instance.item_Skill.gameObject.SetActive(true);
            Skill_num3.text = "데메테르";
            _4_4 = true;
        }
    }
}
