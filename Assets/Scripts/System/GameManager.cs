using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject scoreboard;  //���ھ��ȭ��
    public Text Player_Text;  //�¸� �ؽ�Ʈ
    public Text Enemy_Text;  //�¸� �ؽ�Ʈ
    public Text Player_Gold_Text; //�÷��̾� ���ǥ�� �ؽ�Ʈ
    public Text Enemy_Gold_Text; //�÷��̾� ���ǥ�� �ؽ�Ʈ
    public Text Player_Unit; //�÷��̾� ����ǥ�� �ؽ�Ʈ
    public Text Enemy_Unit; //�� ����ǥ�� �ؽ�Ʈ
    public Text Player_Point;//������ ǥ��
    public Text Enemy_Point; //���� ������ ǥ��

    //���ֽ��� �⺻ �˻� �ɷ�ġ
    public int health = 100;
    public int attackPower = 13;
    public float moveSpeed = 7;

    public Text Allgold_text; //���� ��ȭ ����
    public int gold = 300; //�ʱ� ��ȭ + 
    public int total_gold = 0;

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
    private float currentTime = 301.0f; //5�� = 301

    //��ų ��Ÿ��
    public Text active_Skill;
    public Text buff_Skill;
    //�Ҹ� ��ų ���� Ƚ��
    public Text item_Skill;
    Audio_Manager Audio_Manager;

    public bool Sound_ = true; // ���� �ѹ��� ������ bool�� �߰�

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
        Audio_Manager = FindAnyObjectByType<Audio_Manager>();
        Aobj();
        e_population = 0;
        startTime = currentTime;
        active_Skill.gameObject.SetActive(false);
        buff_Skill.gameObject.SetActive(false);

        gold = 300;
        total_gold += gold;

        InvokeRepeating("Upgold", 1.0f, 1.0f); //1�� �Ŀ� 1�ʸ���
    }

    private void Update()
    {
        //�ÿ��� �ð����� �ڵ� ���� �����
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    if (Time.timeScale == 0)
        //        Time.timeScale = 1;
        //    else
        //        Time.timeScale = 0;
        //}

        Allgold_text.text = " " + gold + "G"; //���� ��ȭ

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            Audio_Manager.StopMusic(Audio_Manager.Main_background);
            Time.timeScale = 0;
            currentTime = 0;
            scoreboard.SetActive(true); // ���ӳ����� ���ھ�� ǥ��
            Player_Gold_Text.text = total_gold.ToString() + "G"; // ���� ������ ���� ��ȭ ǥ��
            Enemy_Gold_Text.text = EnemySpawn.instance.etotal_gold.ToString() + "G"; //���� ������ ���� ���� ��ȭ ǥ��
            Player_Unit.text = All_Obj.ToString() + " / 30"; //���� ������ ǥ�õ� �츮 ����
            Enemy_Unit.text = e_population.ToString() + " / 30"; // ���� ������ ǥ�õ� ���� ����

            // Ÿ�̸Ӱ� ������ �� �������� ���Ŀ� ���� �߰�
            int a = 0;
            int b = 0;
            for (int i = 0; i < 5; i++)
            {
                if (check[i] == 1)
                    ++a;
                if (check[i] == 2)
                    ++b;
                Enemy_Point.text = a.ToString() + "/5";//���ھ�� ������ ǥ��
                Player_Point.text = b.ToString() + "/5";//���ھ�� ������ ǥ��
            }

            if (a > b)
            {
                if (Sound_)
                {
                    Audio_Manager.WinMusic(Audio_Manager.loser);
                    Sound_ = false;
                }
                Debug.Log("�й�");
                Enemy_Text.text = "�¸�";
                Player_Text.text = "�й�";
                Enemy_Text.color = Color.yellow;
                Player_Text.color = Color.red;
            }
            else if (a < b)
            {
                if (Sound_)
                {
                    Audio_Manager.WinMusic(Audio_Manager.Win);
                    Sound_ = false;
                }              
                Debug.Log("�¸�");
                Player_Text.text = "�¸�";
                Enemy_Text.text = "�й�";
                Player_Text.color = Color.yellow;
                Enemy_Text.color = Color.red;
            }

            //else if (a == b)
            //{
            //    if (e_score > p_score)
            //    {
            //        Debug.Log("�й�");
            //    }
            //    else if (e_score < p_score)
            //    {
            //        Debug.Log("�¸�");
            //    }
            //}
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
        gold += 10; //��ȭ 5�� ����
        total_gold += 10;
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
