using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Result_FindValue : MonoBehaviour
{
    public TMP_Text RemainTime_Value;
    public TMP_Text[] SceneValue;
    public TMP_Text Result;

    private float RESULT = 0f; //���� �����
    private string[] CorrectResult = {"1","3","2","4","0"};
    
    private float timer;
    private List<string> sceneIDs;
    public void Start()
    {
        // �� �ĺ��� ����Ʈ ��������
        List<string> sceneIDs = ResultData.Instance.orderedSceneIdentifiers;
        // Ÿ�̸� �� ��������
        float timer = ResultData.Instance.finish_timer;
    }

    public void RemainTimeChecking() {

        string minutes = ((int)(timer / 60)).ToString();
        string seconds = ((int)(timer % 60)).ToString();

        if (timer <= 300f)
        {
            RemainTime_Value.text = "0" + minutes + ":" + seconds;
            RESULT += 3f;
        }
        else {
            RemainTime_Value.text = "0" + minutes + ":" + seconds;
        }
    }

    public void CheckSceneNumber() {
        
        for (int i = 0; i < sceneIDs.Count; i++)
        {
            if (sceneIDs[i] == CorrectResult[i])
            {
                SceneValue[i].text = "Correct!";
                RESULT += 1f;
            }
            else
            {
                SceneValue[i].text = "Error?";
            }
        }
    }

    public void CheckFinalResult() {
        if (RESULT == 6f)
        {
            Result.text = "A";
        }
        else if (RESULT > 3f && RESULT <= 6f)
        {
            Result.text = "B";
        }
        else {
            Result.text = "C";
        }
    }
}
