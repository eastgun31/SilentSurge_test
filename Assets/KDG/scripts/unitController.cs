using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitController : MonoBehaviour
{

    public GameObject follower;
    public int unitNum;

    float speed = 5f;
    public float health;
    float time;
    float timer = 2f;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        //Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        gameObject.transform.position = 
        Vector3.MoveTowards(transform.position, new Vector3(follower.transform.position.x, gameObject.transform.position.y, follower.transform.position.z), Time.deltaTime * speed);

        if(time>timer)
        {
            time = 0;
            health -= 10;
        }

        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameManager_KDG.Instance.unitcheck--;
        Destroy(gameObject);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.tag == "Apoint")
    //    {

    //    }
    //    if(other.tag == "Bpoint")
    //    {

    //    }
    //    if(other.tag == "Cpoint")
    //    {

    //    }
    //}

}
