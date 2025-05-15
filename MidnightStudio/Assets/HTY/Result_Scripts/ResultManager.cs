using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultMangager : MonoBehaviour
{
    public ResultTMP_production resultTMP_Production;
    public Result_FindValue result_FindValue;
    public void Start()
    {
        result_FindValue.VarySet();
        result_FindValue.RemainTimeChecking();
        result_FindValue.CheckSceneNumber();
        resultTMP_Production.StartDissolve(result_FindValue.CheckFinalResult());
    }

    public void SceneChange()
    {
        Destroy(ResultData.Instance);
        SceneManager.LoadScene("Title");
    }
}
