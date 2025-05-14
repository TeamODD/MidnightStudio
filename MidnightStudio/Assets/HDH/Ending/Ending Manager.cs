using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    public MoviePart_Manager MoviePartManager;

    private void Start()
    {
         MoviePlayer();
    }

    private void MoviePlayer()
    {
        ResultData resultData_ = GameObject.Find("ResultData").GetComponent<ResultData>();

        MoviePartManager.Production_Start(resultData_.orderedSceneIdentifiers);
    }

    public void MovieEnd()
    {
        SceneManager.LoadScene("Result");
    }
}
