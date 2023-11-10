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
            Vector3 worldPosition = new Vector3
                (localClick.x+248, mainCamera.transform.position.y, localClick.y+235);

            // ī�޶� �ش� ��ġ�� �̵��մϴ�.
            mainCamera.transform.position = worldPosition;
            // ī�޶� �ش� ��ġ�� �̵��մϴ�.
            mainCamera.transform.position = worldPosition;
        }
    }
}