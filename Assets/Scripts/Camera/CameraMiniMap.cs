using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMiniMap : MonoBehaviour, IPointerClickHandler
{
    public Camera mainCamera; // �� ���� ī�޶�
    public RectTransform miniMapRect; // �̴ϸ� UI ����� RectTransform

    public void OnPointerClick(PointerEventData eventData)
    {
        // Ŭ���� ��ġ�� �̴ϸ� ���� ���� ��ǥ�� �����ɴϴ�.
        Vector2 localClick;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle
            (miniMapRect, eventData.position, eventData.pressEventCamera, out localClick))
        {
            Debug.Log("���� ��ǥ" + localClick);
            Vector3 worldPosition = new Vector3
                (localClick.x+253, mainCamera.transform.position.y, localClick.y+245);

            // ī�޶� �ش� ��ġ�� �̵��մϴ�.
            mainCamera.transform.position = worldPosition;
            Debug.Log("���� ��ǥ" + worldPosition);
            // ī�޶� �ش� ��ġ�� �̵��մϴ�.
            mainCamera.transform.position = worldPosition;
        }
    }
}