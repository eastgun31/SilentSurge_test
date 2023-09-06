using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float camSpeed = 30f;

    public float scrollSpeed = 50f;

    public float minY = 5f;
    public float maxY = 25f;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        //À§
        if(Input.GetKey(KeyCode.W)) 
        {
            pos.z += camSpeed * Time.deltaTime;
        }
        //¾Æ·¡
        if (Input.GetKey(KeyCode.S))
        {
            pos.z -= camSpeed * Time.deltaTime;
        }
        //¿Þ
        if (Input.GetKey(KeyCode.A))
        {
            pos.x -= camSpeed * Time.deltaTime;
        }
        //¿À
        if (Input.GetKey(KeyCode.D))
        {
            pos.x += camSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed * 100f * Time.deltaTime;
    
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;
    }
}
