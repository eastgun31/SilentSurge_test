using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Main : MonoBehaviour
{

    void Start()
    {
        FindAnyObjectByType<Audio_Manager>().Main_Music();
    }
}
