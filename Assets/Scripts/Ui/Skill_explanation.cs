using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill_explanation : MonoBehaviour
{
    public GameObject background_1;
    public GameObject background_2;
    public GameObject background_3;
    public GameObject background_4;
    public Text Name;
    public Text explanation;
    bool ON_1 = true;
    bool ON_2 = true;
    bool ON_3 = true;
    bool ON_4 = true;
    public string nametext;
    public string explanationtext;

    public void Click_1()
    {
        if (ON_1 || background_1.activeSelf == false) 
        {
            background_1.SetActive(true);
            background_2.SetActive(false);
            background_3.SetActive(false);
            background_4.SetActive(false);
            Name.text = nametext;
            explanation.text = explanationtext;
            ON_1 = false;
        }
        //else if (background_1.activeSelf == false)
        //{
        //    ON_1 = true;
        //    background_1.SetActive(true);
        //    background_2.SetActive(false);
        //    background_3.SetActive(false);
        //    background_4.SetActive(false);
        //}
        else if (!ON_1 && background_1.activeSelf == true)
        {
            background_1.SetActive(false);
            background_2.SetActive(false);
            background_3.SetActive(false);
            background_4.SetActive(false);
            ON_1 = true;
        }

    }

    public void Click_2()
    {
        if (ON_2 || background_2.activeSelf == false)
        {
            background_1.SetActive(false);
            background_2.SetActive(true);
            background_3.SetActive(false);
            background_4.SetActive(false);
            Name.text = nametext;
            explanation.text = explanationtext;
            ON_2 = false;
        }
        //else if (background_2.activeSelf == false)
        //{
        //    ON_2 = true;
        //    background_1.SetActive(false);
        //    background_2.SetActive(true);
        //    background_3.SetActive(false);
        //    background_4.SetActive(false);
        //}
        else if (!ON_2 && background_2.activeSelf == true)
        {
            background_1.SetActive(false);
            background_2.SetActive(false);
            background_3.SetActive(false);
            background_4.SetActive(false);
            ON_2 = true;
        }

    }

    public void Click_3()
    {
        if (ON_3 || background_3.activeSelf == false)
        {
            background_1.SetActive(false);
            background_2.SetActive(false);
            background_3.SetActive(true);
            background_4.SetActive(false);
            Name.text = nametext;
            explanation.text = explanationtext;
            ON_3 = false;
        }
        //else if (background_3.activeSelf == false)
        //{
        //    ON_3 = true;
        //    background_1.SetActive(false);
        //    background_2.SetActive(false);
        //    background_3.SetActive(true);
        //    background_4.SetActive(false);
        //}
        else if (!ON_3 && background_3.activeSelf == true)
        {
            background_1.SetActive(false);
            background_2.SetActive(false);
            background_3.SetActive(false);
            background_4.SetActive(false);
            ON_3 = true;
        }
    }

    public void Click_4()
    {
        if (ON_4 || background_4.activeSelf == false)
        {
            background_1.SetActive(false);
            background_2.SetActive(false);
            background_3.SetActive(false);
            background_4.SetActive(true);
            Name.text = nametext;
            explanation.text = explanationtext;
            ON_4 = false;
        }
        //else if (background_4.activeSelf == false)
        //{
        //    ON_4 = true;
        //    background_1.SetActive(false);
        //    background_2.SetActive(false);
        //    background_3.SetActive(false);
        //    background_4.SetActive(true);
        //}
        else if (!ON_4 && background_4.activeSelf == true)
        {
            background_1.SetActive(false);
            background_2.SetActive(false);
            background_3.SetActive(false);
            background_4.SetActive(false);
            ON_4 = true;
        }      
    }

}
