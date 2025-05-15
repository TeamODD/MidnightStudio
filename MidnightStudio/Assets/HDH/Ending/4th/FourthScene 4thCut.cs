using UnityEngine;
using System.Collections;

public class FourthScene_4thCut : MonoBehaviour
{
    public float Time;
    public SpriteRenderer Obj_SR;
    public Obj_Production Obj_OP;
    public UI_Production Obj_UP;
    public Sprite[] Protagonist = new Sprite[2];
    public bool IsTest = false;
    public FourthScene_Manager Scene_Manager;
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
        yield return new WaitForSeconds(1f);

        Obj_UP.Position("Instant", 0f, Vector3.zero, new Vector3(2.6f, -1.3f, 0f));

        Obj_SR.sprite = Protagonist[1];

        Obj_UP.Position("Smooth", 0.3f, new Vector3(2.6f, -1.45f, 0f), new Vector3(2.6f, -1.3f, 0f));
        Obj_OP.Coloring("Lerp", 0.07f, new Color(1f, 1f, 1f, 0.5f), new Color(1f, 1f, 1f, 1f));

        Scene_Manager.AudioPlayer.PlaySE(Clips[0]);
        yield return new WaitForSeconds(0.75f);
        // * 컷.
    }
}
