using UnityEngine;
using System.Collections;

public class FifthScene_1stCut : MonoBehaviour
{
    public float Time;
    public bool IsTest = false;
    public FifthScene_Manager Scene_Manager;
    public MoviePart_Subtitle Subtitle;
    private IEnumerator Production_Start_Sign;

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
        yield return new WaitForSeconds(0.25f);

        Subtitle.Engage("어머. 소심한 반항인걸까?", "Down", 2f);
        yield return new WaitForSeconds(2.25f);

        Subtitle.Engage("그냥 순순히 최후를 맞이ㅎ...", "Down", 1.5f);
        yield return new WaitForSeconds(0.05f);

        Scene_Manager.AudioPlayer.PlaySE(Clips[0]);
        yield return new WaitForSeconds(0.05f);
        // * 컷.
    }
}
