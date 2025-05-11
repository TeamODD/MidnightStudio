using UnityEngine;

public class ResultMangager : MonoBehaviour
{
    public ResultTMP_production resultTMP_Production;
    public Result_FindValue result_FindValue;
    public void Start()
    {
        resultTMP_Production.StartShowResultObject();
        result_FindValue.RemainTimeChecking();
        result_FindValue.CheckSceneNumber();
        result_FindValue.CheckFinalResult();
    }
}
