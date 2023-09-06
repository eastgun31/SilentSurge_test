using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lv3_LCL : MonoBehaviour
{
    int Gold;
    int U;

    public Button button_warrio_3Lr; //검사 3레벨 판매 버튼
    public GameObject Warrior_3L_Obj;//검사 3레벨 오브젝트

    public Button button_shield_3Lr; //방패병 3레벨 판매 버튼
    public GameObject Shield_3L_Obj;//방패병 3레벨 오브젝트

    public Button button_archer_3L; //궁수 3레벨 판매 버튼
    public GameObject Archer_3L_Obj; //궁수 3레벨 오브젝트

    public Button button_horseMan_3L; //기마병 3레벨 판매 버튼
    public GameObject HorseMan_3L_Obj; //기마병 3레벨 오브젝트

    // Start is called before the first frame update
    void Start()
    {

        button_warrio_3Lr.onClick.AddListener(Down_warrior_3L); //검사 3레벨 판매 버튼 클릭 연결
        button_shield_3Lr.onClick.AddListener(Down_shield_3L); //방패병 3레벨 판매 버튼 클릭 연결
        button_archer_3L.onClick.AddListener(Down_archer_3L); //궁수 3레벨 판매 버튼 클릭 연결
        button_horseMan_3L.onClick.AddListener(Down_horseMan_3L); //기마병 3레벨 판매 버튼 클릭 연결
    }

    // Update is called once per frame
    void Update()
    {
        Gold = GameManager.instance.gold;
        U = GameManager.instance.Obj;

        button_warrio_3Lr.interactable = (Gold >= 5); //검사 2레벨 5코인이 없을 시 버튼 비활성화
        button_shield_3Lr.interactable = (Gold >= 5); //방패병 2레벨 5코인이 없을 시 버튼 비활성화
        button_archer_3L.interactable = (Gold >= 8); //궁수 2레벨 8코인이 없을 시 버튼 비활성화
        button_horseMan_3L.interactable = (Gold >= 15); //기마병 2레벨 15코인이 없을 시 버튼 비활성화
    }


    private void Down_warrior_3L()
    {
        if (U < 30)//검사 유닛 제한 30
        {
            if (Gold >= 5)
            {
                GameManager.instance.gold -= 5;
                GameObject newObject = Instantiate(Warrior_3L_Obj, transform.position, Quaternion.identity); //검사 2레벨 유닛 생성
                GameManager.instance.Obj++;
                GameManager.instance.Aobj();
            }
        }
    }

    private void Down_shield_3L()
    {
        if (U < 30)//방패병 유닛 제한 30
            if (Gold >= 5)
            {
                GameManager.instance.gold -= 5; //버튼을 누르면 검사 1레벨 필요 재화 5씩 없앰
                GameObject newObject = Instantiate(Shield_3L_Obj, transform.position, Quaternion.identity); //방패병 2레벨 유닛 생성
                GameManager.instance.Obj++;
                GameManager.instance.Aobj();
            }
    }

    private void Down_archer_3L()
    {
        if (U < 30)//궁수 유닛 제한 30
            if (Gold >= 8)
            {
                GameManager.instance.gold -= 8; //버튼을 누르면 궁수 1레벨 필요 재화 8씩 없앰
                GameObject newObject = Instantiate(Archer_3L_Obj, transform.position, Quaternion.identity); //궁수 2레벨 유닛 생성
                GameManager.instance.Obj++;
                GameManager.instance.Aobj();
            }
    }

    private void Down_horseMan_3L()
    {
        if (U < 30)//기마병 유닛 제한 30
            if (Gold >= 15)
            {
                GameManager.instance.gold -= 15; //버튼을 누르면 기마병 1레벨 필요 재화 15씩 없앰
                GameObject newObject = Instantiate(HorseMan_3L_Obj, transform.position, Quaternion.identity); //기마병 2레벨 유닛 생성
                GameManager.instance.Obj++;
                GameManager.instance.Aobj();
            }
    }
}
