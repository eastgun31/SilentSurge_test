using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    public int value;   //������ �ѹ��� ABCDE ���ʴ�� 01234
    public int pointcheck; //�߸����� 0 ,�����ɽ� 1, �÷��̾ ���ɽ� 2
    public float time;

    public GameObject playerhome;
    public List<E_unitMove> e_unit = new List<E_unitMove>();
    public GameObject[] points;

    public float p_distance; //������ �÷��̾�����
    public float e_distance; //������ ������

    private int ppoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        p_distance = 100f;
        e_distance = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        //if (pointcheck == 2) //pointcheck�� 0�̾��ٰ� 2�� �Ǹ� ��ȭ ȹ��
        //{
        //    if (ppoint == 0)
        //    {
        //        GameManager.instance.gold += 200;
        //    }
        //}

        //if (pointcheck == 2) //pointcheck�� 1�̾��ٰ� 2�� �Ǹ� ��ȭ ȹ��
        //{
        //    if (ppoint == 1)
        //    {
        //        GameManager.instance.gold += 100;
        //    }
        //}

        ////if (pointcheck == 3) //pointcheck�� 1�̾��ٰ� 2�� �Ǹ� ��ȭ ȹ��
        ////{
        ////    if (ppoint == 1)
        ////    {
        ////        GameManager.instance.gold += 100;
        ////    }
        ////}

        //ppoint = pointcheck;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            e_unit.Add(other.gameObject.GetComponent<E_unitMove>());

            StartCoroutine("eUnitNewPoint", other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //if(other.tag == "Enemy" && other.tag == "Player")
        //{
        //    pointcheck = 0;
        //}

        if (other.tag == "Enemy")
        {
            e_distance = Vector3.Distance(other.transform.position, transform.position);
        }
        if (other.tag == "Player")
        {
            p_distance = Vector3.Distance(other.transform.position, transform.position);
        }

        if (e_distance <= 10f && p_distance > 10f) //�� ���ɽ�
        {
            time += Time.deltaTime;

            if (time > 10f)
            {
                pointcheck = 1;

                switch (value)
                {
                    case 0:
                        GameManager.instance.check[0] = pointcheck;
                        break;
                    case 1:
                        GameManager.instance.check[1] = pointcheck;
                        break;
                    case 2:
                        GameManager.instance.check[2] = pointcheck;
                        break;
                    case 3:
                        GameManager.instance.check[3] = pointcheck;
                        break;
                    case 4:
                        GameManager.instance.check[4] = pointcheck;
                        break;
                }

                time = 0;
            }
        }

        if (p_distance <= 10f && e_distance > 10f)
        {
            time += Time.deltaTime;

            if (pointcheck == 1) //�÷��̾ �� ���� ��������
            {
                GameManager.instance.attacking = true;

                switch (value)
                {
                    case 0:
                        GameManager.instance.attackPoint = 0;
                        break;
                    case 1:
                        GameManager.instance.attackPoint = 1;
                        break;
                    case 2:
                        GameManager.instance.attackPoint = 2;
                        break;
                    case 3:
                        GameManager.instance.attackPoint = 3;
                        break;
                    case 4:
                        GameManager.instance.attackPoint = 4;
                        break;
                }
            }

            if (time > 10f) //�÷��̾ ���ɽ�
            {
                time = 0;
                pointcheck = 2;

                switch (value)
                {
                    case 0:
                        GameManager.instance.check[0] = pointcheck;
                        break;
                    case 1:
                        GameManager.instance.check[1] = pointcheck;
                        break;
                    case 2:
                        GameManager.instance.check[2] = pointcheck;
                        break;
                    case 3:
                        GameManager.instance.check[3] = pointcheck;
                        break;
                    case 4:
                        GameManager.instance.check[4] = pointcheck;
                        break;
                }
            }
        }

        if (e_distance <= 10f && p_distance <= 10f) //�������� ������
        {
            time += Time.deltaTime;

            if (time > 10f)
            {
                pointcheck = 3;

                switch (value)
                {
                    case 0:
                        GameManager.instance.check[0] = pointcheck;
                        break;
                    case 1:
                        GameManager.instance.check[1] = pointcheck;
                        break;
                    case 2:
                        GameManager.instance.check[2] = pointcheck;
                        break;
                    case 3:
                        GameManager.instance.check[3] = pointcheck;
                        break;
                    case 4:
                        GameManager.instance.check[4] = pointcheck;
                        break;
                }

                time = 0;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            time = 0;
            e_distance = 100f;
        }

        if (other.tag == "Player")
        {
            time = 0;
            GameManager.instance.attacking = false;
            p_distance = 100f;
        }
    }

    IEnumerator eUnitNewPoint(GameObject other)
    {
        int random = Random.Range(0, 5);
        if (pointcheck == 1)
        {


            if (GameManager.instance.attacking == true)
            {
                for (int i = 0; i < e_unit.Count; i++)
                {
                    e_unit[i].MovePoint(points[GameManager.instance.attackPoint].transform.position);
                    e_unit.Remove(e_unit[i]);
                }
            }
            else if (GameManager.instance.check[random] == 0 || GameManager.instance.check[random] == 2)
            {
                for (int i = 3; i < e_unit.Count; i++)
                {
                    e_unit[i].MovePoint(points[random].transform.position);
                    e_unit.Remove(e_unit[i]);
                }

            }
            else if (GameManager.instance.check[0] == 1 && GameManager.instance.check[1] == 1 && GameManager.instance.check[2] == 1 && GameManager.instance.check[3] == 1 && GameManager.instance.check[4] == 1)
            {
                for (int i = 0; i < e_unit.Count; i++)
                {
                    e_unit[i].MovePoint(playerhome.transform.position);
                    e_unit.Remove(e_unit[i]);
                }

            }

        }

        yield return new WaitForSeconds(1f);

        StartCoroutine("eUnitNewPoint", other.gameObject);
    }
}
