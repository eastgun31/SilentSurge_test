using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject win;  //�¸�ȭ��
    public GameObject lose; //�й�ȭ��
    public GameObject draw; //���º�ȭ��

    //���ֽ���
    public int health = 100;
    public int attackPower = 10;
    public int defense = 3;
    public float moveSpeed = 7;

    public Text Allgold_text; //���� ��ȭ ����
    public int gold = 300; //�ʱ� ��ȭ + 

    public Text All_ObjText;//���� �� �����ִ� �ؽ�Ʈ
    public int All_Obj = 0;//���� �� ���� ��

    public int[] check = { 0, 0, 0, 0, 0 };   //����, 0 �߸�, 1 ��������, 2�÷��̾�, 3�������� ����

    public bool attacking = false;  //���� ���ݹ޴»���
    public int attackPoint = 0;     //���� ���ݹ޴� ����
    public int e_population = 0;    //�����ּ�

    public bool pointBattle = false; //��������
    public bool battle = false;      //��������

    public bool pointCan = false;

    public int p_score = 0;
    public int e_score = 0;

    //���� �ð�
    public Text timerText;
    private float startTime;
    private float currentTime = 181.0f;

    //��ų ��Ÿ��
    public Text active_Skill;
    public Text buff_Skill;
    //�Ҹ� ��ų ���� Ƚ��
    public Text item_Skill;

    public void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);

        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        Aobj();
        e_population = 0;
        startTime = currentTime;
        active_Skill.gameObject.SetActive(false);
        buff_Skill.gameObject.SetActive(false);

        gold = 300;

        InvokeRepeating("Upgold", 1.0f, 1.0f); //1�� �Ŀ� 1�ʸ���
    }

    private void Update()
    {
        //�ÿ��� �ð����� �ڵ� ���� �����
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Time.timeScale == 0)
                Time.timeScale = 1;
            else
                Time.timeScale = 0;
        }

        Allgold_text.text = " " + gold; //���� ��ȭ

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            Time.timeScale = 0;
            currentTime = 0;
            // Ÿ�̸Ӱ� ������ �� �������� ���Ŀ� ���� �߰�
            int a = 0;
            int b = 0;
            for (int i = 0; i < 5; i++)
            {
                if (check[i] == 1)
                    a++;
                if (check[i] == 2)
                    b++;
            }

            if (a > b)
            {
                Debug.Log("�й�");
                lose.SetActive(true);
            }
            else if (a < b)
            {
                Debug.Log("�¸�");
                win.SetActive(true);
            }
            else if (a == b)
            {
                if (e_score > p_score)
                {
                    Debug.Log("�й�");
                    lose.SetActive(true);
                }
                else if (e_score < p_score)
                {
                    Debug.Log("�¸�");
                    win.SetActive(true);
                }
            }

        }

        // �ð��� ��:�� �������� ǥ��
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        SkillCool_UI();
        ItemSkill_UI();
    }

    private void Upgold()
    {
        gold += 5; //��ȭ 2�� ����
    }

    public void Aobj()
    {
        All_ObjText.text = All_Obj.ToString() + " / 30"; //���� ����
    }

    public void SkillCool_UI()
    {
        if (Skill.instance.currentCooldown_1 > 0.0f)
        {
            active_Skill.gameObject.SetActive(true);
            active_Skill.text = " " + Skill.instance.currentCooldown_1.ToString("0");
        }
        else
            active_Skill.gameObject.SetActive(false);

        if (Skill.instance.currentCooldown_2 > 0.0f)
        {
            buff_Skill.gameObject.SetActive(true);
            buff_Skill.text = " " + Skill.instance.currentCooldown_2.ToString("0");
        }
        else
            buff_Skill.gameObject.SetActive(false);
    }

    public void ItemSkill_UI()
    {
        if (Skill.instance.itemLimit > 0)
        {
            item_Skill.text = " " + Skill.instance.itemLimit.ToString("0");
        }
        else
        {
            // ������ ����Ʈ�� 0 ������ ��� �ؽ�Ʈ�� ���������� ����
            item_Skill.text = " " + Skill.instance.itemLimit.ToString("0");
            item_Skill.color = Color.red;
        }
    }
}
