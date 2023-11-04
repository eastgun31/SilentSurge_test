using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawn : MonoBehaviour
{
    public GameObject arrow1;
    public GameObject arrow2;

    public GameObject[] _arrow1;
    public GameObject[] _arrow2;
    public int arPoolsize;

    float time;

    // Start is called before the first frame update
    void Start()
    {
        _arrow1 = new GameObject[arPoolsize];
        _arrow2 = new GameObject[arPoolsize];

        for (int i = 0; i < arPoolsize; i++)
        {
            _arrow1[i] = Instantiate(arrow1, transform);
            _arrow1[i].SetActive(false);

            _arrow2[i] = Instantiate(arrow2, transform);
            _arrow2[i].SetActive(false);
        }

        time = 0;
    }

}
