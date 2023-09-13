using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_unitMove : MonoBehaviour
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
        follower = transform.parent.gameObject;
        health = 100;
        //Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        gameObject.transform.position =
        Vector3.MoveTowards(transform.position, new Vector3(follower.transform.position.x, gameObject.transform.position.y, follower.transform.position.z), Time.deltaTime * speed);

        if (time > timer)
        {
            time = 0;
            health -= 4;
        }

        if (health <= 0)
        {
            Die();
        }
    }

    //private void OnEnable()
    //{
    //    follower = transform.parent.gameObject;
    //}

    void Die()
    {
        GameManager.instance.e_population--;
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
        }

    }

}
