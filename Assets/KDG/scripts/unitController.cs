using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitController : MonoBehaviour
{

    public GameObject follower;
    float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = 
        Vector3.MoveTowards(transform.position, new Vector3(follower.transform.position.x, gameObject.transform.position.y, follower.transform.position.z), Time.deltaTime * speed);
    }
}
