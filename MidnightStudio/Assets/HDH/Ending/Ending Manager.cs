using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    public MoviePart_Manager MoviePartManager;
    public AudioManager AudioPlayer;
    public Epilog_Result EpilogResult;
    public AudioClip Clip;

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

        EpilogResult.MoviePlayer();
        if (EpilogResult.Grade == "A") { AudioPlayer.PlayBGM(Clip); }
    }

    public void MovieEnd()
    {
        Debug.Log(EpilogResult.Grade);
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
