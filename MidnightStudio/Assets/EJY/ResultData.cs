using System.Collections.Generic;
using UnityEngine;

public class ResultData : MonoBehaviour
{
    public static ResultData Instance { get; private set; }
    public GaugeControl gaugeControl;

    // 슬롯 인덱스 순서대로 Scene Identifier만 저장
    public List<string> orderedSceneIdentifiers = new List<string>();
    public float finish_timer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveSlotData(Dictionary<int, SceneDrag> sceneInSlot, int totalSlots)
    {
        orderedSceneIdentifiers.Clear();

        for (int i = 0; i < totalSlots; i++)
        {
            if (sceneInSlot.ContainsKey(i) && sceneInSlot[i] != null)
            {
                orderedSceneIdentifiers.Add(sceneInSlot[i].sceneIdentifier);
            }
            else
            {
                orderedSceneIdentifiers.Add(""); // 비어 있는 슬롯 처리
            }
        }
        finish_timer = gaugeControl.step_timer;

        Debug.Log("씬 식별자 저장 완료: " + string.Join(", ", orderedSceneIdentifiers));
    }
}
