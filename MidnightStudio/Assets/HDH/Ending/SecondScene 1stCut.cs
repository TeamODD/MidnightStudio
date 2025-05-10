using System.Collections;
using System.Linq;
using UnityEngine;

public class SecondScene_1stCut : MonoBehaviour
{
    public float Time;
    public UI_Production[] Obj = new UI_Production[1];
    public SpriteRenderer Enemy_SR;
    public Sprite[] Enemy = new Sprite[3];
    public bool IsTest = false;
    public SecondScene_Manager Scene_Manager;
    public MoviePart_Subtitle Subtitle;
    private IEnumerator Production_Start_Sign;

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
        Enemy_SR.sprite = Enemy[0];
        Obj[0].Position("Smooth", 2f, new Vector3(-1.6f, -0.25f, -1f), new Vector3(-1.4f, 0f, -1f));
        Obj[0].Rotation("Smooth", 2f, new Vector3(0f, 0f, 4f), new Vector3(0f, 0f, 0f));
        Obj[0].Alpha("Instant", 0f, 0f, 1f);
        yield return new WaitForSeconds(0.25f);

        Subtitle.Engage("지상 최강의 명사수라며?", "Down", 2.25f);
        yield return new WaitForSeconds(2.25f);

        Subtitle.Engage("그러면...", "Down", 2f);
        Obj[0].Move("Smooth", 2f, "y", 0f, 0.15f);
        yield return new WaitForSeconds(2.25f);
        
        Enemy_SR.sprite = Enemy[1];
        Subtitle.Engage("확인 좀 해볼까!", "Down", 2f);
        Obj[0].Move("Smooth", 0.25f, "y", 0.15f, 0.5f);
        yield return new WaitForSeconds(0.25f);

        Enemy_SR.sprite = Enemy[2];
        Obj[0].Move("Instant", 0f, "x", 0f, 0.04f);
        Obj[0].Move("Lerp", 0.05f, "y", 0.5f, -0.3f);
        yield return new WaitForSeconds(0.1f);

        Obj[0].Move("Smooth", 0.1f, "y", -0.3f, 0f);
        yield return new WaitForSeconds(0.2f);

        // * 컷.
    }
}
