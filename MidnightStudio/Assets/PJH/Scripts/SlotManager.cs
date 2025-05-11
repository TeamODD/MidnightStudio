using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public static SlotManager Instance { get; private set; }

    // 슬롯 인덱스를 키로, 해당 슬롯에 있는 SceneDrag 컴포넌트를 값으로 저장
    private Dictionary<int, SceneDrag> sceneInSlot = new Dictionary<int, SceneDrag>();

    // 슬롯의 총 개수 (Inspector에서 설정하거나 코드로 찾도록 수정 가능)
    public int totalSlots = 5; // 예시: 슬롯이 3개라고 가정

    void Awake()
    {
        if (Instance == null)
        { 
            Instance = this;
            InitializeSlots();
            // DontDestroyOnLoad(gameObject); // 씬 전환 시 유지해야 한다면 주석 해제
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InitializeSlots()
    {
        // 먼저 모든 슬롯을 비어있는 상태로 초기화 (혹시 슬롯 인덱스가 건너뛰는 경우 대비)
        for (int i = 0; i < totalSlots; i++)
        {
            sceneInSlot[i] = null;
        }

        // 씬에 있는 모든 Slot 컴포넌트를 찾음
        Slot[] allSlots = FindObjectsOfType<Slot>();
        foreach (Slot slot in allSlots)
        {
            SceneDrag initialScene = slot.GetComponentInChildren<SceneDrag>(); // 슬롯의 자식에서 SceneDrag 찾기
            UpdateSlot(slot.slotIndex, initialScene); // 찾은 씬으로 해당 슬롯 정보 업데이트
        }
    }

    // 특정 슬롯의 씬 정보를 업데이트하는 함수
    public void UpdateSlot(int slotIndex, SceneDrag scene)
    {
        if (slotIndex >= 0 && slotIndex < totalSlots)
        {
            sceneInSlot[slotIndex] = scene;
            Debug.Log($"Slot {slotIndex} updated with scene: {(scene != null ? scene.gameObject.name : "Empty")}");
        }
    }

    // 특정 슬롯에 어떤 씬이 있는지 가져오는 함수 (필요 시 사용)
    public SceneDrag GetSceneInSlot(int slotIndex)
    {
        if (sceneInSlot.ContainsKey(slotIndex))
        {
            return sceneInSlot[slotIndex];
        }
        return null;
    }

    // 전체 슬롯 상태를 확인하는 디버그용 함수 (필요 시 사용)
    public void PrintSlotStatus()
    {
        Debug.Log("--- Slot Status ---");
        for (int i = 0; i < totalSlots; i++)
        {
            Debug.Log($"Slot {i}: {(sceneInSlot.ContainsKey(i) && sceneInSlot[i] != null ? sceneInSlot[i].gameObject.name : "Empty")}");
        }
        Debug.Log("-------------------");
    }

    public void SaveSlotResultsForNextScene()
    {
        if (ResultData.Instance != null)
        {
            ResultData.Instance.SaveSlotData(sceneInSlot, totalSlots);
        }
    }
}