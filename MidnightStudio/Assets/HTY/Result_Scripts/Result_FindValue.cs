using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Result_FindValue : MonoBehaviour
{
    public TMP_Text RemainTime_Value;
    public TMP_Text Result;

    public Image[] Scene_Images;
    public Sprite[] Scene_Sprites;

    private float RESULT = 0f; //���� �����
    private string[] CorrectResult = {"1","3","2","4","0"};
    public int Correct;

    private float timer;
    // [SerializeField]
    public List<string> SceneIDs = new List<string>();

    public void VarySet()
    {
        // �� �ĺ��� ����Ʈ ��������
        SceneIDs = GameObject.Find("ResultData").GetComponent<ResultData>().orderedSceneIdentifiers;
        // Ÿ�̸� �� ��������
        timer =  GameObject.Find("ResultData").GetComponent<ResultData>().finish_timer;
    }

    public void RemainTimeChecking() {
        
        if (timer < 360f) { RESULT = 2.5f; }
        else if (timer >= 360f && timer < 420f) { RESULT = 2f; }
        else if (timer >= 420f) { RESULT = 1f; }
        Debug.Log(RESULT);

        string minutes = ((int)(timer / 60)).ToString();
        string seconds = ((int)(timer % 60)).ToString();

        if (timer <= 480f)
        {
            if ((int)(timer % 60) < 10f) { RemainTime_Value.text = "0" + minutes + ":" + "0" + seconds; }
            else { RemainTime_Value.text = "0" + minutes + ":" + seconds; }
        }
        else
        {
            if ((int)(timer % 60) < 10f) { RemainTime_Value.text = "0" + minutes + ":" + "0" + seconds; }
            else { RemainTime_Value.text = "0" + minutes + ":" + seconds; }
        }
        
    }

    public void CheckSceneNumber() {
        for (int i = 0; i < SceneIDs.Count; i++)
        {
            Scene_Images[i].sprite = Scene_Sprites[int.Parse(SceneIDs[i])];

            if (SceneIDs[i] == CorrectResult[i])
            {
                RESULT += 1f;
                Correct += 1;
            }
            
            Debug.Log(RESULT);
        }

        Result.text = $"정답 중 {Correct}개의 추리 정답.";
    }

    public string CheckFinalResult() {
        
        if (RESULT >= 7.5f) { return "A"; }
        else if (RESULT > 4f && RESULT < 7.5f) { return "B"; }
        else { return "C"; }
    }
}
