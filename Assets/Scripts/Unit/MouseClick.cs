using System.Collections.Generic;
using UnityEngine;
using static UnitController;

public class MouseClick : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerUnit;
    [SerializeField]
    private LayerMask layerGround;

    public GameObject ClickPointer;
    private GameObject ClickPointer_instance;

    private Camera mainCamera;
    private RTSUnitController rtsUnitController;

    private void Awake()
    {
        mainCamera = Camera.main;
        rtsUnitController = GetComponent<RTSUnitController>();
    }

    private void Update()
    {
        // ���콺 ���� Ŭ������ ���� ���� or ����
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            // ������ �ε����� ������Ʈ�� ���� �� (=������ Ŭ������ ��)
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerUnit))
            {
                if (hit.transform.GetComponent<UnitController>() == null) return;

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    rtsUnitController.ShiftClickSelectUnit(hit.transform.GetComponent<UnitController>());
                }
                else if (Input.GetKey(KeyCode.LeftControl))
                {
                    rtsUnitController.CtrlClickSelelctUnit(hit.transform.GetComponent<UnitController>());
                }
                else
                {
                    rtsUnitController.ClickSelectUnit(hit.transform.GetComponent<UnitController>());
                }
            }
            // ������ �ε����� ������Ʈ�� ���� ��
            else
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    rtsUnitController.DeselectAll();
                }
            }
        }

        // ���콺 ������ Ŭ������ ���� �̵�
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            Skill.instance.CancelSkill();

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerGround))
            {
                // ���� ������ ���� ���� ����
                Vector3 center = hit.point; // ���� �߽���
                float radius = 3.0f; // ���� ������
                int numUnits = rtsUnitController.GetSelectedUnits().Count; // ���õ� ���� ��

                //Ŭ�� ����Ʈ
                ClickPointer_instance = Instantiate(ClickPointer);
                ClickPointer_instance.transform.position = hit.point;
                Destroy(ClickPointer_instance, 0.7f);

                // �� ������ �������� ����
                List<UnitController> selectedUnits = rtsUnitController.GetSelectedUnits();
                for (int i = 0; i < numUnits; i++)
                {
                    // ���� ���
                    float angle = i * (360f / numUnits);

                    // ���� ���� ��ġ ���
                    Vector3 unitPosition = center + new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), 0, Mathf.Sin(Mathf.Deg2Rad * angle)) * radius;
                    selectedUnits[i].MoveTo(unitPosition);
                }
            }
        }

        // ���콺 ������ Ŭ������ ���� �̵�
        //if (Input.GetMouseButtonDown(1))
        //{
        //    RaycastHit hit;
        //    Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        //    // ���� ������Ʈ(layerUnit)�� Ŭ������ ��
        //    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerGround))
        //    {
        //        rtsUnitController.MoveSelectedUnits(hit.point);
        //    }
        //}
    }
}