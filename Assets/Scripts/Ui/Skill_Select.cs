using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Select : MonoBehaviour
{
    //---------------------------------��Ƽ��------------------------------------//
    #region
    public GameObject ZEUS_ON;
    public GameObject Poseidon_ON;
    public GameObject Hades_ON;

    public bool ZEUS_Select;
    public bool Poseidon_Select;
    public bool Hades_Select;
    #endregion
    //----------------------------------�нú�-----------------------------------//
    #region
    public GameObject Hephaestus_ON;
    public GameObject ARTMS_ON;
    public GameObject Ares_ON;

    public bool Hephaestus_Select;
    public bool ARTMS_Select;
    public bool Ares_Select;
    #endregion
    //---------------------------------�������� �� �����------------------------//
    #region
    public GameObject Hera_ON;
    public GameObject Apollo_ON;
    public GameObject Athena_ON;
    public GameObject Aphrodite_ON;

    public bool Hera_Select;
    public bool Apollo_Select;
    public bool Athena_Select;
    public bool Aphrodite_Select;
    #endregion
    //---------------------------------------�Ҹ�ǰ------------------------------//
    #region
    public GameObject Hermes_ON;
    public GameObject Hestia_ON;
    public GameObject Dionysus_ON;
    public GameObject Demeter_ON;

    public bool Hermes_Select;
    public bool Hestia_Select;
    public bool Dionysus_Select;
    public bool Demetere_Select;
    #endregion
    //---------------------------------------------------------------------------//
    public void Update()
    {
        //-------------------------------��Ƽ��----------------------------------//
        if (ZEUS_ON.activeSelf == true)
        {
            ZEUS_Select = true;
            Poseidon_Select = false;
            Hades_Select = false;
        }
        else if (Poseidon_ON.activeSelf == true)
        {
            ZEUS_Select = false;
            Poseidon_Select = true;
            Hades_Select = false;
        }
        else if (Hades_ON.activeSelf == true)
        {
            ZEUS_Select = false;
            Poseidon_Select = false;
            Hades_Select = true;
        }
        //----------------------------------�нú�-------------------------------//
        if (Hephaestus_ON.activeSelf == true)
        {
            Hephaestus_Select = true;
            ARTMS_Select = false;
            Ares_Select = false;
        }
        else if (ARTMS_ON.activeSelf == true)
        {
            Hephaestus_Select = false;
            ARTMS_Select = true;
            Ares_Select = false;
        }
        else if (Ares_ON.activeSelf == true)
        {
            Hephaestus_Select = false;
            ARTMS_Select = false;
            Ares_Select = true;
        }
        //---------------------------------�������� �� �����--------------------//
        if (Hera_ON.activeSelf == true)
        {
            Hera_Select =true;
            Apollo_Select = false;
            Athena_Select = false;
            Aphrodite_Select = false;
        }
        else if (Apollo_ON.activeSelf == true)
        {
            Hera_Select = false;
            Apollo_Select = true;
            Athena_Select = false;
            Aphrodite_Select = false;
        }
        else if (Athena_ON.activeSelf == true)
        {
            Hera_Select = false;
            Apollo_Select = false;
            Athena_Select = true;
            Aphrodite_Select = false;
        }
        else if (Aphrodite_ON.activeSelf == true)
        {
            Hera_Select = false;
            Apollo_Select = false;
            Athena_Select = false;
            Aphrodite_Select = true;
        }
        //---------------------------------------�Ҹ�ǰ--------------------------//
        if (Hermes_ON.activeSelf == true)
        {
            Hermes_Select = true;
            Hestia_Select = false;
            Dionysus_Select = false;
            Demetere_Select = false;
        }
        else if (Hestia_ON.activeSelf == true)
        {
            Hermes_Select = false;
            Hestia_Select = true;
            Dionysus_Select = false;
            Demetere_Select = false;
        }
        else if (Dionysus_ON.activeSelf == true)
        {
            Hermes_Select = false;
            Hestia_Select = false;
            Dionysus_Select = true;
            Demetere_Select = false;
        }
        else if (Demeter_ON.activeSelf == true)
        {
            Hermes_Select = false;
            Hestia_Select = false;
            Dionysus_Select = false;
            Demetere_Select = true;
        }
        //-------------------------------------------------------------------------------//
    }
    //-----------------------------------��Ƽ�� ��ư----------------------------------//
    public void ZEUS_ON_Select_Button()
    {
        ZEUS_ON.SetActive(true);
        Poseidon_ON.SetActive(false);
        Hades_ON.SetActive(false);
    }
    public void Poseidon_ON_Select_Button()
    {
        ZEUS_ON.SetActive(false);
        Poseidon_ON.SetActive(true);
        Hades_ON.SetActive(false);
    }
    public void Hades_ON_Select_Button()
    {
        ZEUS_ON.SetActive(false);
        Poseidon_ON.SetActive(false);
        Hades_ON.SetActive(true);
    }
    //-----------------------------------�нú� ��ư---------------------------------//
    public void Hephaestus_ON_Select_Button()
    {
        Hephaestus_ON.SetActive(true);
        ARTMS_ON.SetActive(false);
        Ares_ON.SetActive(false);
    }
    public void ARTMS_ON_Select_Button()
    {
        Hephaestus_ON.SetActive(false);
        ARTMS_ON.SetActive(true);
        Ares_ON.SetActive(false);
    }
    public void Ares_ON_Select_Button()
    {
        Hephaestus_ON.SetActive(false);
        ARTMS_ON.SetActive(false);
        Ares_ON.SetActive(true);
    }
    //---------------------------------�������� �� ����� ��ư---------------------------------//
    public void Hera_ON_Select_Button()
    {
        Hera_ON.SetActive(true);
        Apollo_ON.SetActive(false);
        Athena_ON.SetActive(false);
        Aphrodite_ON.SetActive(false);
    }
    public void Apollo_ON_Select_Button()
    {
        Hera_ON.SetActive(false);
        Apollo_ON.SetActive(true);
        Athena_ON.SetActive(false);
        Aphrodite_ON.SetActive(false);
    }
    public void Athena_ON_Select_Button()
    {
        Hera_ON.SetActive(false);
        Apollo_ON.SetActive(false);
        Athena_ON.SetActive(true);
        Aphrodite_ON.SetActive(false);
    }
    public void Aphrodite_ON_Select_Button()
    {
        Hera_ON.SetActive(false);
        Apollo_ON.SetActive(false);
        Athena_ON.SetActive(false);
        Aphrodite_ON.SetActive(true);
    }
    //---------------------------------------�Ҹ�ǰ ��ư--------------------------------------//
    public void Hermes_ON_Select_Button()
    {
        Hermes_ON.SetActive(true);
        Hestia_ON.SetActive(false);
        Dionysus_ON.SetActive(false);
        Demeter_ON.SetActive(false);
    }
    public void Hestia_ON_Select_Button()
    {
        Hermes_ON.SetActive(false);
        Hestia_ON.SetActive(true);
        Dionysus_ON.SetActive(false);
        Demeter_ON.SetActive(false);
    }
    public void Dionysus_ON_Select_Button()
    {
        Hermes_ON.SetActive(false);
        Hestia_ON.SetActive(false);
        Dionysus_ON.SetActive(true);
        Demeter_ON.SetActive(false);
    }
    public void Demeter_ON_Select_Button()
    {
        Hermes_ON.SetActive(false);
        Hestia_ON.SetActive(false);
        Dionysus_ON.SetActive(false);
        Demeter_ON.SetActive(true);
    }
    //----------------------------------------------------------------------------------//
}
