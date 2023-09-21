using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //���ֽ���
    public int health = 100;
    public int attackPower = 10;
    public int defense = 3;
    public float moveSpeed = 7;

    public Text Allgold_text; //���� ��ȭ ����
    public int gold = 1000; //�ʱ� ��ȭ + 

    public Text All_ObjText;//���� �� �����ִ� �ؽ�Ʈ
    public int All_Obj = 0;//���� �� ���� ��

    public int[] check = { 0, 0, 0, 0, 0 };   //����, 0 �߸�, 1 ��������, 2�÷��̾�, 3�������� ����

    public bool attacking = false;  //���� ���ݹ޴»���
    public int attackPoint = 0;     //���� ���ݹ޴� ����
    public int e_population = 4;    //�����ּ�

    public bool pointBattle = false; //��������
    public bool battle = false;      //��������

    public bool pointCan = false;

    //���� �ð�
    public Text timerText;
    private float startTime;
    private float currentTime = 901.0f;

    //��ų ��Ÿ��
    public Text active_Skill;
    public Text buff_Skill;
    //�Ҹ� ��ų ���� Ƚ��
    public Text item_Skill;

    public void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        startTime = currentTime;
        active_Skill.gameObject.SetActive(false);
        buff_Skill.gameObject.SetActive(false);

        InvokeRepeating("Upgold", 1.0f, 1.0f); //1�� �Ŀ� 1�ʸ���
    }

    private void Update()
    {
        Allgold_text.text = " " + gold; //���� ��ȭ

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            // Ÿ�̸Ӱ� ������ �� �������� ���Ŀ� ���� �߰�
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
        gold += 2; //��ȭ 2�� ����
    }

    public void Aobj()
    {
        All_ObjText.text = " " + All_Obj.ToString(); //���� ����
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
