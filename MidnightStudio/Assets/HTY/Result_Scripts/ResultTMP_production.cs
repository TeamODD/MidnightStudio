using NUnit.Framework.Constraints;
using System.Collections;
using UnityEngine;

public class ResultTMP_production : MonoBehaviour
{
    public GameObject RemainTime;

    public UI_Production[] Panel;
    public UI_Production[] Total;
    public UI_Production[] Sequences;
    public UI_Production[] Timer;

    private bool CanClick = true;

    public ResultMangager Manager;

    private AudioManager AudioPlayer;
    public AudioClip[] Clips;

    public void Start()
    {
        AudioPlayer = AudioManager.Instance;
        //StartShowResultObject();
    }

    public void StartDissolve(string Grade)
    {
        StartCoroutine(StartDissolve_Coroutine(Grade));
    }

    private IEnumerator StartDissolve_Coroutine(string Grade)
    {
        Panel[0].Alpha("Lerp", 0.5f, 1f, 0f);
        yield return new WaitForSeconds(2f);

        StartShowResultObject(Grade);
    }

    public void StartShowResultObject(string Grade)
    {
        StartCoroutine(ShowResultObject(Grade));
    }

    private IEnumerator ShowResultObject(string Grade)
    {
        Panel[0].Alpha("Lerp", 0.1f, 0f, 1f);
        Total[0].Scale("Lerp", 0.1f, new Vector3(2f, 2f, 2f), new Vector3(0.95f, 0.95f, 1f));
        Total[0].Alpha("Lerp", 0.1f, 0f, 1f);
        yield return new WaitForSeconds(0.1f);

        Total[0].Scale("Smooth", 0.1f, new Vector3(0.95f, 0.95f, 1f), new Vector3(1f, 1f, 1f));
        AudioPlayer.PlaySE(Clips[0]);
        yield return new WaitForSeconds(1f);

        Sequences[0].Move("Smooth", 0.2f, "x", -360f, -471f);
        Sequences[0].Alpha("Lerp", 0.2f, 0f, 1f);
        AudioPlayer.PlaySE(Clips[1]);
        yield return new WaitForSeconds(0.5f);

        for (int Repeat = 0; Repeat < 9; Repeat++)
        {
            Sequences[Repeat + 1].Move("Smooth", 0.1f, "y", -185f, -170f);
            Sequences[Repeat + 1].Alpha("Lerp", 0.05f, 0f, 1f);
            if (Repeat % 2 == 0) { AudioPlayer.PlaySE(Clips[2]); }
            yield return new WaitForSeconds(0.25f);
        }
        yield return new WaitForSeconds(0.5f);

        Sequences[10].Move("Smooth", 0.2f, "x", -610f, -721f);
        Sequences[10].Alpha("Lerp", 0.2f, 0f, 1f);
        AudioPlayer.PlaySE(Clips[1]);
        yield return new WaitForSeconds(1f);

        Timer[0].Move("Smooth", 0.2f, "x", -360f, -471f);
        Timer[0].Alpha("Lerp", 0.2f, 0f, 1f);
        AudioPlayer.PlaySE(Clips[1]);
        yield return new WaitForSeconds(0.5f);

        Timer[1].Move("Smooth", 0.1f, "y", -111f, 0f);
        Timer[1].Alpha("Lerp", 0.2f, 0f, 1f);
        AudioPlayer.PlaySE(Clips[2]);
        yield return new WaitForSeconds(1f);

        switch (Grade)
        {
            case "A":
                Total[1].Scale("Lerp", 0.1f, new Vector3(2f, 2f, 1f), new Vector3(0.95f, 0.95f, 1f));
                Total[1].Alpha("Instant", 0f, 0f, 1f);
                yield return new WaitForSeconds(0.1f);

                Total[1].Scale("Smooth", 0.1f, new Vector3(0.95f, 0.95f, 1f), new Vector3(1f, 1f, 1f));
                break;
            case "B":
                Total[2].Scale("Lerp", 0.1f, new Vector3(2f, 2f, 1f), new Vector3(0.95f, 0.95f, 1f));
                Total[2].Alpha("Instant", 0f, 0f, 1f);
                yield return new WaitForSeconds(0.1f);

                Total[2].Scale("Smooth", 0.1f, new Vector3(0.95f, 0.95f, 1f), new Vector3(1f, 1f, 1f));
                break;
            case "C":
                Total[3].Scale("Lerp", 0.1f, new Vector3(2f, 2f, 1f), new Vector3(0.95f, 0.95f, 1f));
                Total[3].Alpha("Instant", 0f, 0f, 1f);
                yield return new WaitForSeconds(0.1f);

                Total[3].Scale("Smooth", 0.1f, new Vector3(0.95f, 0.95f, 1f), new Vector3(1f, 1f, 1f));
                break;
        }
        AudioPlayer.PlaySE(Clips[0]);
        yield return new WaitForSeconds(1f);

        Total[4].gameObject.SetActive(true);
        Total[4].Move("Smooth", 0.2f, "y", -485f, -460f);
        Total[4].Alpha("Lerp", 0.2f, 0f, 1f);
        AudioPlayer.PlaySE(Clips[2]);
    }

    public void SceneChange()
    {
        if (CanClick == true)
        {
            CanClick = false;
            StartCoroutine(SceneChange_Coroutine());
        }
    }

    private IEnumerator SceneChange_Coroutine()
    {
        Panel[1].Move("Smooth", 1f, "x", 2400f, -1920f);
        Panel[1].Alpha("Instant", 0f, 0f, 1f);
        yield return new WaitForSeconds(0.5f);
        
        Manager.SceneChange();
    }
}
