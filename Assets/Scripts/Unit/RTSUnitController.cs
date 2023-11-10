using System.Collections.Generic;
using UnityEngine;
using static UnitController;

public class RTSUnitController : MonoBehaviour
{
    [SerializeField]
    private All_Lv_LCL all_Lv_LCL;

    public List<UnitController> selectedUnitList = new List<UnitController>(); // �÷��̾ Ŭ�� or �巡�׷� ������ ����
    public List<UnitController> UnitList = new List<UnitController>(); // �ʿ� �����ϴ� ��� ����

    public static RTSUnitController instance = null;

    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    // ���콺 Ŭ������ ������ ������ �� ȣ��
    public void ClickSelectUnit(UnitController newUnit)
    {
        // ������ ���õǾ� �ִ� ��� ���� ����
        DeselectAll();

        SelectUnit(newUnit);
    }

    // Shift+���콺 Ŭ������ ������ ������ �� ȣ��
    public void ShiftClickSelectUnit(UnitController newUnit)
    {
        // ������ ���õǾ� �ִ� ������ ����������
        if (selectedUnitList.Contains(newUnit))
        {
            DeselectUnit(newUnit);
        }
        // ���ο� ������ ����������
        else
        {
            SelectUnit(newUnit);
        }
    }
    // Ctrl + ���콺 Ŭ������ ���� ���� �ѹ��� ����
    public void CtrlClickSelelctUnit(UnitController newUnit)
    {
        DeselectAll(); // ��� ���� ���� ����

        foreach (UnitController unit in UnitList)
        {
            if (unit.t_State == newUnit.t_State)
            {
                SelectUnit(unit); // ���ϴ� ���� Ÿ�Ը� ����
            }
        }
    }

    //���콺 �巡�׷� ������ ������ �� ȣ��
    public void DragSelectUnit(UnitController newUnit)
    {
        // ���ο� ������ ����������
        if (!selectedUnitList.Contains(newUnit))
        {
            SelectUnit(newUnit);
        }
    }

    //// ���õ� ��� ������ �̵��� �� ȣ��
    //public void MoveSelectedUnits(Vector3 end)
    //{
    //    for (int i = 0; i < selectedUnitList.Count; ++i)
    //    {
    //        selectedUnitList[i].MoveTo(end);
    //    }
    //}

    //���õ� ���� �� ��ȯ
    public List<UnitController> GetSelectedUnits()
    {
        return selectedUnitList;
    }

    // ��� ������ ������ ������ �� ȣ��
    public void DeselectAll()
    {
        for (int i = 0; i < selectedUnitList.Count; ++i)
        {
            selectedUnitList[i].DeselectUnit();
        }

        selectedUnitList.Clear();
    }

    // �Ű������� �޾ƿ� newUnit ���� ����
    private void SelectUnit(UnitController newUnit)
    {
        // ������ ���õǾ��� �� ȣ���ϴ� �޼ҵ�
        newUnit.SelectUnit();
        // ������ ���� ������ ����Ʈ�� ����
        selectedUnitList.Add(newUnit);
    }

    // �Ű������� �޾ƿ� newUnit ���� ���� ����
    private void DeselectUnit(UnitController newUnit)
    {
        // ������ �����Ǿ��� �� ȣ���ϴ� �޼ҵ�
        newUnit.DeselectUnit();
        // ������ ���� ������ ����Ʈ���� ����
        selectedUnitList.Remove(newUnit);
    }

    //���õ� ���ֵ��� Ÿ�� �ʱ�ȭ
    public void targetClear()
    {
        for (int i = 0; i < selectedUnitList.Count; ++i)
        {
            selectedUnitList[i].targetUnit = null;
        }
    }
}

