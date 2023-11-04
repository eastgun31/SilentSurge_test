using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int value;
    public Vector3 target;

    private void OnEnable()
    {
        Invoke("Off_Arrow", 2f);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, 10f * Time.deltaTime);
    }

    void Off_Arrow()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(value == 1 && other.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
        }
        if(value == 2 && other.gameObject.tag == "Enemy")
        {
            gameObject.SetActive(false);
        }
    }

}
