using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lv1_LCL : MonoBehaviour
{
    int Gold;
    int U;

    public Button button_warrio_1Lr; //검사 1레벨 판매 버튼
    public GameObject Warrior_1L_Obj;//검사 1레벨 오브젝트

    public Button button_shield_1Lr; //방패병 1레벨 판매 버튼
    public GameObject Shield_1L_Obj;//방패병 1레벨 오브젝트

    public Button button_archer_1L; //궁수 1레벨 판매 버튼
    public GameObject Archer_1L_Obj; //궁수 1레벨 오브젝트

    public Button button_horseMan_1L; //기마병 1레벨 판매 버튼
    public GameObject HorseMan_1L_Obj; //기마병 1레벨 오브젝트

    public Button UpButton2L; //본부 2레벨로 업그레이드 시키는 버튼

    // Start is called before the first frame update
    void Start()
    {

        button_warrio_1Lr.onClick.AddListener(Down_warrior_1L); //검사 1레벨 판매 버튼 클릭 연결
        button_shield_1Lr.onClick.AddListener(Down_shield_1L); //방패병 1레벨 판매 버튼 클릭 연결
        button_archer_1L.onClick.AddListener(Down_archer_1L); //궁수 1레벨 판매 버튼 클릭 연결
        button_horseMan_1L.onClick.AddListener(Down_horseMan_1L); //기마병 1레벨 판매 버튼 클릭 연결
        UpButton2L.onClick.AddListener(Up_2L); //본부 2레벨로 업그레이드 버튼 클릭 연결
    }

    // Update is called once per frame
    void Update()
    {
        Gold = GameManager.instance.gold;
        U = GameManager.instance.Obj;

        button_warrio_1Lr.interactable = (Gold >= 5); //검사 1레벨 5코인이 없을 시 버튼 비활성화
        button_shield_1Lr.interactable = (Gold >= 5); //방패병 1레벨 5코인이 없을 시 버튼 비활성화
        button_archer_1L.interactable = (Gold >= 8); //궁수 1레벨 8코인이 없을 시 버튼 비활성화
        button_horseMan_1L.interactable = (Gold >= 15); //기마병 1레벨 15코인이 없을 시 버튼 비활성화
        UpButton2L.interactable = (Gold >= 350); //본부 2레벨로 업그레이드 350코인이 없을 시 버튼 비활성화
    }


    private void Down_warrior_1L()
    {
        if (U < 30)//검사 유닛 제한 30
        {
            if (Gold >= 5)
            {
                GameManager.instance.gold -= 5;
                GameObject newObject = Instantiate(Warrior_1L_Obj, transform.position, Quaternion.identity); //검사 1레벨 유닛 생성
                GameManager.instance.Obj++;
                GameManager.instance.Aobj();
            }
        }
    }

    private void Down_shield_1L()
    {
        if (U < 30)//방패병 유닛 제한 30
            if (Gold >= 5)
            {
                GameManager.instance.gold -= 5; //버튼을 누르면 검사 1레벨 필요 재화 5씩 없앰
                GameObject newObject = Instantiate(Shield_1L_Obj, transform.position, Quaternion.identity); //방패병 1레벨 유닛 생성
                GameManager.instance.Obj++;
                GameManager.instance.Aobj();
            }
    }

    private void Down_archer_1L()
    {
        if (U < 30)//궁수 유닛 제한 30
            if (Gold >= 8)
            {
                GameManager.instance.gold -= 8; //버튼을 누르면 궁수 1레벨 필요 재화 8씩 없앰
                GameObject newObject = Instantiate(Archer_1L_Obj, transform.position, Quaternion.identity); //궁수 1레벨 유닛 생성
                GameManager.instance.Obj++;
                GameManager.instance.Aobj();
            }
    }

    private void Down_horseMan_1L()
    {
        if (U < 30)//기마병 유닛 제한 30
            if (Gold >= 15)
            {
                GameManager.instance.gold -= 15; //버튼을 누르면 기마병 1레벨 필요 재화 15씩 없앰
                GameObject newObject = Instantiate(HorseMan_1L_Obj, transform.position, Quaternion.identity); //기마병 1레벨 유닛 생성
                GameManager.instance.Obj++;
                GameManager.instance.Aobj();
            }
    }

    private void Up_2L()
    {
        if (Gold >= 350)
        {
            GameManager.instance.gold -= 350; //버튼을 누르면 본부 2레벨로 업그레이드 하기 위한 필요 재화 350 없앰

        }
    }
}
