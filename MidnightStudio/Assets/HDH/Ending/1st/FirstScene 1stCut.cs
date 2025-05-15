using System.Collections;
using UnityEngine;

public class FirstScene_1stCut : MonoBehaviour
{
    public float Time;
    public UI_Production[] Obj = new UI_Production[3];
    public bool IsTest = false;
    public FirstScene_Manager Scene_Manager;
    public MoviePart_Subtitle Subtitle;
    private IEnumerator Production_Start_Sign;
    private IEnumerator Production_Protagonist_Sign;

    public AudioClip[] Clips;

    private void Start()
    {
        if (IsTest == true) { Production_Start(); }
    }

    // # 애니메이션을 시작하는 함수.
    public void Production_Start()
    {
        if (Production_Start_Sign != null) { StopCoroutine(Production_Start_Sign); }
        Production_Start_Sign = Production_Start_Coroutine();
        StartCoroutine(Production_Start_Sign);
    }

    // # 애니메이션 로직.
    private IEnumerator Production_Start_Coroutine()
    {
        // * 액션.
        Obj[0].Move("Lerp", 17.5f, "x", 17f, -17f);
        Production_Protagonist();
        Obj[2].Move("Lerp", 5f, "x", 20f, -13f);
        yield return new WaitForSeconds(2.5f);

        // * 디졸브
        Scene_Manager.Production_Ojbects[0].Move("Smooth", 1f, "x", 18f, -18f);
        yield return new WaitForSeconds(0.5f);

        // * 컷.
        if (Production_Protagonist_Sign != null) { StopCoroutine(Production_Protagonist_Sign); }
        yield return new WaitForSeconds(0.5f);
    }

    // # 주인공 캐릭터 걷는 애니메이션을 시작 함수.
    public void Production_Protagonist()
    {
        if (Production_Protagonist_Sign != null) { StopCoroutine(Production_Protagonist_Sign); }
        Production_Protagonist_Sign = Production_Protagonist_Coroutine();
        StartCoroutine(Production_Protagonist_Sign);
    }
    private IEnumerator Production_Protagonist_Coroutine()
    {
        while (true)
        {
            Obj[1].Move("Lerp", 0.1f, "y", -1f, -1.3f);
            yield return new WaitForSeconds(0.1f);
            Obj[1].Move("Smooth", 0.7f, "y", -1.3f, -1f);
            Scene_Manager.AudioPlayer.PlaySE(Clips[0]);
            yield return new WaitForSeconds(0.7f);

            Obj[1].Move("Lerp", 0.1f, "y", -1f, -1.3f);
            yield return new WaitForSeconds(0.1f);
            Obj[1].Move("Smooth", 0.7f, "y", -1.3f, -1f);
            Scene_Manager.AudioPlayer.PlaySE(Clips[1]);
            yield return new WaitForSeconds(0.7f);
        }
    }
}