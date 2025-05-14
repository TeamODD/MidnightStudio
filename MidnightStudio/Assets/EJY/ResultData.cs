using System.Collections.Generic;
using UnityEngine;

public class ResultData : MonoBehaviour
{
    public static ResultData Instance { get; private set; }
    public GaugeControl gaugeControl;

    // ���� �ε��� ������� Scene Identifier�� ����
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
                orderedSceneIdentifiers.Add(""); // ��� �ִ� ���� ó��
            }
        }
        finish_timer = gaugeControl.step_timer;

        Debug.Log("�� �ĺ��� ���� �Ϸ�: " + string.Join(", ", orderedSceneIdentifiers));
    }
}
