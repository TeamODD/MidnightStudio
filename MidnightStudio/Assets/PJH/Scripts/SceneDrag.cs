using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;

public class SceneDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject beingDraggedIcon;
    private bool firstDragFrame = true;
    Vector3 startPosition;
    Vector3 offset; // 마우스와 객체 위치 간의 오프셋
    Vector2 startSizeDelta; // 원래 크기를 저장할 변수 추가
    [SerializeField] Transform onDragParent;
    [HideInInspector] public Transform startParent;
    [HideInInspector] public int startSlotIndex = -1; // 드래그 시작 슬롯 인덱스 (-1은 슬롯 외부 또는 초기 상태)
    private RectTransform rectTransform; // RectTransform 캐싱

    public QuestionManager QuestionManager;

    // 각 씬을 식별하기 위한 ID (QuestionManager의 sceneIndex와 일치하도록 설정)
    public string sceneIdentifier;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (QuestionManager.canClick)
        {
            beingDraggedIcon = gameObject;
            rectTransform = GetComponent<RectTransform>();
            startPosition = transform.position;
            startSizeDelta = rectTransform.sizeDelta;
            startParent = transform.parent;

            // Slot 정보 갱신
            Slot startSlot = startParent.GetComponent<Slot>();
            if (startSlot != null)
            {
                startSlotIndex = startSlot.slotIndex;
                SlotManager.Instance.UpdateSlot(startSlotIndex, null);
            }
            else
            {
                startSlotIndex = -1;
            }

            GetComponent<CanvasGroup>().blocksRaycasts = false;

            //부모를 월드 좌표 유지한 채로 변경
            transform.SetParent(onDragParent, false);

            //마우스 위치로 정확히 이동
            transform.position = Input.mousePosition;

            offset = Vector3.zero;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (QuestionManager.canClick)
        {
            if (firstDragFrame)
            {
                transform.position = Input.mousePosition; // 첫 프레임엔 강제로 보정
                firstDragFrame = false;
            }
            else
            {
                transform.position = Input.mousePosition + offset;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (QuestionManager.canClick)
        {
            beingDraggedIcon = null;
            GetComponent<CanvasGroup>().blocksRaycasts = true;

            // 새로운 슬롯 감지 시도
            Slot newSlot = GetComponentInParent<Slot>();

            if (transform.parent == onDragParent)
            {
                transform.position = startPosition;
                transform.SetParent(startParent);
                rectTransform.sizeDelta = startSizeDelta; // 원래 크기로 복원하는 코드 추가
                                                          // SlotManager 업데이트: 원래 슬롯으로 복원
                if (startSlotIndex != -1) SlotManager.Instance.UpdateSlot(startSlotIndex, this);
            }
            else if (newSlot != null)
            {
                // 새로운 슬롯에 배치된 경우: SlotManager 업데이트
                SlotManager.Instance.UpdateSlot(newSlot.slotIndex, this);

                // 화살표 업데이트 (QuestionManager 호출)
                QuestionManager qm = FindObjectOfType<QuestionManager>();
                if (qm != null)
                {
                    qm.changeSceneQuestion(sceneIdentifier);
                    qm.ActivateArrowBySlotIndex(newSlot.slotIndex);
                }
            }
            if (SlotManager.Instance != null) SlotManager.Instance.PrintSlotStatus();
        }
    }

    // Slot 스크립트에서 원래 크기를 가져갈 수 있도록 함수 추가
    public Vector2 GetOriginalSize()
    {
        return startSizeDelta;
    }
}
