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
        // ResultData 삭제
        ResultData result = FindObjectOfType<ResultData>();
        if (result != null)
        {
            Destroy(result.gameObject);
            Debug.Log("ResultData 오브젝트 삭제 완료");
        }

        // 타이틀 씬으로 전환
        SceneManager.LoadScene("Title");
    }
}
