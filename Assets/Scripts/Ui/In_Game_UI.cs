using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class In_Game_UI : MonoBehaviour
{
    //public GameObject Tab;
    public Animator Tab;
    public GameObject On;
    public GameObject ESC_image;
    public GameObject Option_window;
    public RectTransform _Tab;

    public bool ESC_bool = true;
    public bool ESC_ON = true;
    public bool _OK_ = true;
    public bool Game_Start = true;
    public bool TTab = true;

    Audio_Manager Audio_Manager;

    void Start()
    {
        Audio_Manager = FindAnyObjectByType<Audio_Manager>();
        _Tab.anchoredPosition = new Vector2(-915, -10);
    }

    
    void Update()
    {
        if ((On.activeSelf == true) && (Game_Start == true))
        {
            Time.timeScale = 1;
            Game_Start = false;
        }
        else if ((On.activeSelf == false) && (Game_Start == true))
        {
            Time.timeScale = 0;
        }
        else
        {

        }

        if (Input.GetKeyDown(KeyCode.Tab) && ( On.activeSelf == true) && (TTab == true) && (_OK_ == true))
        {
            Audio_Manager.PlaySFX(Audio_Manager.AudioButton);
            _Tab.anchoredPosition = new Vector2(-613, -10);
            //Tab.SetTrigger("On");
            ESC_ON = false;
            TTab = false;
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && (On.activeSelf == true) && (TTab == false) && (_OK_ == true))
        {
            Audio_Manager.PlaySFX(Audio_Manager.AudioButton);
            _Tab.anchoredPosition = new Vector2(-915, -10);
            ESC_ON = true;
            TTab = true;
        }
        //else if (Input.GetKeyUp(KeyCode.Tab) && (On.activeSelf == true) && (_OK_ == true))
        //{
        //    Tab.SetTrigger("Off");
        //    ESC_ON = true;
        //}
        else
        {

        }

        if (Input.GetKeyDown(KeyCode.Escape) && (On.activeSelf == true) && (ESC_bool == true)&& (ESC_ON == true))
        {
            Audio_Manager.PlaySFX(Audio_Manager.AudioButton);
            ESC_image.SetActive(true);
            ESC_bool = false;
            _OK_ = false;
            Time.timeScale = 0; //ESC를 누르면 시간이 멈춘다
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && (On.activeSelf == true) && (ESC_bool == false)) //&& (Option_window.activeSelf == false))
        {
            Tab.SetTrigger("Off");
            Audio_Manager.PlaySFX(Audio_Manager.AudioButton);
            ESC_image.SetActive(false);
            ESC_bool = true;
            _OK_ = true;
            Option_window.SetActive(false);
            Time.timeScale = 1; //ESC를 다시 누르면 시간이 흐른다.
        }
        else
        {

        }

    }
    public void ESC_OFF()
    {
        if ((On.activeSelf == true) && (ESC_bool == false)) //&& (Option_window.activeSelf == false))
        {
            Tab.SetTrigger("Off");
            Audio_Manager.PlaySFX(Audio_Manager.AudioButton);
            ESC_image.SetActive(false);
            ESC_bool = true;
            _OK_ = true;
            Option_window.SetActive(false);
            Time.timeScale = 1; //ESC를 다시 누르면 시간이 흐른다.
        }
    }

    public void ESC_On()
    {
        if ((On.activeSelf == true) && (ESC_bool == true)&& (ESC_ON == true))
        {
            Audio_Manager.PlaySFX(Audio_Manager.AudioButton);
            ESC_image.SetActive(true);
            ESC_bool = false;
            _OK_ = false;
            Time.timeScale = 0; //ESC를 누르면 시간이 멈춘다
        }
        else if ((On.activeSelf == true) && (ESC_bool == false)) //&& (Option_window.activeSelf == false))
        {
            Tab.SetTrigger("Off");
            Audio_Manager.PlaySFX(Audio_Manager.AudioButton);
            ESC_image.SetActive(false);
            ESC_bool = true;
            _OK_ = true;
            Option_window.SetActive(false);
            Time.timeScale = 1;
            //ESC를 다시 누르면 시간이 흐른다.
        }
        else
        {

        }
    }

    public void RePlay()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Quit()
    {
        Audio_Manager.PlaySFX(Audio_Manager.AudioButton);
        SceneManager.LoadScene("Loby_Scene");
        Time.timeScale = 1;
    }
}
