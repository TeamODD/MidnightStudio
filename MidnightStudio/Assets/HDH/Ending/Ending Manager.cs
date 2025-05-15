using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    public MoviePart_Manager MoviePartManager;
    public AudioManager AudioPlayer;
    public Epilog_Result EpilogResult;

    private void Start()
    {
        AudioPlayer = AudioManager.Instance;
        AudioPlayer.StopBGM();

        MoviePlayer();
    }

    private void MoviePlayer()
    {
        ResultData resultData_ = GameObject.Find("ResultData").GetComponent<ResultData>();

        MoviePartManager.Production_Start(resultData_.orderedSceneIdentifiers);
    }

    public void MovieEnd()
    {
        EpilogResult.MoviePlayer();

        switch (EpilogResult.Grade)
        {
            case "A":
                SceneManager.LoadScene("Credit");
                break;
            case "B":
                SceneManager.LoadScene("Result");
                break;
        }
    }
}
