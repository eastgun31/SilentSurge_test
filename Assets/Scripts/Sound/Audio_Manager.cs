using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Audio_Manager : MonoBehaviour
{
    //private static Audio_Manager instance;

    //public void Awake()
    //{
    //    if (null == instance)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(this.gameObject);
    //    }
    //    else
    //    {
    //        Destroy(this.gameObject);
    //    }
    //}

    [Header("---------- 오디오 소스 ----------")]
    [SerializeField]  AudioSource MusicSource;
    [SerializeField]  AudioSource SFXSource;


    [Header("---------- 오디오 클립 ----------")]
    public AudioClip Loby_background;
    public AudioClip Main_background;
    public AudioClip Story_background;
    public AudioClip AudioButton;

    public AudioClip Zeus;
    public AudioClip Poseidon;
    public AudioClip Hades;

    public AudioClip Hera;
    public AudioClip Apollo;
    public AudioClip Athena;
    public AudioClip Aphrodite;

    public AudioClip Hermes;
    public AudioClip Hestia;
    public AudioClip Dionysus;
    public AudioClip Demeter;

    void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "Loby_Scene")
        {
            MusicSource.clip = Loby_background;
        }
        else if (currentSceneName == "Story_Scene")
        {
            MusicSource.clip = Story_background;
        }
        else if (currentSceneName == "MainScene")
        {
            MusicSource.clip = Main_background;
        }

        MusicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

}


