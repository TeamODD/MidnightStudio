using UnityEngine;
using UnityEngine.EventSystems;
public class Slot : MonoBehaviour, IDropHandler
{
    // 각 슬롯의 고유 인덱스 (Inspector에서 0부터 순서대로 설정)
    public int slotIndex;


    GameObject Icon() {
        if(transform.childCount > 0)
            return transform.GetChild(0).gameObject;
        else
            return null;
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject draggedIcon = SceneDrag.beingDraggedIcon;
        if (draggedIcon == null) return; // 드래그 중인 아이콘이 없으면 종료

        SceneDrag dragScript = draggedIcon.GetComponent<SceneDrag>();
        if (dragScript == null) return; // SceneDrag 스크립트가 없으면 종료
        int startSlotIndex = dragScript.startSlotIndex; // 드래그 시작 슬롯 인덱스 가져오기

        Transform startParent = dragScript.startParent; // 드래그 시작 슬롯 (원래 부모)
        Vector2 originalSize = dragScript.GetOriginalSize(); // 드래그 시작 시 아이콘의 크기

        GameObject existingIcon = Icon(); // 현재 슬롯에 이미 있는 아이콘 가져오기

        if(existingIcon == null) // 1. 현재 슬롯이 비어있는 경우
        {
            // 드래그한 아이콘을 이 슬롯으로 이동
            RectTransform droppedRect = draggedIcon.GetComponent<RectTransform>(); // RectTransform 가져오기
            draggedIcon.transform.SetParent(transform);
            draggedIcon.transform.position = transform.position; // 월드 포지션 사용 (롤백)
            if (droppedRect != null) {

                droppedRect.sizeDelta = originalSize;
            }
            // SlotManager 업데이트: 현재 슬롯에는 드롭된 아이콘, 시작 슬롯은 비움
            SlotManager.Instance.UpdateSlot(slotIndex, dragScript);
            if (startSlotIndex != -1) SlotManager.Instance.UpdateSlot(startSlotIndex, null); // 시작 슬롯 정보가 있으면 비움
        }
        else // 2. 현재 슬롯에 이미 아이콘이 있는 경우 (아이템 교체)
        {
            if (existingIcon == draggedIcon) return; // 자기 자신 위에 드롭하는 경우는 무시

            // 기존 아이콘(existingIcon)을 드래그 시작 슬롯(startParent)으로 이동
            RectTransform existingRect = existingIcon.GetComponent<RectTransform>(); // RectTransform 가져오기
            existingIcon.transform.SetParent(startParent);
            existingIcon.transform.position = startParent.position; // 월드 포지션 사용 (롤백)
            // 기존 아이콘의 크기도 드래그했던 아이콘의 원래 크기로 설정 (슬롯 간 크기 일관성 유지)
            if (existingRect != null)
            {
                // existingRect.anchoredPosition = Vector2.zero; // 롤백
                existingRect.sizeDelta = originalSize;
            }
            // SlotManager 업데이트: 시작 슬롯에는 기존 아이콘(existingIcon)
            SceneDrag existingDragScript = existingIcon.GetComponent<SceneDrag>();
            if (startSlotIndex != -1) SlotManager.Instance.UpdateSlot(startSlotIndex, existingDragScript);

            // 드래그한 아이콘(draggedIcon)을 현재 슬롯(transform)으로 이동
            RectTransform draggedRect = draggedIcon.GetComponent<RectTransform>(); // RectTransform 가져오기
            draggedIcon.transform.SetParent(transform);
            draggedIcon.transform.position = transform.position; // 월드 포지션 사용 (롤백)
            if (draggedRect != null) {
                draggedRect.sizeDelta = originalSize; // 원래 크기로 복원
            }
            // SlotManager 업데이트: 현재 슬롯에는 드래그한 아이콘(draggedIcon)
            SlotManager.Instance.UpdateSlot(slotIndex, dragScript);
        }
        // SlotManager.Instance.PrintSlotStatus(); // 상태 확인용 (디버깅 시 주석 해제)
    }
}
