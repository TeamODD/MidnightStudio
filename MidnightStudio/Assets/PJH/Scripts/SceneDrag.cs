using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;

public class SceneDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject beingDraggedIcon;
    Vector3 startPosition;
    Vector3 offset; // 마우스와 객체 위치 간의 오프셋
    Vector2 startSizeDelta; // 원래 크기를 저장할 변수 추가
    [SerializeField] Transform onDragParent;
    [HideInInspector] public Transform startParent;
    [HideInInspector] public int startSlotIndex = -1; // 드래그 시작 슬롯 인덱스 (-1은 슬롯 외부 또는 초기 상태)
    private RectTransform rectTransform; // RectTransform 캐싱

    public void OnBeginDrag(PointerEventData eventData) {
    
        beingDraggedIcon = gameObject;
        startPosition = transform.position;
        offset = transform.position - Input.mousePosition; // 드래그 시작 시 오프셋 계산
        rectTransform = GetComponent<RectTransform>(); // RectTransform 가져오기
        startSizeDelta = rectTransform.sizeDelta; // 시작 시 크기 저장
        startParent = transform.parent;
        // 시작 슬롯 인덱스 찾기
        Slot startSlot = startParent.GetComponent<Slot>();
        if (startSlot != null) {
            startSlotIndex = startSlot.slotIndex;
            // SlotManager 업데이트: 시작 슬롯을 비움 (임시)
            SlotManager.Instance.UpdateSlot(startSlotIndex, null);
        } else { startSlotIndex = -1; } // 슬롯 위에서 시작한 게 아니면 -1
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        transform.SetParent(onDragParent);
 
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition + offset; // 마우스 위치에 오프셋을 더해 위치 설정
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        beingDraggedIcon = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if(transform.parent == onDragParent)
        {
            transform.position = startPosition;
            transform.SetParent(startParent);
            rectTransform.sizeDelta = startSizeDelta; // 원래 크기로 복원하는 코드 추가
            // SlotManager 업데이트: 원래 슬롯으로 복원
            if (startSlotIndex != -1) SlotManager.Instance.UpdateSlot(startSlotIndex, this);
        }
        if (SlotManager.Instance != null) SlotManager.Instance.PrintSlotStatus();
    }

    // Slot 스크립트에서 원래 크기를 가져갈 수 있도록 함수 추가
    public Vector2 GetOriginalSize() {
        return startSizeDelta;
    }
}
