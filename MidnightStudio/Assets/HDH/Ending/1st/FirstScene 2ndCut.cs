using System.Collections;
using UnityEngine;

public class FirstScene_2ndCut : MonoBehaviour
{
    public float Time = 2.7f;
    public UI_Production[] Obj = new UI_Production[3];
    public SpriteRenderer Protagonist_SR;
    public Sprite[] Protagonist = new Sprite[3];
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
        Obj[0].Scale("Lerp", 7.5f, new Vector3(1.2f, 1.2f, 1f), new Vector3(1f, 1f, 1f));
        Production_Protagonist();
        yield return new WaitForSeconds(1f);

        Subtitle.Engage("\"달의 그림자에게 안식을.\" 이거 맞나?", "Up", 2.5f);
        yield return new WaitForSeconds(1.25f);
        yield return new WaitForSeconds(0.5f);

        if (Production_Protagonist_Sign != null) { StopCoroutine(Production_Protagonist_Sign); }
        Scene_Manager.AudioPlayer.PlaySE(Clips[0]);
        Obj[0].Scale("Smooth", 1.5f, Obj[0].GetComponent<RectTransform>().localScale, new Vector3(1.13f, 1.13f, 0f));
        Obj[1].Position("Smooth", 0.45f, Obj[1].GetComponent<RectTransform>().anchoredPosition, new Vector3(2.4f, -1f, 0f));
        
        Obj[2].Position("Lerp", 0.2f, new Vector3(0f, 0f, 0f), new Vector3(-0.05f, 0.05f, 0f));
        Obj[2].Rotation("Lerp", 0.2f, new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 5f));
        Protagonist_SR.sprite = Protagonist[1];
        Scene_Manager.AudioPlayer.PlaySE(Clips[2]);
        yield return new WaitForSeconds(0.2f);

        Obj[2].Position("Smooth", 0.25f, new Vector3(-0.5f, 0f, 0f), new Vector3(-0.5f, 0.3f, 0f));
        Obj[2].Rotation("Smooth", 0.25f, new Vector3(0f, 0f, 2f), new Vector3(0f, 0f, 0f));
        Protagonist_SR.sprite = Protagonist[2];
        yield return new WaitForSeconds(0.5f);

        // * 컷.
        yield return new WaitForSeconds(1f);
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
            // * 오른발!
            Obj[1].Position("Lerp", 0.2f, new Vector3(2.4f, -1f, 0f), new Vector3(2.1f, -1.1f, 0f));
            Obj[1].Rotation("Lerp", 0.2f, new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0.5f));
            yield return new WaitForSeconds(0.2f);
            Obj[1].Position("Smooth", 0.6f, new Vector3(2.1f, -1.1f, 0f), new Vector3(2.4f, -1f, 0f));
            Obj[1].Rotation("Smooth", 0.6f, new Vector3(0f, 0f, 0.5f), new Vector3(0f, 0f, 0f));
            Scene_Manager.AudioPlayer.PlaySE(Clips[0]);
            yield return new WaitForSeconds(0.6f);

            // * 왼발!
            Obj[1].Position("Lerp", 0.2f, new Vector3(2.4f, -1f, 0f), new Vector3(2.65f, -1.1f, 0f));
            Obj[1].Rotation("Lerp", 0.2f, new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, -0.5f));
            yield return new WaitForSeconds(0.2f);
            Obj[1].Position("Smooth", 0.6f, new Vector3(2.65f, -1.1f, 0f), new Vector3(2.4f, -1f, 0f));
            Obj[1].Rotation("Smooth", 0.6f, new Vector3(0f, 0f, -0.5f), new Vector3(0f, 0f, 0f));
            Scene_Manager.AudioPlayer.PlaySE(Clips[1]);
            yield return new WaitForSeconds(0.6f);
        }
    }
}
