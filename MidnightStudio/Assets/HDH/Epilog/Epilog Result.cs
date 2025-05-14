using System.Collections.Generic;
using UnityEngine;

public class Epilog_Result : MonoBehaviour
{
    [SerializeField]
    private float Score = 0f;
    public string Grade = null;
    private List<string> Real_Sequence = new List<string> {"1", "3", "2", "4", "0"};

    public void MoviePlayer()
    {
        ResultData resultData_ = GameObject.Find("ResultData").GetComponent<ResultData>();

        Result_Cal(resultData_.finish_timer, resultData_.orderedSceneIdentifiers);
    }

    private void Result_Cal(float Time, List<string> Sequence)
    {
        if (Time <= 24f) { Score += 3f; }
        for (int i = 0; i < 5; i++) { if (Sequence[i] == Real_Sequence[i]) { Score += 1f; } }

        if (Score > 6f) { Grade = "A"; }
        else if (Score > 3f && Score <= 6f) { Grade = "B"; }
        else { Grade = "C"; }
    }
}