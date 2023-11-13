using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Kijung_GameManager : MonoBehaviour
{
    public string Scene_Name;
    public GameObject HOW;
    public GameObject OP;
    public GameObject source_ON;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HOW.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                HOW.SetActive(false);
            }
        }
        if (OP.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OP.SetActive(false);
            }
        }
        if (source_ON.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                source_ON.SetActive(false);
            }
        }
    }

    public void Game_Start()
    {
        SceneManager.LoadScene(Scene_Name);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
}
