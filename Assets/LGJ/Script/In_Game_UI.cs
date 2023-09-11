using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class In_Game_UI : MonoBehaviour
{
    //public GameObject Tab;
    public Animator Tab;
    public GameObject On;
    public GameObject ESC_image;

    public bool ESC_bool = true;
    public bool ESC_ON = true;
    public bool _OK_ = true;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && ( On.activeSelf == true) && (_OK_ == true))
        {
            Tab.SetTrigger("On");
            ESC_ON = false;

        }
        else if (Input.GetKeyUp(KeyCode.Tab) && (On.activeSelf == true) && (_OK_ == true))
        {
            Tab.SetTrigger("Off");
            ESC_ON = true;
        }
        else
        {

        }

        if (Input.GetKeyDown(KeyCode.Escape) && (On.activeSelf == true) && (ESC_bool == true) && (ESC_ON == true))
        {
            ESC_image.SetActive(true);
            ESC_bool = false;
            _OK_ = false;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && (On.activeSelf == true) && (ESC_bool == false))
        {
            ESC_image.SetActive(false);
            ESC_bool = true;
            _OK_ = true;
        }

    }
}
