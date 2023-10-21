using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_ : MonoBehaviour
{
    Audio_Manager audio_Manager;
    void Start()
    {
        audio_Manager = FindAnyObjectByType<Audio_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            audio_Manager.PlaySFX(audio_Manager.Zeus);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            audio_Manager.PlaySFX(audio_Manager.Poseidon);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            audio_Manager.PlaySFX(audio_Manager.Hades);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            audio_Manager.PlaySFX(audio_Manager.Hera);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            audio_Manager.PlaySFX(audio_Manager.Apollo);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            audio_Manager.PlaySFX(audio_Manager.Athena);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            audio_Manager.PlaySFX(audio_Manager.Aphrodite);
        }

    }
}
