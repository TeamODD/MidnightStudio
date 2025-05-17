using System.Collections.Generic;
using UnityEngine;

public class Epilog_Result : MonoBehaviour
{
    [SerializeField]
    
    public string Grade = null;
    private List<string> Real_Sequence = new List<string> {"1", "3", "2", "4", "0"};
    
    public void MoviePlayer()
    {
        ResultData resultData_ = GameObject.Find("ResultData").GetComponent<ResultData>();

        Result_Cal(resultData_.finish_timer, resultData_.orderedSceneIdentifiers);
    }

    private void Result_Cal(float Time, List<string> Sequence)
    {
        float Score = 0f;
        if (Time < 360f) { Score = 2.5f; }
        else if (Time >= 360f && Time < 420f) { Score = 2f; }
        else if (Time >= 420f) { Score = 1f; }

        for (int i = 0; i < Sequence.Count; i++)
        {
            if (Sequence[i] == Real_Sequence[i])
            {
                Score += 1f;
            }
        }

        if (Score >= 7.5f) { Grade = "A"; }
        else if (Score > 4f && Score < 7.5f) { Grade = "B"; }
        else { Grade = "C"; }
    }
}