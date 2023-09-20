using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerUnit;
    [SerializeField]
    private LayerMask layerGround;

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

        // ���콺 ������ Ŭ������ ���� �̵�
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            // ���� ������Ʈ(layerUnit)�� Ŭ������ ��
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerGround))
            {
                // ���� ������ ���� ���� ����
                Vector3 center = hit.point; // ���� �߽���
                float radius = 3.0f; // ���� ������
                int numUnits = rtsUnitController.GetSelectedUnits().Count; // ���õ� ���� ��

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
    }

}

